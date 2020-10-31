namespace Codenesium.DatabaseForgeLib.UserForms
{
    partial class FormForeignKeyCreator
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
            this.comboBoxTable = new MetroFramework.Controls.MetroComboBox();
            this.comboBoxColumn = new MetroFramework.Controls.MetroComboBox();
            this.textBoxKeyName = new MetroFramework.Controls.MetroTextBox();
            this.labeTables = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.buttonSave = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // comboBoxTable
            // 
            this.comboBoxTable.FormattingEnabled = true;
            this.comboBoxTable.ItemHeight = 23;
            this.comboBoxTable.Location = new System.Drawing.Point(23, 119);
            this.comboBoxTable.Name = "comboBoxTable";
            this.comboBoxTable.Size = new System.Drawing.Size(226, 29);
            this.comboBoxTable.TabIndex = 6;
            this.comboBoxTable.UseSelectable = true;
            this.comboBoxTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxTable_SelectedIndexChanged);
            // 
            // comboBoxField
            // 
            this.comboBoxColumn.FormattingEnabled = true;
            this.comboBoxColumn.ItemHeight = 23;
            this.comboBoxColumn.Location = new System.Drawing.Point(23, 203);
            this.comboBoxColumn.Name = "comboBoxField";
            this.comboBoxColumn.Size = new System.Drawing.Size(226, 29);
            this.comboBoxColumn.TabIndex = 7;
            this.comboBoxColumn.UseSelectable = true;
            this.comboBoxColumn.SelectedIndexChanged += new System.EventHandler(this.comboBoxField_SelectedIndexChanged);
            // 
            // textBoxKeyName
            // 
            // 
            // 
            // 
            this.textBoxKeyName.CustomButton.Image = null;
            this.textBoxKeyName.CustomButton.Location = new System.Drawing.Point(204, 1);
            this.textBoxKeyName.CustomButton.Name = "";
            this.textBoxKeyName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBoxKeyName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxKeyName.CustomButton.TabIndex = 1;
            this.textBoxKeyName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxKeyName.CustomButton.UseSelectable = true;
            this.textBoxKeyName.CustomButton.Visible = false;
            this.textBoxKeyName.Lines = new string[0];
            this.textBoxKeyName.Location = new System.Drawing.Point(23, 298);
            this.textBoxKeyName.MaxLength = 32767;
            this.textBoxKeyName.Name = "textBoxKeyName";
            this.textBoxKeyName.PasswordChar = '\0';
            this.textBoxKeyName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxKeyName.SelectedText = "";
            this.textBoxKeyName.SelectionLength = 0;
            this.textBoxKeyName.SelectionStart = 0;
            this.textBoxKeyName.ShortcutsEnabled = true;
            this.textBoxKeyName.Size = new System.Drawing.Size(226, 23);
            this.textBoxKeyName.TabIndex = 8;
            this.textBoxKeyName.UseSelectable = true;
            this.textBoxKeyName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxKeyName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // labeTables
            // 
            this.labeTables.AutoSize = true;
            this.labeTables.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labeTables.Location = new System.Drawing.Point(23, 91);
            this.labeTables.Name = "labeTables";
            this.labeTables.Size = new System.Drawing.Size(60, 25);
            this.labeTables.TabIndex = 9;
            this.labeTables.Text = "Tables";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(23, 165);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(54, 25);
            this.metroLabel1.TabIndex = 10;
            this.metroLabel1.Text = "Fields";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(23, 259);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(89, 25);
            this.metroLabel2.TabIndex = 11;
            this.metroLabel2.Text = "Key Name";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(174, 350);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 31;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseSelectable = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormForeignKeyCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 396);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.labeTables);
            this.Controls.Add(this.textBoxKeyName);
            this.Controls.Add(this.comboBoxColumn);
            this.Controls.Add(this.comboBoxTable);
            this.Name = "FormForeignKeyCreator";
            this.ShowInTaskbar = false;
            this.Text = "Key Forge";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox comboBoxTable;
        private MetroFramework.Controls.MetroComboBox comboBoxColumn;
        private MetroFramework.Controls.MetroTextBox textBoxKeyName;
        private MetroFramework.Controls.MetroLabel labeTables;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton buttonSave;
    }
}