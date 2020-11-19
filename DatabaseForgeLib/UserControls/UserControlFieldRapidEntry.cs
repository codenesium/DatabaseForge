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
using Codenesium.DataConversionExtensions;
using Codenesium.DatabaseContracts;

namespace Codenesium.DatabaseForgeLib.UserControls
{
    public partial class UserControlFieldRapidEntry : MetroUserControl
    {
        public EventHandler<ColumnSavedEventArgs> ColumnSavedEvent;
        public EventHandler<ForeignKeySavedEventArgs> ForeignKeySavedEvent;
        public EventHandler<ConstraintSavedEventArgs> ConstraintSavedEvent;
        private string originalName = string.Empty;
        public Column Column { get; private set; } = new Column();

        public Table Table { get; private set; }

        public Schema Schema { get; set; }

        private string _provider;

        public UserControlFieldRapidEntry(string provider)
        {
            this.InitializeComponent();
            this.SetProvider(provider);
        }

        protected override void OnEnter(EventArgs e)
        {
            this.textBoxFieldName.Select();
            base.OnEnter(e); // this will raise the Enter event
        }


        public void SetProvider(string provider)
        {
            this._provider = provider;
            this.loadControls();
        }
        private void loadControls()
        {
            void loadTypes()
            {
                this.comboBoxFieldType.Items.Clear();
                IDatabaseInterface sqlInterface = DatabaseInterfaceFactory.Factory(this._provider);
                foreach (string item in sqlInterface.ColumnTypes)
                {
                    this.comboBoxFieldType.Items.Add(item);
                }
            }


            if (this.comboBoxFieldType.InvokeRequired)
            {
                this.comboBoxFieldType.Invoke(new MethodInvoker(delegate
                {
                    loadTypes();
                }));
            }
            else
            {
                loadTypes();
            }


            if (this.textBoxFieldName.InvokeRequired)
            {
                this.textBoxFieldName.Invoke(new MethodInvoker(delegate
                {
                    this.textBoxFieldName.Select();
                }));
            }
            else
            {
                this.textBoxFieldName.Select();
            }     
        }

        public void LoadForm(Schema schema,Table table)
        {
            this.Column = new Column();
            this.Schema = schema;
            this.Table = table;
            this.clearFields();
            this.textBoxFieldName.Select();
        }

        public void LoadForm()
        {
            this.clearFields();
            this.textBoxFieldName.Select();
 
        }

        public void LoadForm(Schema schema, Table table, Column column)
        {
            this.Column = column;
            this.Table = table;
            this.Schema = schema;
            this.textBoxFieldName.Text = this.Column.Name;
            this.comboBoxFieldType.SelectedItem = this.Column.DataType;
            this.textBoxMaxLength.Text = this.Column.MaxLength.ToString();
            this.checkBoxNullable.Checked = this.Column.IsNullable;
            this.checkBoxDatabaseGenerated.Checked = this.Column.DatabaseGenerated;
            this.textBoxFieldName.Select();
            this.originalName = column.Name;
        }

        private void clearFields()
        {
            this.originalName = string.Empty;
            this.textBoxFieldName.Clear();
            this.comboBoxFieldType.SelectedIndex = -1;
            this.textBoxMaxLength.Clear();
            this.checkBoxNullable.Checked = false; 
            this.checkBoxDatabaseGenerated.Checked = false;
        }

        private void save()
        {
            if(this.comboBoxFieldType.SelectedIndex == -1)
            {
                this.comboBoxFieldType.SelectedItem = "varchar";
                this.textBoxMaxLength.Text = "128";
            }

            this.Column.Name = this.textBoxFieldName.Text;
            this.Column.DataType =  this.comboBoxFieldType.SelectedItem.ToString();
            this.Column.MaxLength = this.textBoxMaxLength.Text.ToInt();
            this.Column.IsNullable = this.checkBoxNullable.Checked;
            this.Column.DatabaseGenerated = this.checkBoxDatabaseGenerated.Checked;
            this.setPrimaryKey(this.Column.Name);
            this.setForeignKey(this.Column.Name, this.Table.Name);
            this.ColumnSavedEvent(this, new ColumnSavedEventArgs(this.Column.Clone(), this.originalName));
            this.clearFields();
            this.textBoxFieldName.Select();
        }

        private void textBoxFieldName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.textBoxFieldName.Text) && e.KeyChar == (char)Keys.Enter)
            {
                this.save();
                e.Handled = true;
            }
        }

        private void setPrimaryKey(string columnName)
        {
            if (columnName.ToUpper() == "ID")
            {
                DatabaseContracts.Constraint primaryKey = new DatabaseContracts.Constraint();
                primaryKey.Columns.Add(new ConstraintColumn()
                {
                    Name = columnName,
                    Order = 0,
                    IsIdentity = true
                });
                primaryKey.Name = "PK_" + this.Table.Name;
                primaryKey.SchemaName = this.Schema.Name;
                primaryKey.TableName = this.Table.Name;
                primaryKey.IsPrimaryKey = true;
                primaryKey.IsUnique = true;
                this.ConstraintSavedEvent(this, new ConstraintSavedEventArgs(primaryKey));
            }
        }
        private void setForeignKey(string columnName, string tableName)
        {
            if (columnName.ToUpper().EndsWith("ID"))
            {
                string primaryKeyTableName = columnName.ToUpper().Remove(columnName.Length - 2, 2);

                if (this.Schema.Tables.Any(x => x.Name.ToUpper() == primaryKeyTableName))
                {
                    Table primaryKeyTable = this.Schema.Tables.First(x => x.Name.ToUpper() == primaryKeyTableName.ToUpper());
                    DatabaseContracts.Constraint primaryKey = primaryKeyTable.Constraints.FirstOrDefault(x => x.IsPrimaryKey);


                    if (primaryKey != null)
                    {
                        ForeignKey foreignKey = new ForeignKey()
                        {
                            ForeignKeyName = ($"FK_{tableName}_{columnName}_{primaryKeyTableName}_{primaryKey.Columns.First().Name}").ToLower(),
                            Columns = new List<ForeignKeyColumn>()
                            {
                                new ForeignKeyColumn()
                                {
                                    ForeignKeyColumnName = columnName,
                                    ForeignKeySchemaName = this.Schema.Name,
                                    ForeignKeyTableName = this.Table.Name,
                                    Order = 0,
                                    PrimaryKeyColumnName = primaryKey.Columns.First().Name,
                                    PrimaryKeySchemaName = this.Schema.Name,
                                    PrimaryKeyTableName = primaryKeyTable.Name
                                }
                            }
                        };


                        DatabaseContracts.Constraint contraint = new DatabaseContracts.Constraint()
                        {
                            ConstraintType = "NONCLUSTERED",
                            Name = $"IX_{tableName}_{columnName}",
                            SchemaName = this.Schema.Name,
                            TableName = tableName,
                            IsPrimaryKey = false,
                            IsUnique = false,
                            Columns = new List<ConstraintColumn>()
                            {
                                new ConstraintColumn()
                                {
                                    Descending = false,
                                    IsIncludedColumn = false,
                                    Order = 0,
                                    Name = columnName,
                                    IsIdentity = false,
                                }
                            }
                        };
                        this.ForeignKeySavedEvent(this, new ForeignKeySavedEventArgs(foreignKey, contraint));
                    }
                }
            }
        }

        private void textBoxFieldName_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void textBoxFieldName_TextChanged(object sender, EventArgs e)
        {
            if (this.comboBoxFieldType.SelectedIndex == -1)
            {
                if (this.textBoxFieldName.Text.ToUpper() == "ID" || this.textBoxFieldName.Text.ToUpper().EndsWith("ID"))
                {

                    this.comboBoxFieldType.SelectedItem = "int";
                    this.textBoxMaxLength.Text = "";
                    if (this.textBoxFieldName.Text.ToUpper() == "ID")
                    {
                        this.checkBoxDatabaseGenerated.Checked = true;
                    }
                }
                else if (this.textBoxFieldName.Text.ToUpper() == "EXTERNALID" || this.textBoxFieldName.Text.ToUpper() == "ROWGUID")
                {
                    this.comboBoxFieldType.SelectedItem = "uniqueidentifier";
                    this.textBoxMaxLength.Text = "";
                }
                else if (this.textBoxFieldName.Text.ToUpper() == "NAME")
                {
                    this.comboBoxFieldType.SelectedItem = "varchar";
                    this.textBoxMaxLength.Text = "128";
                }
                else if (this.textBoxFieldName.Text.ToUpper().Contains("DATE"))
                {
                    this.comboBoxFieldType.SelectedItem = "datetime";
                    this.textBoxMaxLength.Text = "";
                }
            }
        }

        private void comboBoxFieldType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.save();
                e.Handled = true;
            }

        }

        private void textBoxMaxLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.save();
                e.Handled = true;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.save();
        }
    }
} 
