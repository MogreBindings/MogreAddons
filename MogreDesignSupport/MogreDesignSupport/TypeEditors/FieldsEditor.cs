using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using Mogre;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Reflection;
using System.ComponentModel;

namespace MogreDesignSupport.TypeEditors
{
    //Fields Editor was ripped from the internet almost entirely 
    

    internal class FieldsEditorDialog : Form
    {
        object target;
        private PropertyGrid propertyGridFields;
    
        public object Target
        {
            get { return target; }
            set { target = value; }
        }

        private void InitializeComponent()
        {
            this.propertyGridFields = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGridFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridFields.Location = new System.Drawing.Point(0, 0);
            this.propertyGridFields.Name = "propertyGrid1";
            this.propertyGridFields.Size = new System.Drawing.Size(237, 287);
            this.propertyGridFields.TabIndex = 1;
            // 
            // FieldsEditorDialog
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(237, 287);
            this.Controls.Add(this.propertyGridFields);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FieldsEditorDialog";
            this.Text = "Fields Editor";
            this.ResumeLayout(false);

        }

        public FieldsEditorDialog(object obj)
        {
            this.target = obj;
            InitializeComponent();
            this.Text = obj.GetType().Name;

            propertyGridFields.SelectedObject = new FieldsToPropertiesProxyTypeDescriptor(obj);
        }
    }

    public class FieldsEditor : System.Drawing.Design.UITypeEditor
    {
        public FieldsEditor()
        {
        }

        // Indicates whether the UITypeEditor provides a form-based (modal) dialog, 
        // drop down dialog, or no UI outside of the properties window.
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        // Displays the UI for value selection.
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            FieldsEditorDialog fieldsDialog = new FieldsEditorDialog(value);
            using (fieldsDialog)
            {
                fieldsDialog.ShowDialog();

                if(context.PropertyDescriptor.PropertyType.IsValueType)
                    context.PropertyDescriptor.SetValue(context.Instance, fieldsDialog.Target);
                
                return fieldsDialog.Target;
            }
        }

    }



    public class FieldsToPropertiesProxyTypeDescriptor : ICustomTypeDescriptor
    {
        private object _target; // object to be described

        private PropertyDescriptorCollection _propCache;
        private FilterCache _filterCache;

        public FieldsToPropertiesProxyTypeDescriptor(object target)
        {
            if (target == null) throw new ArgumentNullException("target");
            _target = target;
        }

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return _target; // properties belong to the target object
        }

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            // Gets the attributes of the target object
            return TypeDescriptor.GetAttributes(_target, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            // Gets the class name of the target object
            return TypeDescriptor.GetClassName(_target, true);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(null);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
            Attribute[] attributes)
        {
            bool filtering = (attributes != null && attributes.Length > 0);
            PropertyDescriptorCollection props = _propCache;
            FilterCache cache = _filterCache;

            // Use a cached version if possible
            if (filtering && cache != null && cache.IsValid(attributes))
                return cache.FilteredProperties;
            else if (!filtering && props != null)
                return props;

            // Create the property collection and filter
            props = new PropertyDescriptorCollection(null);
            foreach (PropertyDescriptor prop in
                TypeDescriptor.GetProperties(
                _target, attributes, true))
            {
                props.Add(prop);
            }
            foreach (FieldInfo field in _target.GetType().GetFields())
            {
                FieldPropertyDescriptor fieldDesc =
                    new FieldPropertyDescriptor(field);
                if (!filtering ||
                    fieldDesc.Attributes.Contains(attributes))
                    props.Add(fieldDesc);
            }

            // Store the computed properties
            if (filtering)
            {
                cache = new FilterCache();
                cache.Attributes = attributes;
                cache.FilteredProperties = props;
                _filterCache = cache;
            }
            else _propCache = props;

            return props;
        }

        #region ICustomTypeDescriptor Members


        public string GetComponentName()
        {
            return null;
        }

        public TypeConverter GetConverter()
        {
            return null;
        }

        public EventDescriptor GetDefaultEvent()
        {
            return null;
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return null;
        }

        public object GetEditor(Type editorBaseType)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return null;
        }

        public EventDescriptorCollection GetEvents()
        {
            return null;
        }

        #endregion
    }

    internal class FilterCache
    {
        public Attribute[] Attributes;
        public PropertyDescriptorCollection FilteredProperties;
        public bool IsValid(Attribute[] other)
        {
            if (other == null || Attributes == null) return false;

            if (Attributes.Length != other.Length) return false;

            for (int i = 0; i < other.Length; i++)
            {
                if (!Attributes[i].Match(other[i])) return false;
            }

            return true;
        }
    }

    internal class FieldPropertyDescriptor : PropertyDescriptor
    {
        private FieldInfo _field;

        public FieldInfo Field { get { return _field; } }

        public FieldPropertyDescriptor(FieldInfo field)
            : base(field.Name,
                (Attribute[])field.GetCustomAttributes(typeof(Attribute), true))
        {
            _field = field;
        }

        public override bool Equals(object obj)
        {
            FieldPropertyDescriptor other = obj as FieldPropertyDescriptor;
            return other != null && other._field.Equals(_field);
        }

        public override int GetHashCode() { return _field.GetHashCode(); }

        public override bool IsReadOnly { get { return false; } }

        public override void ResetValue(object component) { }

        public override bool CanResetValue(object component) { return false; }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get { return _field.DeclaringType; }
        }

        public override Type PropertyType { get { return _field.FieldType; } }

        public override object GetValue(object component)
        {
            return _field.GetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            _field.SetValue(component, value);
            OnValueChanged(component, EventArgs.Empty);
        }
    }


}