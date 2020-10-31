namespace Codenesium.DatabaseForgeLib.UserForms
{
    partial class FormDatabaseForge
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDatabaseForge));
            this.panelContainer = new System.Windows.Forms.Panel();
            this.buttonMenu = new MetroFramework.Controls.MetroButton();
            this.contextMenuStripMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadFromDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mSSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.postgreSQLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Location = new System.Drawing.Point(23, 63);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(800, 500);
            this.panelContainer.TabIndex = 1;
            // 
            // buttonMenu
            // 
            this.buttonMenu.Location = new System.Drawing.Point(202, 25);
            this.buttonMenu.Name = "buttonMenu";
            this.buttonMenu.Size = new System.Drawing.Size(119, 29);
            this.buttonMenu.TabIndex = 2;
            this.buttonMenu.Text = "Menu";
            this.buttonMenu.UseSelectable = true;
            this.buttonMenu.Click += new System.EventHandler(this.buttonMenu_Click);
            this.buttonMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonMenu_MouseDown);
            // 
            // contextMenuStripMenu
            // 
            this.contextMenuStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFromDatabaseToolStripMenuItem,
            this.newProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.loadProjectToolStripMenuItem});
            this.contextMenuStripMenu.Name = "contextMenuStripMenu";
            this.contextMenuStripMenu.Size = new System.Drawing.Size(195, 92);
            // 
            // loadFromDatabaseToolStripMenuItem
            // 
            this.loadFromDatabaseToolStripMenuItem.Name = "loadFromDatabaseToolStripMenuItem";
            this.loadFromDatabaseToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.loadFromDatabaseToolStripMenuItem.Text = "Load from Database";
            this.loadFromDatabaseToolStripMenuItem.Click += new System.EventHandler(this.loadFromDatabaseToolStripMenuItem_Click);
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSSQLToolStripMenuItem,
            this.postgreSQLToolStripMenuItem});
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // mSSQLToolStripMenuItem
            // 
            this.mSSQLToolStripMenuItem.Name = "mSSQLToolStripMenuItem";
            this.mSSQLToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.mSSQLToolStripMenuItem.Text = "MSSQL";
            this.mSSQLToolStripMenuItem.Click += new System.EventHandler(this.mSSQLToolStripMenuItem_Click);
            // 
            // postgreSQLToolStripMenuItem
            // 
            this.postgreSQLToolStripMenuItem.Name = "postgreSQLToolStripMenuItem";
            this.postgreSQLToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.postgreSQLToolStripMenuItem.Text = "PostgreSQL";
            this.postgreSQLToolStripMenuItem.Click += new System.EventHandler(this.postgreSQLToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project to Disk";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // loadProjectToolStripMenuItem
            // 
            this.loadProjectToolStripMenuItem.Name = "loadProjectToolStripMenuItem";
            this.loadProjectToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.loadProjectToolStripMenuItem.Text = "Load Project from Disk";
            this.loadProjectToolStripMenuItem.Click += new System.EventHandler(this.loadProjectToolStripMenuItem_Click);
            // 
            // FormDatabaseForge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 571);
            this.Controls.Add(this.buttonMenu);
            this.Controls.Add(this.panelContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDatabaseForge";
            this.Text = "Database Forge";
            this.contextMenuStripMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private MetroFramework.Controls.MetroButton buttonMenu;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMenu;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFromDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mSSQLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem postgreSQLToolStripMenuItem;
    }
}