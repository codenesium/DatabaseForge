using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Codenesium.DatabaseForgeLib.UserForms
{
    public partial class FormValueForm : MetroForm
    {
        public bool Saved { get; private set; }
        public  string Value { get; private set; }

        public FormValueForm(string title, string fieldName)
        {
            this.InitializeComponent();
            this.labelValue.Text = fieldName;
            this.Text = title;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.Value = this.textBoxValue.Text;
            this.Saved = true;
            this.Close();
        }
    }
}
