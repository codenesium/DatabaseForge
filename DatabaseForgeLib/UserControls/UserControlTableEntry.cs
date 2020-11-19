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
    public partial class UserControlTableEntry : MetroUserControl
    {
        public EventHandler<TableSavedEventArgs> TableSavedEvent;
        public Table Table { get; private set; } = new Table();
        private string _schemaName;
        private string _originalName = string.Empty;
        public UserControlTableEntry()
        {
            this.InitializeComponent();
            this.textBoxTableName.Focus();
        }

        public void LoadTable(Table table, string schemaName)
        {
            this.Table = table;
            this._schemaName = schemaName;
            this._originalName = table.Name;
            this.PopulateForm(table);
            this.textBoxTableName.Select();
        }

        private void PopulateForm(Table table)
        {
            this.textBoxTableName.Text = table.Name;
            this.textBoxTableName.Select();
        }
        private void Save()
        {
            this.Table.Name = this.textBoxTableName.Text;
            this.TableSavedEvent(this, new TableSavedEventArgs(this.Table, this._originalName));
            this.textBoxTableName.Clear();
        }

        private void textBoxTableName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.textBoxTableName.Text) &&  e.KeyChar == (char)Keys.Enter)
            {
                this.Save();
                e.Handled = true;
            }
        }

        private void UserControlTableEntry_VisibleChanged(object sender, EventArgs e)
        {
        }
    }
}
