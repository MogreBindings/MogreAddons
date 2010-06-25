using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using Mogre;
using System.Windows.Forms.Design;
using System.Drawing;

namespace MogreDesignSupport.TypeEditors
{
    public class ColourValueEditor : System.Drawing.Design.UITypeEditor
    {
        public ColourValueEditor()
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
            // Return the value if the value is not of type Int32, Double and Single.
            if (value!=null && value.GetType() != typeof(ColourValue))
                return value;

            // Uses the IWindowsFormsEditorService to display a 
            // drop-down UI in the Properties window.
            //IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

            ColorDialog colorDialog = new ColorDialog();
            using (colorDialog)
                if(colorDialog.ShowDialog()== DialogResult.OK)
                {
                    ColourValue cv=new ColourValue();
                    cv.SetAsARGB((uint)colorDialog.Color.ToArgb());
                    return cv;
                }

            return value;
        }

        // Draws a representation of the property's value.
        public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
        {
            if(!(e.Value is ColourValue)) return;

            ColourValue colVal=(ColourValue)e.Value;
            Color col = Color.FromArgb((int)colVal.GetAsARGB());

            int normalX = (e.Bounds.Width / 2);
            int normalY = (e.Bounds.Height / 2);

            // Fill rectangle indicating current color in property grid 
            e.Graphics.FillRectangle(new SolidBrush(col), e.Bounds);

            System.Drawing.Rectangle alphaBounds = e.Bounds;
            alphaBounds.Width = (alphaBounds.Width * 30) / 100;
            //alphaBounds.X = alphaBounds.X + e.Bounds.Width - alphaBounds.Width;

            colVal.a = 1;
            col = Color.FromArgb((int)colVal.GetAsARGB());
            e.Graphics.FillRectangle(new SolidBrush(col),alphaBounds);
        }

        // Indicates whether the UITypeEditor supports painting a 
        // representation of a property's value.
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return true;
        } 
    }
}
