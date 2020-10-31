namespace Codenesium.DatabaseForgeLib.UserControls
{
    partial class UserControlDBForgeEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labeTables = new MetroFramework.Controls.MetroLabel();
            this.listBoxTables = new System.Windows.Forms.ListBox();
            this.panelContainer = new MetroFramework.Controls.MetroPanel();
            this.textBoxTableSearch = new MetroFramework.Controls.MetroTextBox();
            this.contextMenuStripIndexes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addVirtualIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.listBoxColumns = new System.Windows.Forms.ListBox();
            this.buttonAddTable = new MetroFramework.Controls.MetroButton();
            this.buttonAddFields = new MetroFramework.Controls.MetroButton();
            this.buttonAddSchema = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.listBoxSchemas = new System.Windows.Forms.ListBox();
            this.contextMenuAddPrimaryKey = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createPrimaryKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deletePrimaryKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editForeignKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteForeignKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createForeignKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialogProject = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialogProject = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStripTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCompleteSchemaEditing = new MetroFramework.Controls.MetroButton();
            this.buttonCancelSchemaEditing = new MetroFramework.Controls.MetroButton();
            this.buttonTools = new MetroFramework.Controls.MetroButton();
            this.contextMenuStripTools = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.geenrateSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sQLServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postgresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildDependencyTreeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateTestDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bulkOperationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addMultiTenancyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addIsDeletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripSchema = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuDeleteSchema = new System.Windows.Forms.ToolStripMenuItem();
            this.removeMultiTenancyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeIsDeletedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripIndexes.SuspendLayout();
            this.contextMenuAddPrimaryKey.SuspendLayout();
            this.contextMenuStripTable.SuspendLayout();
            this.contextMenuStripTools.SuspendLayout();
            this.contextMenuStripSchema.SuspendLayout();
            this.SuspendLayout();
            // 
            // labeTables
            // 
            this.labeTables.AutoSize = true;
            this.labeTables.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labeTables.Location = new System.Drawing.Point(24, 207);
            this.labeTables.Name = "labeTables";
            this.labeTables.Size = new System.Drawing.Size(60, 25);
            this.labeTables.TabIndex = 6;
            this.labeTables.Text = "Tables";
            // 
            // listBoxTables
            // 
            this.listBoxTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxTables.FormattingEnabled = true;
            this.listBoxTables.Location = new System.Drawing.Point(27, 236);
            this.listBoxTables.Name = "listBoxTables";
            this.listBoxTables.Size = new System.Drawing.Size(259, 80);
            this.listBoxTables.TabIndex = 5;
            this.listBoxTables.SelectedIndexChanged += new System.EventHandler(this.listBoxTables_SelectedIndexChanged);
            this.listBoxTables.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxTables_MouseDown);
            // 
            // panelContainer
            // 
            this.panelContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContainer.HorizontalScrollbarBarColor = true;
            this.panelContainer.HorizontalScrollbarHighlightOnWheel = false;
            this.panelContainer.HorizontalScrollbarSize = 10;
            this.panelContainer.Location = new System.Drawing.Point(350, 93);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(435, 378);
            this.panelContainer.TabIndex = 7;
            this.panelContainer.VerticalScrollbarBarColor = true;
            this.panelContainer.VerticalScrollbarHighlightOnWheel = false;
            this.panelContainer.VerticalScrollbarSize = 10;
            // 
            // textBoxTableSearch
            // 
            // 
            // 
            // 
            this.textBoxTableSearch.CustomButton.Image = null;
            this.textBoxTableSearch.CustomButton.Location = new System.Drawing.Point(136, 1);
            this.textBoxTableSearch.CustomButton.Name = "";
            this.textBoxTableSearch.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBoxTableSearch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxTableSearch.CustomButton.TabIndex = 1;
            this.textBoxTableSearch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxTableSearch.CustomButton.UseSelectable = true;
            this.textBoxTableSearch.CustomButton.Visible = false;
            this.textBoxTableSearch.Lines = new string[0];
            this.textBoxTableSearch.Location = new System.Drawing.Point(128, 207);
            this.textBoxTableSearch.MaxLength = 32767;
            this.textBoxTableSearch.Name = "textBoxTableSearch";
            this.textBoxTableSearch.PasswordChar = '\0';
            this.textBoxTableSearch.PromptText = "Search";
            this.textBoxTableSearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxTableSearch.SelectedText = "";
            this.textBoxTableSearch.SelectionLength = 0;
            this.textBoxTableSearch.SelectionStart = 0;
            this.textBoxTableSearch.ShortcutsEnabled = true;
            this.textBoxTableSearch.Size = new System.Drawing.Size(158, 23);
            this.textBoxTableSearch.TabIndex = 27;
            this.textBoxTableSearch.UseSelectable = true;
            this.textBoxTableSearch.WaterMark = "Search";
            this.textBoxTableSearch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxTableSearch.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxTableSearch.TextChanged += new System.EventHandler(this.textBoxTableSearch_TextChanged);
            // 
            // contextMenuStripIndexes
            // 
            this.contextMenuStripIndexes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addVirtualIndexToolStripMenuItem});
            this.contextMenuStripIndexes.Name = "contextMenuStripIndexes";
            this.contextMenuStripIndexes.Size = new System.Drawing.Size(165, 26);
            // 
            // addVirtualIndexToolStripMenuItem
            // 
            this.addVirtualIndexToolStripMenuItem.Name = "addVirtualIndexToolStripMenuItem";
            this.addVirtualIndexToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.addVirtualIndexToolStripMenuItem.Text = "Add Virtual Index";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(24, 323);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(79, 25);
            this.metroLabel1.TabIndex = 9;
            this.metroLabel1.Text = "Columns";
            // 
            // listBoxColumns
            // 
            this.listBoxColumns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxColumns.FormattingEnabled = true;
            this.listBoxColumns.Location = new System.Drawing.Point(27, 350);
            this.listBoxColumns.Name = "listBoxColumns";
            this.listBoxColumns.Size = new System.Drawing.Size(259, 119);
            this.listBoxColumns.TabIndex = 8;
            this.listBoxColumns.SelectedIndexChanged += new System.EventHandler(this.listBoxFields_SelectedIndexChanged);
            this.listBoxColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxFields_MouseDown);
            // 
            // buttonAddTable
            // 
            this.buttonAddTable.Location = new System.Drawing.Point(293, 236);
            this.buttonAddTable.Name = "buttonAddTable";
            this.buttonAddTable.Size = new System.Drawing.Size(32, 23);
            this.buttonAddTable.TabIndex = 28;
            this.buttonAddTable.Text = "+";
            this.buttonAddTable.UseSelectable = true;
            this.buttonAddTable.Click += new System.EventHandler(this.buttonAddTable_Click);
            // 
            // buttonAddFields
            // 
            this.buttonAddFields.Location = new System.Drawing.Point(293, 350);
            this.buttonAddFields.Name = "buttonAddFields";
            this.buttonAddFields.Size = new System.Drawing.Size(32, 23);
            this.buttonAddFields.TabIndex = 29;
            this.buttonAddFields.Text = "+";
            this.buttonAddFields.UseSelectable = true;
            this.buttonAddFields.Click += new System.EventHandler(this.buttonAddFields_Click);
            // 
            // buttonAddSchema
            // 
            this.buttonAddSchema.Location = new System.Drawing.Point(293, 93);
            this.buttonAddSchema.Name = "buttonAddSchema";
            this.buttonAddSchema.Size = new System.Drawing.Size(32, 23);
            this.buttonAddSchema.TabIndex = 33;
            this.buttonAddSchema.Text = "+";
            this.buttonAddSchema.UseSelectable = true;
            this.buttonAddSchema.Click += new System.EventHandler(this.buttonAddSchema_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(24, 65);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(79, 25);
            this.metroLabel2.TabIndex = 32;
            this.metroLabel2.Text = "Schemas";
            // 
            // listBoxSchemas
            // 
            this.listBoxSchemas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxSchemas.FormattingEnabled = true;
            this.listBoxSchemas.Location = new System.Drawing.Point(27, 93);
            this.listBoxSchemas.Name = "listBoxSchemas";
            this.listBoxSchemas.Size = new System.Drawing.Size(259, 93);
            this.listBoxSchemas.TabIndex = 31;
            this.listBoxSchemas.SelectedIndexChanged += new System.EventHandler(this.listBoxSchemas_SelectedIndexChanged);
            this.listBoxSchemas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxSchemas_MouseDown);
            // 
            // contextMenuAddPrimaryKey
            // 
            this.contextMenuAddPrimaryKey.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createPrimaryKeyToolStripMenuItem,
            this.deletePrimaryKeyToolStripMenuItem,
            this.editForeignKeyToolStripMenuItem,
            this.deleteForeignKeyToolStripMenuItem,
            this.createForeignKeyToolStripMenuItem,
            this.deleteFieldToolStripMenuItem});
            this.contextMenuAddPrimaryKey.Name = "contextMenuStripForeignKeys";
            this.contextMenuAddPrimaryKey.Size = new System.Drawing.Size(175, 136);
            this.contextMenuAddPrimaryKey.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuAddPrimaryKey_Opening);
            // 
            // createPrimaryKeyToolStripMenuItem
            // 
            this.createPrimaryKeyToolStripMenuItem.Name = "createPrimaryKeyToolStripMenuItem";
            this.createPrimaryKeyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.createPrimaryKeyToolStripMenuItem.Text = "Create Primary Key";
            this.createPrimaryKeyToolStripMenuItem.Click += new System.EventHandler(this.createPrimaryKeyToolStripMenuItem_Click);
            // 
            // deletePrimaryKeyToolStripMenuItem
            // 
            this.deletePrimaryKeyToolStripMenuItem.Name = "deletePrimaryKeyToolStripMenuItem";
            this.deletePrimaryKeyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.deletePrimaryKeyToolStripMenuItem.Text = "Delete Primary Key";
            this.deletePrimaryKeyToolStripMenuItem.Click += new System.EventHandler(this.deletePrimaryKeyToolStripMenuItem_Click);
            // 
            // editForeignKeyToolStripMenuItem
            // 
            this.editForeignKeyToolStripMenuItem.Name = "editForeignKeyToolStripMenuItem";
            this.editForeignKeyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.editForeignKeyToolStripMenuItem.Text = "Edit Foreign Key";
            this.editForeignKeyToolStripMenuItem.Click += new System.EventHandler(this.editForeignKeyToolStripMenuItem_Click);
            // 
            // deleteForeignKeyToolStripMenuItem
            // 
            this.deleteForeignKeyToolStripMenuItem.Name = "deleteForeignKeyToolStripMenuItem";
            this.deleteForeignKeyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.deleteForeignKeyToolStripMenuItem.Text = "Delete Foreign Key";
            this.deleteForeignKeyToolStripMenuItem.Click += new System.EventHandler(this.deleteForeignKeyToolStripMenuItem_Click);
            // 
            // createForeignKeyToolStripMenuItem
            // 
            this.createForeignKeyToolStripMenuItem.Name = "createForeignKeyToolStripMenuItem";
            this.createForeignKeyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.createForeignKeyToolStripMenuItem.Text = "Create Foreign Key";
            this.createForeignKeyToolStripMenuItem.Click += new System.EventHandler(this.createForeignKeyToolStripMenuItem_Click);
            // 
            // deleteFieldToolStripMenuItem
            // 
            this.deleteFieldToolStripMenuItem.Name = "deleteFieldToolStripMenuItem";
            this.deleteFieldToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.deleteFieldToolStripMenuItem.Text = "Delete Column";
            this.deleteFieldToolStripMenuItem.Click += new System.EventHandler(this.deleteFieldToolStripMenuItem_Click);
            // 
            // openFileDialogProject
            // 
            this.openFileDialogProject.FileName = "project.json";
            // 
            // contextMenuStripTable
            // 
            this.contextMenuStripTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addIndexToolStripMenuItem,
            this.editTableToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStripTable.Name = "contextMenuStripTable";
            this.contextMenuStripTable.Size = new System.Drawing.Size(140, 70);
            // 
            // addIndexToolStripMenuItem
            // 
            this.addIndexToolStripMenuItem.Name = "addIndexToolStripMenuItem";
            this.addIndexToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.addIndexToolStripMenuItem.Text = "Index Forge";
            this.addIndexToolStripMenuItem.Click += new System.EventHandler(this.addIndexToolStripMenuItem_Click);
            // 
            // editTableToolStripMenuItem
            // 
            this.editTableToolStripMenuItem.Name = "editTableToolStripMenuItem";
            this.editTableToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.editTableToolStripMenuItem.Text = "Edit Table";
            this.editTableToolStripMenuItem.Click += new System.EventHandler(this.editTableToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.deleteToolStripMenuItem.Text = "Delete Table";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // buttonCompleteSchemaEditing
            // 
            this.buttonCompleteSchemaEditing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonCompleteSchemaEditing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCompleteSchemaEditing.ForeColor = System.Drawing.Color.White;
            this.buttonCompleteSchemaEditing.Location = new System.Drawing.Point(27, 17);
            this.buttonCompleteSchemaEditing.Name = "buttonCompleteSchemaEditing";
            this.buttonCompleteSchemaEditing.Size = new System.Drawing.Size(175, 35);
            this.buttonCompleteSchemaEditing.TabIndex = 42;
            this.buttonCompleteSchemaEditing.Text = "Complete Schema Editing";
            this.buttonCompleteSchemaEditing.UseCustomBackColor = true;
            this.buttonCompleteSchemaEditing.UseCustomForeColor = true;
            this.buttonCompleteSchemaEditing.UseSelectable = true;
            this.buttonCompleteSchemaEditing.Click += new System.EventHandler(this.buttonCompleteSchemaEditing_Click);
            // 
            // buttonCancelSchemaEditing
            // 
            this.buttonCancelSchemaEditing.BackColor = System.Drawing.Color.Blue;
            this.buttonCancelSchemaEditing.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCancelSchemaEditing.ForeColor = System.Drawing.Color.White;
            this.buttonCancelSchemaEditing.Location = new System.Drawing.Point(240, 17);
            this.buttonCancelSchemaEditing.Name = "buttonCancelSchemaEditing";
            this.buttonCancelSchemaEditing.Size = new System.Drawing.Size(175, 35);
            this.buttonCancelSchemaEditing.TabIndex = 43;
            this.buttonCancelSchemaEditing.Text = "Cancel Schema Editing";
            this.buttonCancelSchemaEditing.UseCustomBackColor = true;
            this.buttonCancelSchemaEditing.UseCustomForeColor = true;
            this.buttonCancelSchemaEditing.UseSelectable = true;
            this.buttonCancelSchemaEditing.Click += new System.EventHandler(this.buttonCancelSchemaEditing_Click);
            // 
            // buttonTools
            // 
            this.buttonTools.ContextMenuStrip = this.contextMenuStripTools;
            this.buttonTools.Location = new System.Drawing.Point(350, 67);
            this.buttonTools.Name = "buttonTools";
            this.buttonTools.Size = new System.Drawing.Size(118, 23);
            this.buttonTools.TabIndex = 45;
            this.buttonTools.Text = "Tools";
            this.buttonTools.UseSelectable = true;
            this.buttonTools.Click += new System.EventHandler(this.buttonTools_Click);
            this.buttonTools.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonTools_MouseDown);
            // 
            // contextMenuStripTools
            // 
            this.contextMenuStripTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geenrateSQLToolStripMenuItem,
            this.buildDependencyTreeToolStripMenuItem,
            this.generateTestDataToolStripMenuItem,
            this.webReportToolStripMenuItem,
            this.bulkOperationsToolStripMenuItem});
            this.contextMenuStripTools.Name = "contextMenuStripTools";
            this.contextMenuStripTools.Size = new System.Drawing.Size(211, 136);
            // 
            // geenrateSQLToolStripMenuItem
            // 
            this.geenrateSQLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sQLServerToolStripMenuItem,
            this.postgresToolStripMenuItem});
            this.geenrateSQLToolStripMenuItem.Name = "geenrateSQLToolStripMenuItem";
            this.geenrateSQLToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.geenrateSQLToolStripMenuItem.Text = "Generate Drop and Create";
            this.geenrateSQLToolStripMenuItem.Click += new System.EventHandler(this.geenrateSQLToolStripMenuItem_Click);
            // 
            // sQLServerToolStripMenuItem
            // 
            this.sQLServerToolStripMenuItem.Name = "sQLServerToolStripMenuItem";
            this.sQLServerToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.sQLServerToolStripMenuItem.Text = "SQL Server";
            this.sQLServerToolStripMenuItem.Click += new System.EventHandler(this.sQLServerToolStripMenuItem_Click);
            // 
            // postgresToolStripMenuItem
            // 
            this.postgresToolStripMenuItem.Name = "postgresToolStripMenuItem";
            this.postgresToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.postgresToolStripMenuItem.Text = "Postgres";
            this.postgresToolStripMenuItem.Click += new System.EventHandler(this.postgresToolStripMenuItem_Click);
            // 
            // buildDependencyTreeToolStripMenuItem
            // 
            this.buildDependencyTreeToolStripMenuItem.Name = "buildDependencyTreeToolStripMenuItem";
            this.buildDependencyTreeToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.buildDependencyTreeToolStripMenuItem.Text = "Build Dependency Tree";
            this.buildDependencyTreeToolStripMenuItem.Click += new System.EventHandler(this.buildDependencyTreeToolStripMenuItem_Click);
            // 
            // generateTestDataToolStripMenuItem
            // 
            this.generateTestDataToolStripMenuItem.Name = "generateTestDataToolStripMenuItem";
            this.generateTestDataToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.generateTestDataToolStripMenuItem.Text = "Generate Test Data";
            this.generateTestDataToolStripMenuItem.Click += new System.EventHandler(this.generateTestDataToolStripMenuItem_Click);
            // 
            // webReportToolStripMenuItem
            // 
            this.webReportToolStripMenuItem.Name = "webReportToolStripMenuItem";
            this.webReportToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.webReportToolStripMenuItem.Text = "Web Report";
            this.webReportToolStripMenuItem.Click += new System.EventHandler(this.webReportToolStripMenuItem_Click);
            // 
            // bulkOperationsToolStripMenuItem
            // 
            this.bulkOperationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addMultiTenancyToolStripMenuItem,
            this.removeMultiTenancyToolStripMenuItem,
            this.addIsDeletedToolStripMenuItem,
            this.removeIsDeletedToolStripMenuItem});
            this.bulkOperationsToolStripMenuItem.Name = "bulkOperationsToolStripMenuItem";
            this.bulkOperationsToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.bulkOperationsToolStripMenuItem.Text = "Bulk Operations";
            // 
            // addMultiTenancyToolStripMenuItem
            // 
            this.addMultiTenancyToolStripMenuItem.Name = "addMultiTenancyToolStripMenuItem";
            this.addMultiTenancyToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.addMultiTenancyToolStripMenuItem.Text = "Add Multi-Tenancy";
            this.addMultiTenancyToolStripMenuItem.Click += new System.EventHandler(this.addMultiTenancyToolStripMenuItem_Click);
            // 
            // addIsDeletedToolStripMenuItem
            // 
            this.addIsDeletedToolStripMenuItem.Name = "addIsDeletedToolStripMenuItem";
            this.addIsDeletedToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.addIsDeletedToolStripMenuItem.Text = "Add IsDeleted";
            this.addIsDeletedToolStripMenuItem.Click += new System.EventHandler(this.addIsDeletedToolStripMenuItem_Click);
            // 
            // contextMenuStripSchema
            // 
            this.contextMenuStripSchema.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editSchemaToolStripMenuItem,
            this.toolStripMenuDeleteSchema});
            this.contextMenuStripSchema.Name = "contextMenuStripTable";
            this.contextMenuStripSchema.Size = new System.Drawing.Size(153, 48);
            // 
            // editSchemaToolStripMenuItem
            // 
            this.editSchemaToolStripMenuItem.Name = "editSchemaToolStripMenuItem";
            this.editSchemaToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.editSchemaToolStripMenuItem.Text = "Edit Schema";
            this.editSchemaToolStripMenuItem.Click += new System.EventHandler(this.editSchemaToolStripMenuItem_Click);
            // 
            // toolStripMenuDeleteSchema
            // 
            this.toolStripMenuDeleteSchema.Name = "toolStripMenuDeleteSchema";
            this.toolStripMenuDeleteSchema.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuDeleteSchema.Text = "Delete Schema";
            this.toolStripMenuDeleteSchema.Click += new System.EventHandler(this.toolStripMenuDeleteSchema_Click);
            // 
            // removeMultiTenancyToolStripMenuItem
            // 
            this.removeMultiTenancyToolStripMenuItem.Name = "removeMultiTenancyToolStripMenuItem";
            this.removeMultiTenancyToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.removeMultiTenancyToolStripMenuItem.Text = "Remove Multi-Tenancy";
            this.removeMultiTenancyToolStripMenuItem.Click += new System.EventHandler(this.removeMultiTenancyToolStripMenuItem_Click);
            // 
            // removeIsDeletedToolStripMenuItem
            // 
            this.removeIsDeletedToolStripMenuItem.Name = "removeIsDeletedToolStripMenuItem";
            this.removeIsDeletedToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.removeIsDeletedToolStripMenuItem.Text = "Remove IsDeleted";
            this.removeIsDeletedToolStripMenuItem.Click += new System.EventHandler(this.removeIsDeletedToolStripMenuItem_Click);
            // 
            // UserControlDBForgeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonTools);
            this.Controls.Add(this.buttonCancelSchemaEditing);
            this.Controls.Add(this.buttonCompleteSchemaEditing);
            this.Controls.Add(this.buttonAddSchema);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.listBoxSchemas);
            this.Controls.Add(this.buttonAddFields);
            this.Controls.Add(this.buttonAddTable);
            this.Controls.Add(this.textBoxTableSearch);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.listBoxColumns);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.labeTables);
            this.Controls.Add(this.listBoxTables);
            this.Name = "UserControlDBForgeEditor";
            this.Controls.SetChildIndex(this.listBoxTables, 0);
            this.Controls.SetChildIndex(this.labeTables, 0);
            this.Controls.SetChildIndex(this.panelContainer, 0);
            this.Controls.SetChildIndex(this.listBoxColumns, 0);
            this.Controls.SetChildIndex(this.metroLabel1, 0);
            this.Controls.SetChildIndex(this.textBoxTableSearch, 0);
            this.Controls.SetChildIndex(this.buttonAddTable, 0);
            this.Controls.SetChildIndex(this.buttonAddFields, 0);
            this.Controls.SetChildIndex(this.listBoxSchemas, 0);
            this.Controls.SetChildIndex(this.metroLabel2, 0);
            this.Controls.SetChildIndex(this.buttonAddSchema, 0);
            this.Controls.SetChildIndex(this.buttonCompleteSchemaEditing, 0);
            this.Controls.SetChildIndex(this.buttonCancelSchemaEditing, 0);
            this.Controls.SetChildIndex(this.buttonTools, 0);
            this.contextMenuStripIndexes.ResumeLayout(false);
            this.contextMenuAddPrimaryKey.ResumeLayout(false);
            this.contextMenuStripTable.ResumeLayout(false);
            this.contextMenuStripTools.ResumeLayout(false);
            this.contextMenuStripSchema.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel labeTables;
        private System.Windows.Forms.ListBox listBoxTables;
        private MetroFramework.Controls.MetroPanel panelContainer;
        private MetroFramework.Controls.MetroTextBox textBoxTableSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripIndexes;
        private System.Windows.Forms.ToolStripMenuItem addVirtualIndexToolStripMenuItem;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.ListBox listBoxColumns;
        private MetroFramework.Controls.MetroButton buttonAddTable;
        private MetroFramework.Controls.MetroButton buttonAddFields;
        private MetroFramework.Controls.MetroButton buttonAddSchema;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.ListBox listBoxSchemas;
        private System.Windows.Forms.ContextMenuStrip contextMenuAddPrimaryKey;
        private System.Windows.Forms.ToolStripMenuItem createPrimaryKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createForeignKeyToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialogProject;
        private System.Windows.Forms.OpenFileDialog openFileDialogProject;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTable;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteForeignKeyToolStripMenuItem;
        private MetroFramework.Controls.MetroButton buttonCompleteSchemaEditing;
        private MetroFramework.Controls.MetroButton buttonCancelSchemaEditing;
        private System.Windows.Forms.ToolStripMenuItem addIndexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deletePrimaryKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editForeignKeyToolStripMenuItem;
        private MetroFramework.Controls.MetroButton buttonTools;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTools;
        private System.Windows.Forms.ToolStripMenuItem geenrateSQLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildDependencyTreeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSchema;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuDeleteSchema;
        private System.Windows.Forms.ToolStripMenuItem generateTestDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem webReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sQLServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postgresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bulkOperationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addMultiTenancyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addIsDeletedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editSchemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeMultiTenancyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeIsDeletedToolStripMenuItem;
    }
}
