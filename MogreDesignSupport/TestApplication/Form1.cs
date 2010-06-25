using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MogreDesignSupport;

namespace TestApplication
{
    public partial class testForm : Form
    {
        public testForm()
        {
            InitializeComponent();

            MogreDesignSupport.MogreDesignSupportManager.Initialize(false);

            TestObject pgo1 = new TestObject();

            object[] selectedObjects=new object[]{pgo1};
            mogrePropertyGrid.SelectedObjects = selectedObjects;

        }
    }


}
