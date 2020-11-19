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
using MetroFramework.Forms;

namespace Codenesium.DatabaseForgeLib.UserForms
{
    public partial class FormForeignKeyCreator : MetroForm
    {
        Schema _schema;
        string _foreignColumn;
        string _foreignTable;
        public ForeignKey ForeignKey { get; private set; } = new ForeignKey();
        public DatabaseContracts.Constraint Constraint { get; private set; } = new DatabaseContracts.Constraint();

        public bool Saved { get; private set; }
        public FormForeignKeyCreator(Schema schema,string foreignTable,string foreignColumn)
        {
            this._schema = schema;
            this._foreignColumn = foreignColumn;
            this._foreignTable = foreignTable;
            this.InitializeComponent();
            this.comboBoxTable.DisplayMember = nameof(Table.Name);
            this.comboBoxColumn.DisplayMember = nameof(Column.Name);
            this.loadTables();
            this.selectParsedField();
        }

        public FormForeignKeyCreator(Schema schema, ForeignKey key)
        {
            this._schema = schema;
            if (key.Columns.First().ForeignKeyColumnName.ToUpper() == "TENANTID" && key.Columns.Count > 1)
            {
                this._foreignColumn = key.Columns[1].ForeignKeyColumnName;
                this._foreignTable = key.Columns[1].ForeignKeyTableName;
            }
            else
            {

                this._foreignTable = key.Columns.First().ForeignKeyTableName;
                this._foreignColumn = key.Columns.First().ForeignKeyColumnName;
            }

            Table table = schema.Tables.First(t => t.Name.ToUpper() == key.Columns.First().PrimaryKeyTableName.ToUpper());
            this.InitializeComponent();
            this.comboBoxTable.DisplayMember = nameof(Table.Name);
            this.comboBoxColumn.DisplayMember = nameof(Column.Name);
            this.loadTables();
            this.comboBoxTable.SelectedItem = table;
            this.comboBoxColumn.SelectedItem = table.Columns.First(c => c.Name.ToUpper() == key.Columns.First(kc => kc.PrimaryKeyColumnName.ToUpper() != "TENANTID").PrimaryKeyColumnName.ToUpper());
        }

        private void selectParsedField()
        {
            if(this._foreignColumn.ToUpper().EndsWith("ID"))
            {
                string possibleTableName = this._foreignColumn.Substring(0, this._foreignColumn.Length - 2);
                Table possibleTable = this._schema.Tables.FirstOrDefault(x => x.Name.ToUpper() == possibleTableName.ToUpper());
                if (possibleTable != null)
                {
                    this.comboBoxTable.SelectedItem = possibleTable;
                    this.comboBoxColumn.SelectedItem = possibleTable.Columns.FirstOrDefault(x => x.Name.ToUpper() == "ID");
                }
            }
        }

        private void loadTables()
        {
            this.comboBoxTable.Items.Clear();
            this._schema.Tables.OrderBy(x=> x.Name).ToList().ForEach(x =>
            {
                this.comboBoxTable.Items.Add(x);
            });
        }

        private void loadFields()
        {
            if (this.comboBoxTable.SelectedIndex > -1)
            {
                Table table = (Table)this.comboBoxTable.SelectedItem;
                this.comboBoxColumn.Items.Clear();
                table.Columns.OrderBy(x => x.Name).ToList().ForEach(x =>
                {
                    this.comboBoxColumn.Items.Add(x);
                });
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (this.comboBoxTable.SelectedIndex > -1 && this.comboBoxColumn.SelectedIndex > -1)
            {
                Column column = (Column)this.comboBoxColumn.SelectedItem;
                Table table = (Table)this.comboBoxTable.SelectedItem;


                ForeignKeyColumn fkColumn = new ForeignKeyColumn()
                {
                    ForeignKeyColumnName = this._foreignColumn,
                    ForeignKeyTableName = this._foreignTable,
                    PrimaryKeyColumnName = column.Name,
                    PrimaryKeyTableName = table.Name,
                    ForeignKeySchemaName = this._schema.Name,
                    PrimaryKeySchemaName = this._schema.Name
                };

                ForeignKeyColumn tenantColumn = new ForeignKeyColumn()
                {
                    ForeignKeyColumnName = "tenantId",
                    ForeignKeyTableName = this._foreignTable,
                    PrimaryKeyColumnName = "tenantId",
                    PrimaryKeyTableName = table.Name,
                    ForeignKeySchemaName = this._schema.Name,
                    PrimaryKeySchemaName = this._schema.Name
                };



                DatabaseContracts.Constraint contraint = new DatabaseContracts.Constraint()
                {
                    ConstraintType = "NONCLUSTERED",
                    Name = $"IX_{this._foreignTable}_{this._foreignColumn}",
                    SchemaName = this._schema.Name,
                    TableName = this._foreignTable,
                    IsPrimaryKey = false,
                    IsUnique = false,
                    Columns = new List<ConstraintColumn>()
                };

                bool isMultiTenant = table.Columns.Any(x => x.Name.ToUpper() == "TENANTID");
                if (isMultiTenant)
                {
                    contraint.Columns.Add(new ConstraintColumn()
                    {
                        Descending = false,
                        IsIncludedColumn = false,
                        Order = 0,
                        Name = "tenantId",
                        IsIdentity = false,
                    });
                }

                contraint.Columns.Add(new ConstraintColumn()
                {
                    Descending = false,
                    IsIncludedColumn = false,
                    Order = isMultiTenant ? 1 :0,
                    Name = this._foreignColumn,
                    IsIdentity = false,
                });


                this.Constraint = contraint;
                this.ForeignKey.ForeignKeyName = this.textBoxKeyName.Text;

                if (isMultiTenant)
                {
                    this.ForeignKey.Columns.Add(tenantColumn);
                }
                this.ForeignKey.Columns.Add(fkColumn);

                this.Saved = true;
            };
            this.Close();
        }

        private void comboBoxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.loadFields();
        }

        private void comboBoxField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.comboBoxColumn.SelectedIndex > -1)
            {
                Column field = (Column)this.comboBoxColumn.SelectedItem;
                Table table = (Table)this.comboBoxTable.SelectedItem;
                this.textBoxKeyName.Text = $"FK_{this._foreignTable}_{this._foreignColumn}_{table.Name}_{field.Name}";
            }
        }
    }
}
