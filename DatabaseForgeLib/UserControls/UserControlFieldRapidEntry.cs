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
        private bool _loading;
        private string _originalName = string.Empty;
        public Column Column { get; private set; } = new Column();

        public Table Table { get; private set; }

        public Schema Schema { get; set; }

        private string _provider;

        public UserControlFieldRapidEntry(string provider)
        {
            InitializeComponent();
            this._loading = true;
            this.SetProvider(provider);
            this._loading = false;
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
            Action loadTypes = () =>
            {
                comboBoxFieldType.Items.Clear();
                IDatabaseInterface sqlInterface = DatabaseInterfaceFactory.Factory(this._provider);
                foreach (var item in sqlInterface.ColumnTypes)
                {
                    comboBoxFieldType.Items.Add(item);
                }
            };


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
                    textBoxFieldName.Select();
                }));
            }
            else
            {
                textBoxFieldName.Select();
            }     
        }

        public void LoadForm(Schema schema,Table table)
        {
            this._loading = true;
            this.Column = new Column();
            this.Schema = schema;
            this.Table = table;
            this.clearFields();
            this.textBoxFieldName.Select();
            this._loading = false;
        }

        public void LoadForm()
        {
            this._loading = true;
            this.clearFields();
            this.textBoxFieldName.Select();
            this._loading = false;
        }

        public void LoadForm(Schema schema, Table table, Column column)
        {
            this._loading = true;
            this.Column = column;
            this.Table = table;
            this.Schema = schema;
            this.textBoxFieldName.Text = this.Column.Name;
            this.comboBoxFieldType.SelectedItem = this.Column.DataType;
            this.textBoxMaxLength.Text = this.Column.MaxLength.ToString();
            this.checkBoxNullable.Checked = this.Column.IsNullable;
            this.checkBoxDatabaseGenerated.Checked = this.Column.DatabaseGenerated;
            this.textBoxFieldName.Select();
            this._originalName = column.Name;
            this._loading = false;
        }

        private void clearFields()
        {
            this._originalName = string.Empty;
            this.textBoxFieldName.Clear();
            this.comboBoxFieldType.SelectedIndex = -1;
            this.textBoxMaxLength.Clear();
            this.checkBoxNullable.Checked = false; 
            this.checkBoxDatabaseGenerated.Checked = false;
        }

        private void save()
        {
            if(comboBoxFieldType.SelectedIndex == -1)
            {
                comboBoxFieldType.SelectedItem = "varchar";
                textBoxMaxLength.Text = "128";
            }

            this.Column.Name = this.textBoxFieldName.Text;
            this.Column.DataType =  this.comboBoxFieldType.SelectedItem.ToString();
            this.Column.MaxLength = this.textBoxMaxLength.Text.ToInt();
            this.Column.IsNullable = this.checkBoxNullable.Checked;
            this.Column.DatabaseGenerated = checkBoxDatabaseGenerated.Checked;
            this.setPrimaryKey(this.Column.Name);
            this.setForeignKey(this.Column.Name, this.Table.Name);
            this.ColumnSavedEvent(this, new ColumnSavedEventArgs(this.Column.Clone(), this._originalName));
            this.clearFields();
            textBoxFieldName.Select();
        }

        private void textBoxFieldName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(textBoxFieldName.Text) && e.KeyChar == (char)Keys.Enter)
            {
                save();
                e.Handled = true;
            }
        }

        private void setPrimaryKey(string columnName)
        {
            if (columnName.ToUpper() == "ID")
            {
                var primaryKey = new DatabaseContracts.Constraint();
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
                        var foreignKey = new ForeignKey()
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
            if (comboBoxFieldType.SelectedIndex == -1)
            {
                if (textBoxFieldName.Text.ToUpper() == "ID" || textBoxFieldName.Text.ToUpper().EndsWith("ID"))
                {

                    comboBoxFieldType.SelectedItem = "int";
                    textBoxMaxLength.Text = "";
                    if (textBoxFieldName.Text.ToUpper() == "ID")
                    {
                        checkBoxDatabaseGenerated.Checked = true;
                    }
                }
                else if (textBoxFieldName.Text.ToUpper() == "EXTERNALID" || textBoxFieldName.Text.ToUpper() == "ROWGUID")
                {
                    comboBoxFieldType.SelectedItem = "uniqueidentifier";
                    textBoxMaxLength.Text = "";
                }
                else if (textBoxFieldName.Text.ToUpper() == "NAME")
                {
                    comboBoxFieldType.SelectedItem = "varchar";
                    textBoxMaxLength.Text = "128";
                }
                else if (textBoxFieldName.Text.ToUpper().Contains("DATE"))
                {
                    comboBoxFieldType.SelectedItem = "datetime";
                    textBoxMaxLength.Text = "";
                }
            }
        }

        private void comboBoxFieldType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                save();
                e.Handled = true;
            }

        }

        private void textBoxMaxLength_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                save();
                e.Handled = true;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            save();
        }
    }
} 
