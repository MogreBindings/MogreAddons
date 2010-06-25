using System;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoWrap.Meta;

namespace AutoWrap
{
    public partial class Form1 : Form
    {
        Wrapper _wrapper;

        public Form1()
        {
            InitializeComponent();

            Globals.NativeNamespace = "OIS";
            Globals.ManagedNamespace = "MOIS";

            MetaDefinition meta = new MetaDefinition("../../../cpp2java/build/meta.xml", "MOIS");
            meta.AddAttributes("../../Attributes.xml");

            _wrapper = new Wrapper(meta, "../../../../include/auto", "../../../../src/auto", "MOIS", "OIS");

            for (int i = 0; i < _wrapper.IncludeFiles.Count; i++)
            {
                lstTypes.Items.Add(_wrapper.IncludeFiles.Keys[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bar.Visible = true;
            _wrapper.ProduceCodeFiles(bar);
            MessageBox.Show("OK");
            bar.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txt.Text = _wrapper.CreateIncludeCodeForIncludeFile(lstTypes.SelectedItem.ToString()).Replace("\n","\r\n");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool hasContent;
            txt.Text = _wrapper.CreateCppCodeForIncludeFile(lstTypes.SelectedItem.ToString(), out hasContent).Replace("\n", "\r\n");
        }

        private void lstTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt.Text = _wrapper.CreateIncludeCodeForIncludeFile(lstTypes.SelectedItem.ToString()).Replace("\n", "\r\n");
        }
    }
}