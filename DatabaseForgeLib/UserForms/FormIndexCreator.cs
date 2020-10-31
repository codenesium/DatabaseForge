using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Codenesium.DatabaseContracts;

namespace Codenesium.DatabaseForgeLib.UserForms
{
    public partial class FormIndexCreator : MetroFramework.Forms.MetroForm
    {
        public bool Saved { get; private set; }
        public Table Table { get; private set; }
        string _schema;
        public FormIndexCreator(Table table, string schema)
        {
            this.Table = table;
            this._schema = schema;
            InitializeComponent();
            this.loadColumns();
            this.loadConstraints();
        }


        private void loadConstraints(DatabaseContracts.Constraint selection = null)
        {
            this.comboBoxConstraints.DataSource = null;
            this.comboBoxConstraints.DataSource = this.Table.Constraints;
            this.comboBoxConstraints.DisplayMember = "Name";

            if(selection != null)
            {
                this.comboBoxConstraints.SelectedItem = selection;
            }
        }

        private void loadColumns()
        {
            this.comboBoxColumns.DataSource = null;
            this.comboBoxColumns.DataSource = this.Table.Columns;
            this.comboBoxColumns.DisplayMember = "Name";

            if (this.comboBoxColumns.Items.Count > 0)
            {
                this.comboBoxColumns.SelectedIndex = 0;
            }
        }

        private void selectConstraint(Codenesium.DatabaseContracts.Constraint constraint)
        {
            this.listBoxConstraintColumns.DataSource = null;
            this.listBoxConstraintColumns.DataSource = constraint.Columns;
            this.listBoxConstraintColumns.DisplayMember = "Name";

            this.checkBoxIsUnique.Checked = constraint.IsUnique;
            this.checkBoxIsPrimaryKey.Checked = constraint.IsPrimaryKey;
            this.comboBoxConstraintType.SelectedItem = constraint.ConstraintType;
            if (listBoxConstraintColumns.Items.Count > 0)
            {
                listBoxConstraintColumns.SelectedIndex = 0;
            }
            else
            {
                listBoxConstraintColumns.SelectedIndex = -1;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxConstraints.SelectedIndex > -1)
            {
                Codenesium.DatabaseContracts.Constraint constraint = (Codenesium.DatabaseContracts.Constraint)comboBoxConstraints.SelectedItem;
                if (this.comboBoxColumns.SelectedIndex > -1)
                {
                    Column column = (Column)this.comboBoxColumns.SelectedItem;

                    if(constraint.Columns.Any(x => x.Name == column.Name))
                    {
                        return; // the column is already in the list
                    }

                    ConstraintColumn newColumn = new ConstraintColumn()
                    {
                        Name = column.Name,
                        Order = listBoxConstraintColumns.Items.Count + 1,
                        Descending = false,
                        IsIncludedColumn = false
                    };
                    constraint.Columns.Add(newColumn);
                }
                this.selectConstraint(constraint);
                listBoxConstraintColumns.SelectedIndex = -1;                
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(this.Table.Constraints.Any(c => c.Columns.Count == 0))
            {
                DatabaseContracts.Constraint constraint = this.Table.Constraints.First(c => c.Columns.Count == 0);
                MessageBox.Show($"Constraint {constraint.Name} does not have any columns and cannot be saved. You must add columns or delete it.");
                return;
            }
            this.Saved = true;
            this.Close();
        }

        private void comboBoxConstraints_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxConstraints.SelectedIndex > -1)
            {
                selectConstraint(comboBoxConstraints.SelectedItem as Codenesium.DatabaseContracts.Constraint);
            }
            else
            {
                listBoxConstraintColumns.DataSource = null;
            }
        }

        private void listBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBoxConstraintColumns.SelectedIndex > -1)
            {
                ConstraintColumn column = (ConstraintColumn)listBoxConstraintColumns.SelectedItem;
                checkBoxDescending.Checked = column.Descending;
                checkBoxIncludedColumn.Checked = column.IsIncludedColumn;
                checkBoxIsIdentity.Checked = column.IsIdentity;
            }
        }

        private void listBoxItems_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listBoxConstraintColumns.SelectedIndex > -1)
                {
                    int index = listBoxConstraintColumns.IndexFromPoint(e.Location);
                    if (index > -1)
                    {
                        listBoxConstraintColumns.SelectedIndex = index;
                        contextMenuStripColumns.Show(Cursor.Position);
                    }
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxConstraintColumns.SelectedIndex > -1)
            {
                Codenesium.DatabaseContracts.Constraint constraint = (Codenesium.DatabaseContracts.Constraint)comboBoxConstraints.SelectedItem;

                ConstraintColumn column = (ConstraintColumn)listBoxConstraintColumns.SelectedItem;

                constraint.Columns.Remove(column);
           
                selectConstraint(constraint);
            }
        }

        private void buttonAddConstraint_Click(object sender, EventArgs e)
        {
            FormValueForm valueForm = new FormValueForm("Constraint Creator", "Constraint Name");
            valueForm.ShowDialog();
            if(valueForm.Saved)
            {
                if(this.Table.Constraints.Any( x=> x.Name == valueForm.Value))
                {
                    MessageBox.Show($"A contraint with the name {valueForm.Value} already exists...");
                    return;
                }

                this.Table.Constraints.Add(new DatabaseContracts.Constraint()
                {
                    Name = valueForm.Value,
                    SchemaName = this._schema,
                    TableName = this.Table.Name,
                    ConstraintType = comboBoxConstraintType.Text
                });
                loadConstraints(this.Table.Constraints.Last());
            }
        }

        private void checkBoxIsUnique_CheckedChanged(object sender, EventArgs e)
        {
            if(comboBoxConstraints.SelectedIndex > -1)
            {
                Codenesium.DatabaseContracts.Constraint constraint = (Codenesium.DatabaseContracts.Constraint)comboBoxConstraints.SelectedItem;
                constraint.IsUnique = checkBoxIsUnique.Checked;
            }
        }

        private void checkBoxIsPrimaryKey_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBoxConstraints.SelectedIndex > -1)
            {
                Codenesium.DatabaseContracts.Constraint constraint = (Codenesium.DatabaseContracts.Constraint)comboBoxConstraints.SelectedItem;
                constraint.IsPrimaryKey = checkBoxIsPrimaryKey.Checked;
            }
        }

        private void checkBoxIsIdentity_CheckedChanged(object sender, EventArgs e)
        {
            if (comboBoxConstraints.SelectedIndex > -1 && listBoxConstraintColumns.SelectedIndex > -1)
            {
                Codenesium.DatabaseContracts.Constraint constraint = (Codenesium.DatabaseContracts.Constraint)comboBoxConstraints.SelectedItem;

                var column = constraint.Columns.First(x => x.Name.ToUpper() == ((ConstraintColumn)(listBoxConstraintColumns.SelectedValue)).Name.ToUpper());

                column.IsIdentity = checkBoxIsIdentity.Checked;
            }
        }

        private void comboBoxConstraints_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (comboBoxConstraints.SelectedIndex > -1)
                {
                     contextMenuStripConstraint.Show(Cursor.Position);
                }
            }
        }

        private void toolStripMenuItemDeleteConstraint_Click(object sender, EventArgs e)
        {
            if (comboBoxConstraints.SelectedIndex > -1)
            {
                this.Table.Constraints.Remove(comboBoxConstraints.SelectedItem as DatabaseContracts.Constraint);
                loadConstraints();
                clearControls();
            }
        }

        private void clearControls()
        {
            checkBoxDescending.Checked = false;
            checkBoxIsPrimaryKey.Checked = false;
            checkBoxIsUnique.Checked = false;
            checkBoxIsIdentity.Checked = false;
            comboBoxColumns.SelectedIndex = -1;
            checkBoxIncludedColumn.Checked = false;
            loadColumns();
        }

        private void comboBoxConstraintType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxConstraints.SelectedIndex > -1)
            {
                Codenesium.DatabaseContracts.Constraint constraint = (Codenesium.DatabaseContracts.Constraint)comboBoxConstraints.SelectedItem;
                constraint.ConstraintType = comboBoxConstraintType.Text;
            }
        }

        private void checkBoxDescending_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxConstraintColumns.SelectedIndex > -1)
            {
                Codenesium.DatabaseContracts.Constraint constraint = (Codenesium.DatabaseContracts.Constraint)comboBoxConstraints.SelectedItem;

                ConstraintColumn column = (ConstraintColumn)listBoxConstraintColumns.SelectedItem;

                column.Descending = checkBoxDescending.Checked;
            }
        }

        private void checkBoxIncludedColumn_CheckedChanged(object sender, EventArgs e)
        {
            if (listBoxConstraintColumns.SelectedIndex > -1)
            {
                Codenesium.DatabaseContracts.Constraint constraint = (Codenesium.DatabaseContracts.Constraint)comboBoxConstraints.SelectedItem;

                ConstraintColumn column = (ConstraintColumn)listBoxConstraintColumns.SelectedItem;

                column.IsIncludedColumn = checkBoxIncludedColumn.Checked;
            }
        }
    }
}
