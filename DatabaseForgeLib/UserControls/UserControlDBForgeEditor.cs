using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using System.IO;
using Codenesium.DatabaseForgeLib.UserForms;
using Codenesium.DatabaseContracts;
using Codenesium.DatabaseContracts.DependencyResolver;
using Codenesium.DatabaseContracts.Display;
using System.Reflection;

namespace Codenesium.DatabaseForgeLib.UserControls
{
    public partial class UserControlDBForgeEditor : AbstractUserControl
    {
        private UserControlFieldRapidEntry _userControlFieldRapidEntry;
        private UserControlTableEntry _userControlTableEntry;
        private UserControlSchemaEntry _userControlSchemaEntry;
        private UserControlGenerate _userControlGenerate;

        public EventHandler BackSelectedEvent;
        public EventHandler<SchemaEditingCompleteEventArgs> SchemaEditingCompleted;
        private DatabaseContainer _database;
        private ForgeSettings _settings;

        public UserControlDBForgeEditor(ForgeSettings settings, DatabaseContainer database)
        {
            this._settings = settings;
            if (database == null)
            {
                this._database = new DatabaseContainer("", DatabaseContainer.DatabaseTypeMSSQL);
            }
            else
            {
                this._database = database;
            }
            this.InitializeComponent();
            this.initializeControls();
            this.loadSchemas(this._database);
        }

        private void initializeControls()
        {
            this.listBoxColumns.DisplayMember = "Name";
            this.listBoxTables.DisplayMember = "Name";
            this.listBoxSchemas.DisplayMember = "Name";

            this.buttonCompleteSchemaEditing.Visible = this._settings.CodenesiumMode;
            this.buttonCancelSchemaEditing.Visible = this._settings.CodenesiumMode;
            this._userControlGenerate = new UserControlGenerate(this._settings);
            this._userControlFieldRapidEntry = new UserControlFieldRapidEntry(this._database.DatabaseType);
            this._userControlSchemaEntry = new UserControlSchemaEntry();
            this._userControlSchemaEntry.SchemaSavedEvent += (object sender, SchemaSavedEventArgs e) =>
            {
                this._database.AddSchema(e.Schema, e.OriginalName);
                this.loadSchemas(this._database);
            };
            this._userControlFieldRapidEntry.LoadForm();

            this._userControlFieldRapidEntry.ColumnSavedEvent += (object sender, ColumnSavedEventArgs e) =>
            {
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                Table table = (Table)this.listBoxTables.SelectedItem;

                Column existingField = table.Columns.FirstOrDefault(x => x.Name.ToUpper() == e.OriginalName.ToUpper());
                schema.AddColumn(table.Name, e.Column, e.OriginalName);

                if (existingField == null)
                {
                    this.tableSelectionChanged(false, true);
                }
                else
                {
                    this.tableSelectionChanged(true, false);
                }
                this.loadColumns(table.Columns);
            };
            this._userControlFieldRapidEntry.ConstraintSavedEvent += (object sender, ConstraintSavedEventArgs e) =>
            {
                Table table = (Table)this.listBoxTables.SelectedItem;
                table.AddConstraint(e.Constraint);
            };
            this._userControlFieldRapidEntry.ForeignKeySavedEvent += (object sender, ForeignKeySavedEventArgs e) =>
            {
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                Table table = (Table)this.listBoxTables.SelectedItem;
                schema.AddForeignKey(e.Key);
                table.AddConstraint(e.Constraint);
            };

            this._userControlTableEntry = new UserControlTableEntry();
            this._userControlTableEntry.TableSavedEvent += (object sender, TableSavedEventArgs e) =>
            {
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                schema.AddTable(e.Table.Clone(), e.OriginalName);
                this.loadTables(schema);
            };
        }

        private void loadTables(Schema schema)
        {
            this.populateForm(schema.Tables);
        }

        private void populateForm(List<Table> tables, bool selectFirstTable = true)
        {
            this.listBoxColumns.DataSource = null;
            this.listBoxTables.DataSource = null;
            this.listBoxTables.DataSource = tables.OrderBy(x => x.Name).ToList();
            this.listBoxTables.DisplayMember = "Name";

            if (selectFirstTable)
            {
                if (this.listBoxTables.Items.Count > 0)
                {
                    this.listBoxTables.SelectedIndex = 0;
                }
            }
        }

        private void setPanelUserControl(UserControl control)
        {
            Action addcontrol = () =>
            {
                this.panelContainer.Controls.Clear();
                this.panelContainer.Controls.Add(control);
                control.Focus();
            };
            if (this.panelContainer.InvokeRequired)
            {
                this.panelContainer.Invoke(new MethodInvoker(delegate
                {
                    addcontrol();
                }));
            }
            else
            {
                addcontrol();
            }
        }

        private void listBoxTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.tableSelectionChanged();
        }

        private void tableSelectionChanged(bool preserveFieldSelection = false, bool selectLast = false)
        {
            int currentIndex = this.listBoxColumns.SelectedIndex;
            if (this.listBoxTables.SelectedItem != null)
            {
                Table table = (Table)this.listBoxTables.SelectedItem;
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                this.loadColumns(table.Columns);
            }

            if (preserveFieldSelection && currentIndex < this.listBoxColumns.Items.Count)
            {
                this.listBoxColumns.SelectedIndex = currentIndex;
            }

            if (selectLast)
            {
                this.listBoxColumns.SelectedIndex = this.listBoxColumns.Items.Count - 1;
            }
        }

        private void loadColumns(List<Column> columns)
        {
            Table table = (Table)this.listBoxTables.SelectedItem;
            Schema schema = (Schema)this.listBoxSchemas.SelectedItem;

            this._userControlFieldRapidEntry.LoadForm();

            this.listBoxColumns.DataSource = null;
            this.listBoxColumns.DataSource = columns.OrderBy(x => x.Name).ToList().ToUIColumns(table, schema);
            this.listBoxColumns.DisplayMember = "DisplayName";

            if (this.listBoxColumns.Items.Count > 0)
            {
                this.listBoxColumns.SelectedIndex = 0;
            }
        }

        private void loadSchemas(DatabaseContainer database)
        {
            Action loadSchemas = () =>
            {
                this.listBoxSchemas.DataSource = null;
                this.listBoxSchemas.DataSource = database.Schemas;
                this.listBoxSchemas.DisplayMember = "Name";
                if (this.listBoxSchemas.Items.Count > 0)
                {
                    this.listBoxSchemas.SelectedIndex = 0;
                }
            };

            if (this.listBoxSchemas.InvokeRequired)
            {
                this.listBoxSchemas.Invoke(new MethodInvoker(delegate
                {
                    loadSchemas();
                }));
            }
            else
            {
                loadSchemas();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this?.BackSelectedEvent(this, new EventArgs());
        }

        private void textBoxTableSearch_TextChanged(object sender, EventArgs e)
        {
            if (this.listBoxSchemas.SelectedIndex > -1)
            {
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                this.filterTables(schema, this.textBoxTableSearch.Text);
                this.textBoxTableSearch.Focus();
            }
        }

        private void filterTables(Schema schema, string query)
        {
            List<Table> filteredList = schema.Tables.Where(x => x.Name.ToUpper().StartsWith(query.ToUpper())).ToList();
            this.populateForm(filteredList, false);
        }

        private void listBoxFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            Table table = (Table)this.listBoxTables.SelectedItem;
            Schema schema = (Schema)this.listBoxSchemas.SelectedItem;

            if (this.listBoxColumns.SelectedItem != null)
            {
                Column column = (Column)this.listBoxColumns.SelectedItem;
                this._userControlFieldRapidEntry.LoadForm(schema.Clone(), table.Clone(), column.Clone());
                this.setPanelUserControl(this._userControlFieldRapidEntry);
            }
            else if (table == null)
            {
                this._userControlFieldRapidEntry.LoadForm();
            }
            else
            {
                this._userControlFieldRapidEntry.LoadForm(schema.Clone(), table.Clone());
            }
        }

        private void buttonAddFields_Click(object sender, EventArgs e)
        {
            if (this.listBoxTables.SelectedIndex > -1)
            {
                Table table = (Table)this.listBoxTables.SelectedItem;
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                this.listBoxColumns.SelectedIndex = -1;
                this._userControlFieldRapidEntry.LoadForm(schema, table);
                this.setPanelUserControl(this._userControlFieldRapidEntry);
            }
        }

        private void buttonAddTable_Click(object sender, EventArgs e)
        {
            if (this.listBoxSchemas.SelectedIndex > -1)
            {
                this.listBoxTables.SelectedIndex = -1;
                this.listBoxColumns.SelectedIndex = -1;

                this.setPanelUserControl(this._userControlTableEntry);
            }
        }

        private void buttonActions_Click(object sender, EventArgs e)
        {
        }

        private void buttonAddSchema_Click(object sender, EventArgs e)
        {
            this.setPanelUserControl(this._userControlSchemaEntry);
        }

        private void listBoxSchemas_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectSchema();
        }

        private void SelectSchema()
        {
            if (this.listBoxSchemas.SelectedIndex > -1)
            {
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                this.loadTables(schema);
            }
        }

        private void listBoxTables_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.listBoxTables.SelectedIndex > -1)
                {
                    int index = this.listBoxTables.IndexFromPoint(e.Location);
                    if (index > -1)
                    {
                        this.listBoxTables.SelectedIndex = index;
                        this.contextMenuStripTable.Show(Cursor.Position);
                    }
                }
            }
        }

        private void listBoxFields_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.listBoxColumns.SelectedIndex > -1)
                {
                    int index = this.listBoxColumns.IndexFromPoint(e.Location);
                    if (index > -1)
                    {
                        this.listBoxColumns.SelectedIndex = index;
                        Column column = (Column)this.listBoxColumns.Items[index];

                        Table table = (Table)this.listBoxTables.SelectedItem;
                        Schema schema = (Schema)this.listBoxSchemas.SelectedItem;

                        if (table.Constraints.Any(x => x.IsPrimaryKey && x.TableName == table.Name))
                        {
                            this.contextMenuAddPrimaryKey.Text = "Delete Primary Key";
                        }
                        else
                        {
                            this.contextMenuAddPrimaryKey.Text = "Add Primary Key";
                        }
                        this.contextMenuAddPrimaryKey.Show(Cursor.Position);
                    }
                }
            }
        }

        private void createPrimaryKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBoxColumns.SelectedIndex > -1)
            {
                UIColumn column = (UIColumn)this.listBoxColumns.SelectedItem;
                Table table = (Table)this.listBoxTables.SelectedItem;
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                DatabaseContracts.Constraint primaryKey = new DatabaseContracts.Constraint()
                {
                    Name = $"PK_{table.Name}",
                    SchemaName = schema.Name,
                    TableName = table.Name,
                    IsPrimaryKey = true,
                    IsUnique = true,
                    ConstraintType = "CLUSTERED",
                    Columns = new List<ConstraintColumn>()
                };

                bool isMultiTenant = table.Columns.Any(x => x.Name.ToUpper() == "TENANTID");
                if (isMultiTenant)
                {
                    primaryKey.Columns.Add(new ConstraintColumn()
                    {
                        Name = "tenantId",
                        Order = 0,
                        Descending = false,
                        IsIncludedColumn = false
                    });
                }

                primaryKey.Columns.Add(new ConstraintColumn()
                {
                    Name = column.Name,
                    Order = isMultiTenant ? 1 : 0,
                    Descending = false,
                    IsIncludedColumn = false
                });

                table.AddConstraint(primaryKey);
                this.loadColumns(table.Columns);
            }
        }

        private void deleteFieldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DeleteColumn();
        }

        private void deleteFieldToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.DeleteColumn();
        }

        private void DeleteColumn()
        {
            if (this.listBoxColumns.SelectedIndex > -1)
            {
                Column column = (Column)this.listBoxColumns.SelectedItem;
                Table table = (Table)this.listBoxTables.SelectedItem;
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;

                schema.DeleteColumn(table.Name, column.Name);

                this.loadColumns(table.Columns);
            }
        }

        private void contextMenuStripRemoveForeignKey_Opening(object sender, CancelEventArgs e)
        {
        }

        public async Task SaveProject()
        {
            this.saveFileDialogProject.FileName = "Project.json";
            if (this.saveFileDialogProject.ShowDialog() == DialogResult.OK)
            {
                if (!String.IsNullOrWhiteSpace(this.saveFileDialogProject.FileName))
                {
                    await Task.Run(() =>
                    {
                        string project = JsonConvert.SerializeObject(this._database, Formatting.Indented);
                        File.WriteAllText(this.saveFileDialogProject.FileName, project);
                    });
                }
            }
        }

        public async Task LoadProject()
        {
            this.saveFileDialogProject.FileName = "Project.json";
            if (this.openFileDialogProject.ShowDialog() == DialogResult.OK)
            {
                if (!String.IsNullOrWhiteSpace(this.openFileDialogProject.FileName))
                {
                    await Task.Run(() =>
                    {
                        string contents = File.ReadAllText(this.openFileDialogProject.FileName);
                        this._database = JsonConvert.DeserializeObject<DatabaseContainer>(contents);
                        this._userControlFieldRapidEntry.SetProvider(this._database.DatabaseType);
                        this.loadSchemas(this._database);
                    });
                }
            }
        }

        public void LoadProject(DatabaseContainer container)
        {
            this.setPanelUserControl(this._userControlSchemaEntry);
            this._database = container;
            this.listBoxColumns.DataSource = null;
            this.listBoxTables.DataSource = null;
            this.listBoxSchemas.DataSource = null;
            this._userControlFieldRapidEntry.SetProvider(this._database.DatabaseType);
            this.loadSchemas(this._database);
        }

        private void createForeignKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBoxColumns.SelectedIndex > -1)
            {
                Column column = (Column)this.listBoxColumns.SelectedItem;
                Table table = (Table)this.listBoxTables.SelectedItem;
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;

                FormForeignKeyCreator keyCreator = new FormForeignKeyCreator(schema, table.Name, column.Name);
                keyCreator.ShowDialog();
                if (keyCreator.Saved)
                {
                    schema.AddForeignKey(keyCreator.ForeignKey);
                    table.AddConstraint(keyCreator.Constraint);
                }
                this.loadColumns(table.Columns);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBoxTables.SelectedIndex > -1)
            {
                Table table = (Table)this.listBoxTables.SelectedItem;
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                this._database.DeleteTable(schema.Name, table.Name);
                this.loadTables(schema);
            }
        }

        private void deleteForeignKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBoxColumns.SelectedIndex > -1)
            {
                Column column = (Column)this.listBoxColumns.SelectedItem;
                Table table = (Table)this.listBoxTables.SelectedItem;
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;

                schema.DeleteForeignKey(table.Name, column.Name);

                this.loadColumns(table.Columns);
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
        }

        private void buttonCompleteSchemaEditing_Click(object sender, EventArgs e)
        {
            SchemaEditingCompleteEventArgs args = new SchemaEditingCompleteEventArgs(this._database);
            this?.SchemaEditingCompleted(this, args);
        }

        private void buttonCancelSchemaEditing_Click(object sender, EventArgs e)
        {
            SchemaEditingCompleteEventArgs args = new SchemaEditingCompleteEventArgs(null);
            this?.SchemaEditingCompleted(this, args);
        }

        private void buttonBuildDependencies_Click(object sender, EventArgs e)
        {
        }

        private void addIndexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBoxTables.SelectedIndex > -1)
            {
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                Table table = (Table)this.listBoxTables.SelectedItem;
                FormIndexCreator formIndexCreator = new FormIndexCreator(table.Clone(), schema.Name);
                formIndexCreator.ShowDialog();
                if (formIndexCreator.Saved)
                {
                    int schemaIndex = this._database.Schemas.FindIndex(s => s.Name == schema.Name);
                    int tableIndex = this._database.Schemas[schemaIndex].Tables.FindIndex(t => t.Name == table.Name);

                    this._database.Schemas[schemaIndex].Tables[tableIndex] = formIndexCreator.Table.Clone();
                    this.loadTables(this._database.Schemas[schemaIndex]);
                }
            }
        }

        private void contextMenuAddPrimaryKey_Opening(object sender, CancelEventArgs e)
        {
            if (this.listBoxColumns.SelectedIndex > -1)
            {
                UIColumn column = (UIColumn)this.listBoxColumns.SelectedItem;
                Table table = (Table)this.listBoxTables.SelectedItem;

                if (column.IsForeignKey)
                {
                    this.createForeignKeyToolStripMenuItem.Visible = false;
                    this.editForeignKeyToolStripMenuItem.Visible = true;
                    this.deleteForeignKeyToolStripMenuItem.Visible = true;
                }
                else
                {
                    this.createForeignKeyToolStripMenuItem.Visible = true;
                    this.editForeignKeyToolStripMenuItem.Visible = false;
                    this.deleteForeignKeyToolStripMenuItem.Visible = false;
                }

                this.createPrimaryKeyToolStripMenuItem.Visible = false;
                this.deletePrimaryKeyToolStripMenuItem.Visible = false;
                if (column.IsPrimaryKey)
                {
                    this.createPrimaryKeyToolStripMenuItem.Visible = false;
                    this.deletePrimaryKeyToolStripMenuItem.Visible = true;
                }
                else
                {
                    if (!table.Constraints.Any(x => x.IsPrimaryKey))
                    {
                        this.createPrimaryKeyToolStripMenuItem.Visible = true;
                    }
                }
            }
        }

        private void deletePrimaryKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBoxColumns.SelectedIndex > -1)
            {
                Table table = (Table)this.listBoxTables.SelectedItem;
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                this._database.DeletePrimaryKey(schema.Name, table.Name);
                this.loadColumns(table.Columns);
            }
        }

        private void editForeignKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBoxColumns.SelectedIndex > -1)
            {
                Column column = (Column)this.listBoxColumns.SelectedItem;
                Table table = (Table)this.listBoxTables.SelectedItem;
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;

                ForeignKey foreignKey = schema.ForeignKeys.FirstOrDefault(x => x.Columns.Any(c =>
                c.ForeignKeyColumnName.ToUpper() == column.Name.ToUpper() &&
                c.ForeignKeyTableName.ToUpper() == table.Name.ToUpper() &&
                c.ForeignKeySchemaName.ToUpper() == schema.Name.ToUpper()
                ));

                if (foreignKey != null)
                {
                    FormForeignKeyCreator keyCreator = new FormForeignKeyCreator(schema, foreignKey);
                    keyCreator.ShowDialog();
                    if (keyCreator.Saved)
                    {
                        schema.DeleteForeignKey(table.Name, column.Name);
                        schema.AddForeignKey(keyCreator.ForeignKey);
                    }
                    this.loadColumns(table.Columns);
                }
            }
        }

        private void geenrateSQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void buildDependencyTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._userControlGenerate.BuildDependencyTree(this._database);
            this.setPanelUserControl(this._userControlGenerate);
        }

        private void buttonTools_Click(object sender, EventArgs e)
        {
        }

        private void buttonTools_MouseDown(object sender, MouseEventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            this.contextMenuStripTools.Show(ptLowerLeft);
        }

        private void listBoxSchemas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.listBoxSchemas.SelectedIndex > -1)
                {
                    int index = this.listBoxSchemas.IndexFromPoint(e.Location);
                    if (index > -1)
                    {
                        this.listBoxSchemas.SelectedIndex = index;
                        this.contextMenuStripSchema.Show(Cursor.Position);
                    }
                }
            }
        }

        private void toolStripMenuDeleteSchema_Click(object sender, EventArgs e)
        {
            if (this.listBoxSchemas.SelectedIndex > -1)
            {
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                this._database.DeleteSchema(schema.Name);
                this.loadSchemas(this._database);
            }
        }

        private void generateTestDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._userControlGenerate.GenerateInsertStatmentsForDatabase(this._database);
            this.setPanelUserControl(this._userControlGenerate);
        }

        private void webReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string resources = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources");
                string index = Path.Combine(resources, "index.html");
                string reportsDirectory = Path.Combine(resources, "reports");

                Directory.CreateDirectory(reportsDirectory);

                if (!File.Exists(index))
                {
                    MessageBox.Show("Unable to find index.html");
                }
                else
                {
                    string template = File.ReadAllText(index);
                    string schema = JsonConvert.SerializeObject(this._database,
                        Formatting.None,
                          new Newtonsoft.Json.JsonSerializerSettings
                          { StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.EscapeHtml });

                    string modifiedTemplate = template.Replace("var databaseJson = '';", $"var databaseJson = '{schema.Replace(@"\r\n", "").Replace(@"\u", "")}';");
                    string destination = Path.Combine(reportsDirectory, $"{this._database.Name}-{DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss")}.html");
                    File.WriteAllText(destination, modifiedTemplate);
                    System.Diagnostics.Process.Start(destination);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"There was an error generating the report. The message is {ex.Message}");
            }
        }

        private void sQLServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseContainer clonedDatabase = this._database.Clone();
            clonedDatabase.TransformToMssql();

            this._userControlGenerate.Generate(clonedDatabase, SqlGeneratorFactory.Factory(DatabaseContainer.DatabaseTypeMSSQL));
            this.setPanelUserControl(this._userControlGenerate);
        }

        private void postgresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DatabaseContainer clonedDatabase = this._database.Clone();
            clonedDatabase.TransformToPostgres();

            this._userControlGenerate.Generate(clonedDatabase, SqlGeneratorFactory.Factory(DatabaseContainer.DatabaseTypePostgreSQL));
            this.setPanelUserControl(this._userControlGenerate);
        }

        private void addMultiTenancyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._database.AddMultiTenancy();
            this.SelectSchema();
        }

        private void addIsDeletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._database.AddIsDeleted();
            this.SelectSchema();
        }

        private void editTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBoxTables.SelectedIndex > -1)
            {
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                Table table = (Table)this.listBoxTables.SelectedItem;

                this._userControlTableEntry.LoadTable(table, schema.Name);
                this.setPanelUserControl(this._userControlTableEntry);
            }
        }

        private void editSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listBoxSchemas.SelectedIndex > -1)
            {
                Schema schema = (Schema)this.listBoxSchemas.SelectedItem;
                this._userControlSchemaEntry.LoadSchema(schema);
                this.setPanelUserControl(this._userControlSchemaEntry);
            }
        }

        private void removeMultiTenancyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._database.RemoveMultiTenancy();
            this.SelectSchema();
        }

        private void removeIsDeletedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._database.RemoveIsDeleted();
            this.SelectSchema();
        }
    }
}