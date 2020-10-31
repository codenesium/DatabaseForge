namespace Codenesium.DatabaseForgeLib.UserControls
{
    partial class UserControlFieldRapidEntry
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
            this.textBoxFieldName = new MetroFramework.Controls.MetroTextBox();
            this.comboBoxFieldType = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.textBoxMaxLength = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.checkBoxNullable = new MetroFramework.Controls.MetroCheckBox();
            this.checkBoxDatabaseGenerated = new MetroFramework.Controls.MetroCheckBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.buttonSave = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // textBoxFieldName
            // 
            // 
            // 
            // 
            this.textBoxFieldName.CustomButton.Image = null;
            this.textBoxFieldName.CustomButton.Location = new System.Drawing.Point(204, 1);
            this.textBoxFieldName.CustomButton.Name = "";
            this.textBoxFieldName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBoxFieldName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxFieldName.CustomButton.TabIndex = 1;
            this.textBoxFieldName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxFieldName.CustomButton.UseSelectable = true;
            this.textBoxFieldName.CustomButton.Visible = false;
            this.textBoxFieldName.Lines = new string[0];
            this.textBoxFieldName.Location = new System.Drawing.Point(35, 36);
            this.textBoxFieldName.MaxLength = 32767;
            this.textBoxFieldName.Name = "textBoxFieldName";
            this.textBoxFieldName.PasswordChar = '\0';
            this.textBoxFieldName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxFieldName.SelectedText = "";
            this.textBoxFieldName.SelectionLength = 0;
            this.textBoxFieldName.SelectionStart = 0;
            this.textBoxFieldName.ShortcutsEnabled = true;
            this.textBoxFieldName.Size = new System.Drawing.Size(226, 23);
            this.textBoxFieldName.TabIndex = 3;
            this.textBoxFieldName.UseSelectable = true;
            this.textBoxFieldName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxFieldName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxFieldName.TextChanged += new System.EventHandler(this.textBoxFieldName_TextChanged);
            this.textBoxFieldName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxFieldName_KeyDown);
            this.textBoxFieldName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxFieldName_KeyPress);
            // 
            // comboBoxFieldType
            // 
            this.comboBoxFieldType.FormattingEnabled = true;
            this.comboBoxFieldType.ItemHeight = 23;
            this.comboBoxFieldType.Location = new System.Drawing.Point(35, 95);
            this.comboBoxFieldType.Name = "comboBoxFieldType";
            this.comboBoxFieldType.Size = new System.Drawing.Size(226, 29);
            this.comboBoxFieldType.TabIndex = 5;
            this.comboBoxFieldType.UseSelectable = true;
            this.comboBoxFieldType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxFieldType_KeyPress);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(34, 73);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(87, 19);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Column Type";
            // 
            // textBoxMaxLength
            // 
            // 
            // 
            // 
            this.textBoxMaxLength.CustomButton.Image = null;
            this.textBoxMaxLength.CustomButton.Location = new System.Drawing.Point(204, 1);
            this.textBoxMaxLength.CustomButton.Name = "";
            this.textBoxMaxLength.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBoxMaxLength.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxMaxLength.CustomButton.TabIndex = 1;
            this.textBoxMaxLength.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxMaxLength.CustomButton.UseSelectable = true;
            this.textBoxMaxLength.CustomButton.Visible = false;
            this.textBoxMaxLength.Lines = new string[0];
            this.textBoxMaxLength.Location = new System.Drawing.Point(34, 168);
            this.textBoxMaxLength.MaxLength = 32767;
            this.textBoxMaxLength.Name = "textBoxMaxLength";
            this.textBoxMaxLength.PasswordChar = '\0';
            this.textBoxMaxLength.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxMaxLength.SelectedText = "";
            this.textBoxMaxLength.SelectionLength = 0;
            this.textBoxMaxLength.SelectionStart = 0;
            this.textBoxMaxLength.ShortcutsEnabled = true;
            this.textBoxMaxLength.Size = new System.Drawing.Size(226, 23);
            this.textBoxMaxLength.TabIndex = 7;
            this.textBoxMaxLength.UseSelectable = true;
            this.textBoxMaxLength.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxMaxLength.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxMaxLength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxMaxLength_KeyPress);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(33, 143);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(77, 19);
            this.metroLabel3.TabIndex = 8;
            this.metroLabel3.Text = "Max Length";
            // 
            // checkBoxNullable
            // 
            this.checkBoxNullable.AutoSize = true;
            this.checkBoxNullable.Location = new System.Drawing.Point(31, 216);
            this.checkBoxNullable.Name = "checkBoxNullable";
            this.checkBoxNullable.Size = new System.Drawing.Size(67, 15);
            this.checkBoxNullable.TabIndex = 11;
            this.checkBoxNullable.Text = "Nullable";
            this.checkBoxNullable.UseSelectable = true;
            // 
            // checkBoxDatabaseGenerated
            // 
            this.checkBoxDatabaseGenerated.AutoSize = true;
            this.checkBoxDatabaseGenerated.Location = new System.Drawing.Point(31, 246);
            this.checkBoxDatabaseGenerated.Name = "checkBoxDatabaseGenerated";
            this.checkBoxDatabaseGenerated.Size = new System.Drawing.Size(128, 15);
            this.checkBoxDatabaseGenerated.TabIndex = 12;
            this.checkBoxDatabaseGenerated.Text = "Database Generated";
            this.checkBoxDatabaseGenerated.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(34, 11);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(95, 19);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Column Name";
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(335, 321);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(80, 35);
            this.buttonSave.TabIndex = 43;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseCustomBackColor = true;
            this.buttonSave.UseCustomForeColor = true;
            this.buttonSave.UseSelectable = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // UserControlFieldRapidEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.checkBoxDatabaseGenerated);
            this.Controls.Add(this.checkBoxNullable);
            this.Controls.Add(this.textBoxMaxLength);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.comboBoxFieldType);
            this.Controls.Add(this.textBoxFieldName);
            this.Controls.Add(this.metroLabel1);
            this.Name = "UserControlFieldRapidEntry";
            this.Size = new System.Drawing.Size(435, 377);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox textBoxFieldName;
        private MetroFramework.Controls.MetroComboBox comboBoxFieldType;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox textBoxMaxLength;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroCheckBox checkBoxNullable;
        private MetroFramework.Controls.MetroCheckBox checkBoxDatabaseGenerated;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton buttonSave;
    }
}
