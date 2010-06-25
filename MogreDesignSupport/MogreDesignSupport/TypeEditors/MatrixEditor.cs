using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Mogre;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms.Design;

namespace MogreDesignSupport.TypeEditors
{
    internal class MatrixEditorControl : UserControl
    {
        enum MatrixType
        {
            _3x3,
            _4x4,
        }
        
        public object Matrix
        {
            get { return MatrixFromBoxes(); }
            set { MatrixToBoxes(value); }
        }

        MatrixType matrixType;

        TextBox[,] textBoxes;

        object baseMatrix;

        private Panel panelMatrixBorder;
        private LinkLabel linkLabelIdentity;
        private LinkLabel linkLabelZero;
        private LinkLabel linkLabelClipSpace2dToImageSpace;
        private LinkLabel linkLabelRevert;
        private System.ComponentModel.IContainer components;

        private TableLayoutPanel tableLayoutMElements;

        private void InitializeComponent()
        {
            this.tableLayoutMElements = new System.Windows.Forms.TableLayoutPanel();
            this.panelMatrixBorder = new System.Windows.Forms.Panel();
            this.linkLabelIdentity = new System.Windows.Forms.LinkLabel();
            this.linkLabelZero = new System.Windows.Forms.LinkLabel();
            this.linkLabelClipSpace2dToImageSpace = new System.Windows.Forms.LinkLabel();
            this.linkLabelRevert = new System.Windows.Forms.LinkLabel();
            this.panelMatrixBorder.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutMElements
            // 
            this.tableLayoutMElements.BackColor = System.Drawing.Color.Gray;
            this.tableLayoutMElements.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutMElements.ColumnCount = 1;
            this.tableLayoutMElements.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutMElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMElements.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMElements.Name = "tableLayoutMElements";
            this.tableLayoutMElements.RowCount = 1;
            this.tableLayoutMElements.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.00062F));
            this.tableLayoutMElements.Size = new System.Drawing.Size(263, 133);
            this.tableLayoutMElements.TabIndex = 0;
            // 
            // panelMatrixBorder
            // 
            this.panelMatrixBorder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMatrixBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMatrixBorder.Controls.Add(this.tableLayoutMElements);
            this.panelMatrixBorder.Location = new System.Drawing.Point(0, 0);
            this.panelMatrixBorder.Name = "panelMatrixBorder";
            this.panelMatrixBorder.Size = new System.Drawing.Size(265, 135);
            this.panelMatrixBorder.TabIndex = 1;
            // 
            // linkLabelIdentity
            // 
            this.linkLabelIdentity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelIdentity.AutoSize = true;
            this.linkLabelIdentity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelIdentity.Location = new System.Drawing.Point(3, 138);
            this.linkLabelIdentity.Name = "linkLabelIdentity";
            this.linkLabelIdentity.Size = new System.Drawing.Size(49, 13);
            this.linkLabelIdentity.TabIndex = 2;
            this.linkLabelIdentity.TabStop = true;
            this.linkLabelIdentity.Text = "Identity";
            this.linkLabelIdentity.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Identity_LinkClicked);
            // 
            // linkLabelZero
            // 
            this.linkLabelZero.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelZero.AutoSize = true;
            this.linkLabelZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelZero.Location = new System.Drawing.Point(58, 138);
            this.linkLabelZero.Name = "linkLabelZero";
            this.linkLabelZero.Size = new System.Drawing.Size(33, 13);
            this.linkLabelZero.TabIndex = 3;
            this.linkLabelZero.TabStop = true;
            this.linkLabelZero.Text = "Zero";
            this.linkLabelZero.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Zero_LinkClicked);
            // 
            // linkLabelClipSpace2dToImageSpace
            // 
            this.linkLabelClipSpace2dToImageSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelClipSpace2dToImageSpace.AutoSize = true;
            this.linkLabelClipSpace2dToImageSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelClipSpace2dToImageSpace.Location = new System.Drawing.Point(97, 138);
            this.linkLabelClipSpace2dToImageSpace.Name = "linkLabelClipSpace2dToImageSpace";
            this.linkLabelClipSpace2dToImageSpace.Size = new System.Drawing.Size(138, 13);
            this.linkLabelClipSpace2dToImageSpace.TabIndex = 4;
            this.linkLabelClipSpace2dToImageSpace.TabStop = true;
            this.linkLabelClipSpace2dToImageSpace.Text = "ClipSpace2DToImageSpace";
            this.linkLabelClipSpace2dToImageSpace.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ClipSpace2dToImageSpace_LinkClicked);
            // 
            // linkLabelRevert
            // 
            this.linkLabelRevert.AutoSize = true;
            this.linkLabelRevert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelRevert.LinkColor = System.Drawing.Color.Green;
            this.linkLabelRevert.Location = new System.Drawing.Point(247, 138);
            this.linkLabelRevert.Name = "linkLabelRevert";
            this.linkLabelRevert.Size = new System.Drawing.Size(16, 13);
            this.linkLabelRevert.TabIndex = 5;
            this.linkLabelRevert.TabStop = true;
            this.linkLabelRevert.Text = "R";
            this.linkLabelRevert.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Revert_LinkClicked);
            // 
            // MatrixEditorControl
            // 
            this.Controls.Add(this.linkLabelRevert);
            this.Controls.Add(this.linkLabelClipSpace2dToImageSpace);
            this.Controls.Add(this.linkLabelZero);
            this.Controls.Add(this.linkLabelIdentity);
            this.Controls.Add(this.panelMatrixBorder);
            this.Name = "MatrixEditorControl";
            this.Size = new System.Drawing.Size(265, 156);
            this.panelMatrixBorder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public MatrixEditorControl(object matrix)
        {
            InitializeComponent();

            baseMatrix = matrix;

            if (matrix is Matrix3)
            {
                matrixType = MatrixType._3x3;
                textBoxes = new TextBox[3, 3];
                linkLabelClipSpace2dToImageSpace.Visible = false;
            }
            else if (matrix is Matrix4)
            {
                matrixType = MatrixType._4x4;
                textBoxes = new TextBox[4, 4];
                linkLabelClipSpace2dToImageSpace.Visible = true;
            }

            InitializeMatrixEditor();

            MatrixToBoxes(matrix);
        }

        TextBox GetTextBox()
        {
            TextBox tb = new TextBox();
            tb.Enabled = true;
            tb.Visible = true;
            tb.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tb.Validating += new System.ComponentModel.CancelEventHandler(NumbersOnly_Validating);
            return tb;
        }

        void NumbersOnly_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            String text = (sender as TextBox).Text;

            float res;
            if (!float.TryParse(text, out res))
                e.Cancel = true;
        }
       
        void InitializeMatrixEditor()
        {
            int rowsC = textBoxes.GetLength(0);
            int colsC = textBoxes.GetLength(1);

            this.tableLayoutMElements.ColumnStyles.Clear();
            this.tableLayoutMElements.RowStyles.Clear();

            this.tableLayoutMElements.ColumnCount = colsC;
            this.tableLayoutMElements.RowCount = rowsC;

            for (int i = 0; i < colsC; i++)
                this.tableLayoutMElements.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100.0f / colsC));

            for (int i = 0; i < rowsC; i++)
                this.tableLayoutMElements.RowStyles.Add(new RowStyle(SizeType.Percent, 100.0f / rowsC));


            for (int i = 0; i < colsC; i++)
                for (int j = 0; j < rowsC; j++)
                {
                    TextBox tb;
                    textBoxes[j,i]=tb=GetTextBox();
                    tableLayoutMElements.Controls.Add(tb, i, j);
                }
        }

        void MatrixToBoxes(object matrix)
        {
            if(matrixType== MatrixType._3x3)
            {
                Matrix3 m3 = (Matrix3)matrix;

                textBoxes[0, 0].Text = m3.m00.ToString();
                textBoxes[1, 0].Text = m3.m10.ToString();
                textBoxes[2, 0].Text = m3.m20.ToString();

                textBoxes[0, 1].Text = m3.m01.ToString();
                textBoxes[1, 1].Text = m3.m11.ToString();
                textBoxes[2, 1].Text = m3.m21.ToString();

                textBoxes[0, 2].Text = m3.m02.ToString();
                textBoxes[1, 2].Text = m3.m12.ToString();
                textBoxes[2, 2].Text = m3.m22.ToString();
            }

            if (matrixType == MatrixType._4x4)
            {
                Matrix4 m4 = (Matrix4)matrix;

                textBoxes[0, 0].Text = m4.m00.ToString();
                textBoxes[1, 0].Text = m4.m10.ToString();
                textBoxes[2, 0].Text = m4.m20.ToString();
                textBoxes[3, 0].Text = m4.m30.ToString();

                textBoxes[0, 1].Text = m4.m01.ToString();
                textBoxes[1, 1].Text = m4.m11.ToString();
                textBoxes[2, 1].Text = m4.m21.ToString();
                textBoxes[3, 1].Text = m4.m31.ToString();

                textBoxes[0, 2].Text = m4.m02.ToString();
                textBoxes[1, 2].Text = m4.m12.ToString();
                textBoxes[2, 2].Text = m4.m22.ToString();
                textBoxes[3, 2].Text = m4.m32.ToString();

                textBoxes[0, 3].Text = m4.m03.ToString();
                textBoxes[1, 3].Text = m4.m13.ToString();
                textBoxes[2, 3].Text = m4.m23.ToString();
                textBoxes[3, 3].Text = m4.m33.ToString();
            }
        }

        object MatrixFromBoxes()
        {
            if (matrixType == MatrixType._3x3)
            {
                Matrix3 m3=new Matrix3();
                float.TryParse(textBoxes[0, 0].Text,out m3.m00);
                float.TryParse(textBoxes[1, 0].Text, out m3.m10);
                float.TryParse(textBoxes[2, 0].Text, out m3.m20);

                float.TryParse(textBoxes[0, 1].Text, out m3.m01);
                float.TryParse(textBoxes[1, 1].Text, out m3.m11);
                float.TryParse(textBoxes[2, 1].Text, out m3.m21);

                float.TryParse(textBoxes[0, 2].Text, out m3.m02);
                float.TryParse(textBoxes[1, 2].Text, out m3.m12);
                float.TryParse(textBoxes[2, 2].Text, out m3.m22);
                return m3;
            }

            if (matrixType == MatrixType._4x4)
            {
                Matrix4 m4 = new Matrix4();
                float.TryParse(textBoxes[0, 0].Text, out m4.m00);
                float.TryParse(textBoxes[1, 0].Text, out m4.m10);
                float.TryParse(textBoxes[2, 0].Text, out m4.m20);
                float.TryParse(textBoxes[3, 0].Text, out m4.m30);

                float.TryParse(textBoxes[0, 1].Text, out m4.m01);
                float.TryParse(textBoxes[1, 1].Text, out m4.m11);
                float.TryParse(textBoxes[2, 1].Text, out m4.m21);
                float.TryParse(textBoxes[3, 1].Text, out m4.m31);

                float.TryParse(textBoxes[0, 2].Text, out m4.m02);
                float.TryParse(textBoxes[1, 2].Text, out m4.m12);
                float.TryParse(textBoxes[2, 2].Text, out m4.m22);
                float.TryParse(textBoxes[3, 2].Text, out m4.m32);

                float.TryParse(textBoxes[0, 3].Text, out m4.m03);
                float.TryParse(textBoxes[1, 3].Text, out m4.m13);
                float.TryParse(textBoxes[2, 3].Text, out m4.m23);
                float.TryParse(textBoxes[3, 3].Text, out m4.m33);

                return m4;
            }

            return null;
        }

        private void Identity_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (matrixType == MatrixType._3x3)
                MatrixToBoxes(Matrix3.IDENTITY);

            if (matrixType == MatrixType._4x4)
                MatrixToBoxes(Matrix4.IDENTITY);
        }

        private void Zero_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (matrixType == MatrixType._3x3)
                MatrixToBoxes(Matrix3.ZERO);

            if (matrixType == MatrixType._4x4)
                MatrixToBoxes(Matrix4.ZERO);
        }

        private void ClipSpace2dToImageSpace_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (matrixType == MatrixType._4x4)
                MatrixToBoxes(Matrix4.CLIPSPACE2DTOIMAGESPACE);
        }

        private void Revert_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MatrixToBoxes(baseMatrix);
        }

    }

    public class MatrixEditor : System.Drawing.Design.UITypeEditor
    {
        public MatrixEditor()
        {
        }

        // Indicates whether the UITypeEditor provides a form-based (modal) dialog, 
        // drop down dialog, or no UI outside of the properties window.
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        // Displays the UI for value selection.
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (value == null)
            {
                return null;
                /*
if (context == null)
    return null;


Type propType = context.PropertyDescriptor.PropertyType;
if (propType == typeof(Matrix3))
    value = new Matrix3();
else if (propType == typeof(Matrix4))
    value = new Matrix4();
*/
            }

            if (value.GetType() != typeof(Matrix3) && value.GetType() != typeof(Matrix4))
                return value;

            // Uses the IWindowsFormsEditorService to display a 
            // drop-down UI in the Properties window.
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                MatrixEditorControl matrixControl = new MatrixEditorControl(value);
                edSvc.DropDownControl(matrixControl);

                return matrixControl.Matrix;

            }
            return value;
        }
    }
}
