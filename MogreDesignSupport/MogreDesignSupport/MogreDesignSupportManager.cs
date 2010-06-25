using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Mogre;
using System.Drawing.Design;
using System.Reflection;
using MogreDesignSupport.TypeConverters;
using MogreDesignSupport.TypeEditors;

namespace MogreDesignSupport
{
    public static class MogreDesignSupportManager
    {
        static MogreDesignSupport mogreDesignSupport;
        public static MogreDesignSupport MogreDesignSupport
        {
            get { return MogreDesignSupportManager.mogreDesignSupport; }
        }

        public static bool IsInitialized
        {
            get{ return (mogreDesignSupport!=null); }
        }

        public static bool Initialize(bool loadAllTypes)
        {
            //if field is already initialized do nothing.
            if (IsInitialized)
            {
                MogreDesignSupportManager.Log("MogreDesignSupport - Failed Initialization: already initialized.", LogMessageLevel.LML_TRIVIAL);
                return false;
            }

            MogreDesignSupportManager.mogreDesignSupport = new MogreDesignSupport();
            MogreDesignSupportManager.mogreDesignSupport.Load(loadAllTypes);

            return true;
        }

        public static bool Deinitialize()
        {
            if (!IsInitialized)
            {
                MogreDesignSupportManager.Log("MogreDesignSupport - Failed Deinitialization: Mogre Design Support was not initialized.", LogMessageLevel.LML_TRIVIAL);
                return false;
            }

            mogreDesignSupport.Unload();
            mogreDesignSupport = null;
            return true;
        }

        public static void Log(string message,LogMessageLevel logLevel)
        {
            if (LogManager.Singleton == null)
                return;

            MogreDesignSupportManager.Log(message, logLevel);
        }
    }
    
    public class MogreDesignSupport 
    {
        List<KeyValuePair<Type, TypeDescriptionProvider>> providers = new List<KeyValuePair<Type, TypeDescriptionProvider>>();

        public UniversalTypeDescriptorProvider DefaultTypeDescriptorProvider{get;protected set;}

        public UniversalTypeDescriptorProvider FallbackTypeDescriptorProvider { get; protected set; }

        public bool IsLoaded
        {
            get;
            protected set;
        }

        public void Load(bool loadAllTypes)
        {
            if (IsLoaded) return;
            OnLoad(loadAllTypes);
            IsLoaded = true;
        }

        protected virtual void OnLoad(bool loadAllTypes)
        {
            if (loadAllTypes)
                LoadFallbackProviders();
            LoadDefaultProviders();
        }

        public void Unload()
        {
            if (!IsLoaded) return;
            OnUnload();
            IsLoaded = false;
        }

        protected virtual void OnUnload()
        {
            for (int i = 0; i < providers.Count; i++)
                TypeDescriptor.RemoveProvider(providers[i].Value, providers[i].Key);

            providers.Clear();
        }

        void LoadDefaultProviders()
        {
            UniversalTypeDescriptorProvider utd = new UniversalTypeDescriptorProvider(TypeDescriptor.GetProvider(typeof(object)));
            utd.Suspended = true;

            utd.AddUniversalTypeDescriptor(typeof(Vector2), new TypeConverterAttribute(typeof(Vector2Converter)));
            utd.AddUniversalTypeDescriptor(typeof(Vector3), new TypeConverterAttribute(typeof(Vector3Converter)));
            utd.AddUniversalTypeDescriptor(typeof(Vector4), new TypeConverterAttribute(typeof(Vector4Converter)));
            utd.AddUniversalTypeDescriptor(typeof(Quaternion), new TypeConverterAttribute(typeof(QuaternionConverter)));
            utd.AddUniversalTypeDescriptor(typeof(Matrix3), new EditorAttribute(typeof(MatrixEditor), typeof(UITypeEditor)));
            utd.AddUniversalTypeDescriptor(typeof(Matrix4), new EditorAttribute(typeof(MatrixEditor), typeof(UITypeEditor)));
            utd.AddUniversalTypeDescriptor(typeof(ColourValue), new TypeConverterAttribute(typeof(ColourValueConverter)), new EditorAttribute(typeof(ColourValueEditor), typeof(UITypeEditor)));
            utd.AddUniversalTypeDescriptor(typeof(AxisAlignedBox), new TypeConverterAttribute(typeof(AxisAlignedBoxConverter)));


            TypeDescriptor.AddProvider(utd, typeof(Vector2));
            TypeDescriptor.AddProvider(utd, typeof(Vector3));
            TypeDescriptor.AddProvider(utd, typeof(Vector4));
            TypeDescriptor.AddProvider(utd, typeof(Quaternion));
            TypeDescriptor.AddProvider(utd, typeof(Matrix3));
            TypeDescriptor.AddProvider(utd, typeof(Matrix4));
            TypeDescriptor.AddProvider(utd, typeof(ColourValue));
            TypeDescriptor.AddProvider(utd, typeof(AxisAlignedBox));


            providers.Add(new KeyValuePair<Type, TypeDescriptionProvider>(typeof(Vector2), utd));
            providers.Add(new KeyValuePair<Type, TypeDescriptionProvider>(typeof(Vector3), utd));
            providers.Add(new KeyValuePair<Type, TypeDescriptionProvider>(typeof(Vector4), utd));
            providers.Add(new KeyValuePair<Type, TypeDescriptionProvider>(typeof(Quaternion), utd));
            providers.Add(new KeyValuePair<Type, TypeDescriptionProvider>(typeof(Matrix3), utd));
            providers.Add(new KeyValuePair<Type, TypeDescriptionProvider>(typeof(Matrix4), utd));
            providers.Add(new KeyValuePair<Type, TypeDescriptionProvider>(typeof(ColourValue), utd));
            providers.Add(new KeyValuePair<Type, TypeDescriptionProvider>(typeof(AxisAlignedBox), utd));

            utd.Suspended = false;

            DefaultTypeDescriptorProvider=utd;
        }

        void LoadFallbackProviders()
        {
            Assembly ass = Assembly.GetAssembly(typeof(Mogre.Root));
            Type[] mogreTypes = ass.GetTypes();

            UniversalTypeDescriptorProvider utd = new UniversalTypeDescriptorProvider(TypeDescriptor.GetProvider(typeof(object)));
            utd.Suspended = true;
            foreach (Type type in mogreTypes)
            {
                if (type.IsEnum || type.IsNotPublic)
                    continue;

                if (type.IsValueType)
                    utd.AddUniversalTypeDescriptor(type, new TypeConverterAttribute(typeof(TypeConverter)), new EditorAttribute(typeof(FieldsEditor), typeof(UITypeEditor)));
                else if (type.IsClass)
                    utd.AddUniversalTypeDescriptor(type, new TypeConverterAttribute(typeof(ExpandableObjectConverter)));
                else
                    continue;

                TypeDescriptor.AddProvider(utd, type);
                providers.Add(new KeyValuePair<Type, TypeDescriptionProvider>(type, utd));
            }
            utd.Suspended = false;

            FallbackTypeDescriptorProvider = utd;
        }
    }

    public class UniversalTypeDescriptor : CustomTypeDescriptor
    {
        public bool InheritAttributes { get; set; }
        public List<Attribute> Attributes { get; protected set; }

        public bool InheritTypeConverter { get; set; }
        public TypeConverter TypeConverter { get; protected set; }

        public bool InheritTypeEditor { get; set; }
        public Dictionary<Type, object> TypeEditors { get; protected set; }

        protected Attribute[] baseAttributes = null;

        public UniversalTypeDescriptor(ICustomTypeDescriptor parent)
            : base(parent)
        {
            InheritAttributes = true;
            InheritTypeConverter = true;
            InheritTypeEditor = true;

            Attributes = new List<Attribute>();
            TypeEditors = new Dictionary<Type, object>();
        }

        public override AttributeCollection GetAttributes()
        {
            if (InheritAttributes)
            {
                if (baseAttributes == null)
                    baseAttributes = GetBaseAttributes();

                Attribute[] curAttribs = new Attribute[baseAttributes.Length + Attributes.Count];
                baseAttributes.CopyTo(curAttribs, 0);
                Attributes.CopyTo(curAttribs, baseAttributes.Length);
                AttributeCollection atc = new AttributeCollection(curAttribs);
                return atc;
            }
            else
            {
                return base.GetAttributes();
            }
        }

        Attribute[] GetBaseAttributes()
        {
            AttributeCollection atrCol = base.GetAttributes();
            Attribute[] attr = new Attribute[atrCol.Count];
            atrCol.CopyTo(attr, 0);
            return attr;
        }

        public override TypeConverter GetConverter()
        {
            if (TypeConverter == null)
                return base.GetConverter();
            else if (InheritTypeConverter)
                return TypeConverter;
            else
                return null;
        }

        public override object GetEditor(Type editorBaseType)
        {
            object editor;
            if (TypeEditors.TryGetValue(editorBaseType, out editor))
                return editor;
            else if (InheritTypeEditor)
                return base.GetEditor(editorBaseType);
            else
                return null;
        }
    }

    public class UniversalTypeDescriptorProvider : TypeDescriptionProvider
    {

        //we suspend in case when we add types to provider and don't want to call this GetTypeDescriptor
        public bool Suspended { get; set; }

        public Dictionary<Type, ICustomTypeDescriptor> TypeDescriptors{get;protected set;}

        public UniversalTypeDescriptorProvider(TypeDescriptionProvider parent)
            : base(parent)
        {
            TypeDescriptors= new Dictionary<Type, ICustomTypeDescriptor>();
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            if (Suspended == true)
                return base.GetTypeDescriptor(objectType, instance);

            ICustomTypeDescriptor ictd;
            if (TypeDescriptors.TryGetValue(objectType, out ictd))
                return ictd;

            Type[] types = new Type[TypeDescriptors.Keys.Count];
            TypeDescriptors.Keys.CopyTo(types, 0);


            for (int i = 0; i < types.Length; i++)
                if (types[i].IsAssignableFrom(objectType))
                    return TypeDescriptors[types[i]];


            return base.GetTypeDescriptor(objectType, instance);
        }
 
        public UniversalTypeDescriptor AddUniversalTypeDescriptor(Type type,params Attribute[] attribs)
        {
            UniversalTypeDescriptor utd = new UniversalTypeDescriptor(base.GetTypeDescriptor(type));
            utd.Attributes.AddRange(attribs);
            TypeDescriptors.Add(type, utd);
            return utd;
        }
    }

}
