namespace TestApplication
{
    partial class testForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mogrePropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // mogrePropertyGrid
            // 
            this.mogrePropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mogrePropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.mogrePropertyGrid.Name = "mogrePropertyGrid";
            this.mogrePropertyGrid.Size = new System.Drawing.Size(292, 266);
            this.mogrePropertyGrid.TabIndex = 0;
            // 
            // testForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.mogrePropertyGrid);
            this.Name = "testForm";
            this.Text = "Design Support Test";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid mogrePropertyGrid;
    }
}

