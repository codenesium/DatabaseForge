namespace Codenesium.DatabaseForgeLib.UserForms
{
    partial class FormIndexCreator
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
            this.listBoxConstraintColumns = new System.Windows.Forms.ListBox();
            this.comboBoxColumns = new MetroFramework.Controls.MetroComboBox();
            this.buttonAdd = new MetroFramework.Controls.MetroButton();
            this.buttonSave = new MetroFramework.Controls.MetroButton();
            this.comboBoxConstraints = new MetroFramework.Controls.MetroComboBox();
            this.checkBoxIsUnique = new System.Windows.Forms.CheckBox();
            this.checkBoxIsPrimaryKey = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddConstraint = new MetroFramework.Controls.MetroButton();
            this.checkBoxDescending = new System.Windows.Forms.CheckBox();
            this.contextMenuStripColumns = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripConstraint = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemDeleteConstraint = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxConstraintType = new MetroFramework.Controls.MetroComboBox();
            this.checkBoxIsIdentity = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludedColumn = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStripColumns.SuspendLayout();
            this.contextMenuStripConstraint.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxConstraintColumns
            // 
            this.listBoxConstraintColumns.FormattingEnabled = true;
            this.listBoxConstraintColumns.Location = new System.Drawing.Point(358, 225);
            this.listBoxConstraintColumns.Name = "listBoxConstraintColumns";
            this.listBoxConstraintColumns.Size = new System.Drawing.Size(173, 173);
            this.listBoxConstraintColumns.TabIndex = 0;
            this.listBoxConstraintColumns.SelectedIndexChanged += new System.EventHandler(this.listBoxItems_SelectedIndexChanged);
            this.listBoxConstraintColumns.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxItems_MouseDown);
            // 
            // comboBoxColumns
            // 
            this.comboBoxColumns.FormattingEnabled = true;
            this.comboBoxColumns.ItemHeight = 23;
            this.comboBoxColumns.Location = new System.Drawing.Point(26, 369);
            this.comboBoxColumns.Name = "comboBoxColumns";
            this.comboBoxColumns.Size = new System.Drawing.Size(226, 29);
            this.comboBoxColumns.TabIndex = 8;
            this.comboBoxColumns.UseSelectable = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(270, 372);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 32;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseSelectable = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(456, 476);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 33;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseSelectable = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxConstraints
            // 
            this.comboBoxConstraints.FormattingEnabled = true;
            this.comboBoxConstraints.ItemHeight = 23;
            this.comboBoxConstraints.Location = new System.Drawing.Point(23, 90);
            this.comboBoxConstraints.Name = "comboBoxConstraints";
            this.comboBoxConstraints.Size = new System.Drawing.Size(226, 29);
            this.comboBoxConstraints.TabIndex = 34;
            this.comboBoxConstraints.UseSelectable = true;
            this.comboBoxConstraints.SelectedIndexChanged += new System.EventHandler(this.comboBoxConstraints_SelectedIndexChanged);
            this.comboBoxConstraints.MouseDown += new System.Windows.Forms.MouseEventHandler(this.comboBoxConstraints_MouseDown);
            // 
            // checkBoxIsUnique
            // 
            this.checkBoxIsUnique.AutoSize = true;
            this.checkBoxIsUnique.Location = new System.Drawing.Point(24, 140);
            this.checkBoxIsUnique.Name = "checkBoxIsUnique";
            this.checkBoxIsUnique.Size = new System.Drawing.Size(60, 17);
            this.checkBoxIsUnique.TabIndex = 35;
            this.checkBoxIsUnique.Text = "Unique";
            this.checkBoxIsUnique.UseVisualStyleBackColor = true;
            this.checkBoxIsUnique.CheckedChanged += new System.EventHandler(this.checkBoxIsUnique_CheckedChanged);
            // 
            // checkBoxIsPrimaryKey
            // 
            this.checkBoxIsPrimaryKey.AutoSize = true;
            this.checkBoxIsPrimaryKey.Location = new System.Drawing.Point(23, 163);
            this.checkBoxIsPrimaryKey.Name = "checkBoxIsPrimaryKey";
            this.checkBoxIsPrimaryKey.Size = new System.Drawing.Size(81, 17);
            this.checkBoxIsPrimaryKey.TabIndex = 36;
            this.checkBoxIsPrimaryKey.Text = "Primary Key";
            this.checkBoxIsPrimaryKey.UseVisualStyleBackColor = true;
            this.checkBoxIsPrimaryKey.CheckedChanged += new System.EventHandler(this.checkBoxIsPrimaryKey_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 353);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Column";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Constraints";
            // 
            // buttonAddConstraint
            // 
            this.buttonAddConstraint.Location = new System.Drawing.Point(267, 93);
            this.buttonAddConstraint.Name = "buttonAddConstraint";
            this.buttonAddConstraint.Size = new System.Drawing.Size(75, 23);
            this.buttonAddConstraint.TabIndex = 40;
            this.buttonAddConstraint.Text = "Add";
            this.buttonAddConstraint.UseSelectable = true;
            this.buttonAddConstraint.Click += new System.EventHandler(this.buttonAddConstraint_Click);
            // 
            // checkBoxDescending
            // 
            this.checkBoxDescending.AutoSize = true;
            this.checkBoxDescending.Location = new System.Drawing.Point(358, 404);
            this.checkBoxDescending.Name = "checkBoxDescending";
            this.checkBoxDescending.Size = new System.Drawing.Size(83, 17);
            this.checkBoxDescending.TabIndex = 41;
            this.checkBoxDescending.Text = "Descending";
            this.checkBoxDescending.UseVisualStyleBackColor = true;
            this.checkBoxDescending.CheckedChanged += new System.EventHandler(this.checkBoxDescending_CheckedChanged);
            // 
            // contextMenuStripColumns
            // 
            this.contextMenuStripColumns.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStripColumns.Name = "contextMenuStripColumns";
            this.contextMenuStripColumns.Size = new System.Drawing.Size(106, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // contextMenuStripConstraint
            // 
            this.contextMenuStripConstraint.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemDeleteConstraint});
            this.contextMenuStripConstraint.Name = "contextMenuStripColumns";
            this.contextMenuStripConstraint.Size = new System.Drawing.Size(106, 26);
            // 
            // toolStripMenuItemDeleteConstraint
            // 
            this.toolStripMenuItemDeleteConstraint.Name = "toolStripMenuItemDeleteConstraint";
            this.toolStripMenuItemDeleteConstraint.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItemDeleteConstraint.Text = "Delete";
            this.toolStripMenuItemDeleteConstraint.Click += new System.EventHandler(this.toolStripMenuItemDeleteConstraint_Click);
            // 
            // comboBoxConstraintType
            // 
            this.comboBoxConstraintType.FormattingEnabled = true;
            this.comboBoxConstraintType.ItemHeight = 23;
            this.comboBoxConstraintType.Items.AddRange(new object[] {
            "CLUSTERED",
            "NONCLUSTERED"});
            this.comboBoxConstraintType.Location = new System.Drawing.Point(26, 318);
            this.comboBoxConstraintType.Name = "comboBoxConstraintType";
            this.comboBoxConstraintType.Size = new System.Drawing.Size(154, 29);
            this.comboBoxConstraintType.TabIndex = 42;
            this.comboBoxConstraintType.UseSelectable = true;
            this.comboBoxConstraintType.SelectedIndexChanged += new System.EventHandler(this.comboBoxConstraintType_SelectedIndexChanged);
            // 
            // checkBoxIsIdentity
            // 
            this.checkBoxIsIdentity.AutoSize = true;
            this.checkBoxIsIdentity.Location = new System.Drawing.Point(358, 450);
            this.checkBoxIsIdentity.Name = "checkBoxIsIdentity";
            this.checkBoxIsIdentity.Size = new System.Drawing.Size(60, 17);
            this.checkBoxIsIdentity.TabIndex = 43;
            this.checkBoxIsIdentity.Text = "Identity";
            this.checkBoxIsIdentity.UseVisualStyleBackColor = true;
            this.checkBoxIsIdentity.CheckedChanged += new System.EventHandler(this.checkBoxIsIdentity_CheckedChanged);
            // 
            // checkBoxIncludedColumn
            // 
            this.checkBoxIncludedColumn.AutoSize = true;
            this.checkBoxIncludedColumn.Location = new System.Drawing.Point(358, 427);
            this.checkBoxIncludedColumn.Name = "checkBoxIncludedColumn";
            this.checkBoxIncludedColumn.Size = new System.Drawing.Size(105, 17);
            this.checkBoxIncludedColumn.TabIndex = 44;
            this.checkBoxIncludedColumn.Text = "Included Column";
            this.checkBoxIncludedColumn.UseVisualStyleBackColor = true;
            this.checkBoxIncludedColumn.CheckedChanged += new System.EventHandler(this.checkBoxIncludedColumn_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 45;
            this.label3.Text = "Type";
            // 
            // FormIndexCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 528);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxIncludedColumn);
            this.Controls.Add(this.checkBoxIsIdentity);
            this.Controls.Add(this.comboBoxConstraintType);
            this.Controls.Add(this.checkBoxDescending);
            this.Controls.Add(this.buttonAddConstraint);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxIsPrimaryKey);
            this.Controls.Add(this.checkBoxIsUnique);
            this.Controls.Add(this.comboBoxConstraints);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.comboBoxColumns);
            this.Controls.Add(this.listBoxConstraintColumns);
            this.Name = "FormIndexCreator";
            this.Text = "Index Forge";
            this.contextMenuStripColumns.ResumeLayout(false);
            this.contextMenuStripConstraint.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxConstraintColumns;
        private MetroFramework.Controls.MetroComboBox comboBoxColumns;
        private MetroFramework.Controls.MetroButton buttonAdd;
        private MetroFramework.Controls.MetroButton buttonSave;
        private MetroFramework.Controls.MetroComboBox comboBoxConstraints;
        private System.Windows.Forms.CheckBox checkBoxIsUnique;
        private System.Windows.Forms.CheckBox checkBoxIsPrimaryKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroButton buttonAddConstraint;
        private System.Windows.Forms.CheckBox checkBoxDescending;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripColumns;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripConstraint;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeleteConstraint;
        private MetroFramework.Controls.MetroComboBox comboBoxConstraintType;
        private System.Windows.Forms.CheckBox checkBoxIsIdentity;
        private System.Windows.Forms.CheckBox checkBoxIncludedColumn;
        private System.Windows.Forms.Label label3;
    }
}