using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using Codenesium.DatabaseContracts;

namespace Codenesium.DatabaseForgeLib.UserControls
{
    public partial class UserControlSchemaEntry : MetroUserControl
    {
        public EventHandler<SchemaSavedEventArgs> SchemaSavedEvent;
        public Schema Schema { get; private set; } = new Schema();

        string _originalName = "";

        public UserControlSchemaEntry()
        {
            this.InitializeComponent();
            this.textBoxSchemaName.Focus();
        }

        public void LoadSchema(Schema schema)
        {
            this.Schema = schema;
            this._originalName = schema.Name;
            this.populateForm(schema);
            this.textBoxSchemaName.Select();
        }

        public void populateForm(Schema schema)
        {
            this.textBoxSchemaName.Text = schema.Name;
            this.textBoxSchemaName.Select();
        }

        private void save()
        {
            this.Schema.Name = this.textBoxSchemaName.Text;
            this.SchemaSavedEvent(this, new SchemaSavedEventArgs(this.Schema.Clone(), this._originalName));
            this.textBoxSchemaName.Clear();
        }

        private void textBoxSchemaName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.textBoxSchemaName.Text) && e.KeyChar == (char)Keys.Enter)
            {
                this.save();
                e.Handled = true;
            }
        }
    }
}
