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
            InitializeComponent();
            textBoxTableName.Focus();
        }

        public void LoadTable(Table table, string schemaName)
        {
            this.Table = table;
            this._schemaName = schemaName;
            this._originalName = table.Name;
            PopulateForm(table);
            this.textBoxTableName.Select();
        }

        private void PopulateForm(Table table)
        {
            this.textBoxTableName.Text = table.Name;
            this.textBoxTableName.Select();
        }
        private void Save()
        {
            this.Table.Name = textBoxTableName.Text;
            this.TableSavedEvent(this, new TableSavedEventArgs(this.Table, this._originalName));
            textBoxTableName.Clear();
        }

        private void textBoxTableName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxTableName.Text) &&  e.KeyChar == (char)Keys.Enter)
            {
                Save();
                e.Handled = true;
            }
        }

        private void UserControlTableEntry_VisibleChanged(object sender, EventArgs e)
        {
        }
    }
}
