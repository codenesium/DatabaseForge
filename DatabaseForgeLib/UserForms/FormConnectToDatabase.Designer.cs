namespace Codenesium.DatabaseForgeLib.UserForms
{
    partial class FormConnectToDatabase
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConnectToDatabase));
			this.comboBoxProvider = new MetroFramework.Controls.MetroComboBox();
			this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
			this.textBoxInstance = new MetroFramework.Controls.MetroTextBox();
			this.labelUserMessage = new MetroFramework.Controls.MetroLabel();
			this.comboBoxDatabases = new MetroFramework.Controls.MetroComboBox();
			this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
			this.progressSpinnerDefault = new MetroFramework.Controls.MetroProgressSpinner();
			this.buttonSave = new MetroFramework.Controls.MetroButton();
			this.textBoxPassword = new MetroFramework.Controls.MetroTextBox();
			this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
			this.textBoxUsername = new MetroFramework.Controls.MetroTextBox();
			this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
			this.labelProject = new MetroFramework.Controls.MetroLabel();
			this.buttonTestConnection = new System.Windows.Forms.Button();
			this.checkBoxWindowsAuth = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// comboBoxProvider
			// 
			this.comboBoxProvider.FormattingEnabled = true;
			this.comboBoxProvider.ItemHeight = 23;
			this.comboBoxProvider.Items.AddRange(new object[] {
            "MSSQL",
            "PostgreSQL"});
			this.comboBoxProvider.Location = new System.Drawing.Point(62, 118);
			this.comboBoxProvider.Name = "comboBoxProvider";
			this.comboBoxProvider.Size = new System.Drawing.Size(216, 29);
			this.comboBoxProvider.TabIndex = 36;
			this.comboBoxProvider.UseSelectable = true;
			this.comboBoxProvider.SelectedIndexChanged += new System.EventHandler(this.ComboBoxProvider_SelectedIndexChanged);
			// 
			// metroLabel4
			// 
			this.metroLabel4.AutoSize = true;
			this.metroLabel4.Location = new System.Drawing.Point(62, 96);
			this.metroLabel4.Name = "metroLabel4";
			this.metroLabel4.Size = new System.Drawing.Size(59, 19);
			this.metroLabel4.TabIndex = 49;
			this.metroLabel4.Text = "Provider";
			// 
			// textBoxInstance
			// 
			// 
			// 
			// 
			this.textBoxInstance.CustomButton.Image = null;
			this.textBoxInstance.CustomButton.Location = new System.Drawing.Point(192, 1);
			this.textBoxInstance.CustomButton.Name = "";
			this.textBoxInstance.CustomButton.Size = new System.Drawing.Size(23, 23);
			this.textBoxInstance.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.textBoxInstance.CustomButton.TabIndex = 1;
			this.textBoxInstance.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.textBoxInstance.CustomButton.UseSelectable = true;
			this.textBoxInstance.CustomButton.Visible = false;
			this.textBoxInstance.Lines = new string[] {
        "localhost"};
			this.textBoxInstance.Location = new System.Drawing.Point(62, 177);
			this.textBoxInstance.MaxLength = 32767;
			this.textBoxInstance.Name = "textBoxInstance";
			this.textBoxInstance.PasswordChar = '\0';
			this.textBoxInstance.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.textBoxInstance.SelectedText = "";
			this.textBoxInstance.SelectionLength = 0;
			this.textBoxInstance.SelectionStart = 0;
			this.textBoxInstance.ShortcutsEnabled = true;
			this.textBoxInstance.Size = new System.Drawing.Size(216, 25);
			this.textBoxInstance.TabIndex = 37;
			this.textBoxInstance.Text = "localhost";
			this.textBoxInstance.UseSelectable = true;
			this.textBoxInstance.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.textBoxInstance.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// labelUserMessage
			// 
			this.labelUserMessage.AutoSize = true;
			this.labelUserMessage.FontSize = MetroFramework.MetroLabelSize.Tall;
			this.labelUserMessage.Location = new System.Drawing.Point(14, 497);
			this.labelUserMessage.Name = "labelUserMessage";
			this.labelUserMessage.Size = new System.Drawing.Size(112, 25);
			this.labelUserMessage.TabIndex = 48;
			this.labelUserMessage.Text = "UserMessage";
			this.labelUserMessage.UseCustomForeColor = true;
			this.labelUserMessage.Visible = false;
			this.labelUserMessage.WrapToLine = true;
			// 
			// comboBoxDatabases
			// 
			this.comboBoxDatabases.FormattingEnabled = true;
			this.comboBoxDatabases.ItemHeight = 23;
			this.comboBoxDatabases.Location = new System.Drawing.Point(62, 372);
			this.comboBoxDatabases.Name = "comboBoxDatabases";
			this.comboBoxDatabases.Size = new System.Drawing.Size(216, 29);
			this.comboBoxDatabases.TabIndex = 40;
			this.comboBoxDatabases.UseSelectable = true;
			// 
			// metroLabel3
			// 
			this.metroLabel3.AutoSize = true;
			this.metroLabel3.Location = new System.Drawing.Point(62, 350);
			this.metroLabel3.Name = "metroLabel3";
			this.metroLabel3.Size = new System.Drawing.Size(63, 19);
			this.metroLabel3.TabIndex = 47;
			this.metroLabel3.Text = "Database";
			// 
			// progressSpinnerDefault
			// 
			this.progressSpinnerDefault.Location = new System.Drawing.Point(223, 61);
			this.progressSpinnerDefault.Maximum = 100;
			this.progressSpinnerDefault.Name = "progressSpinnerDefault";
			this.progressSpinnerDefault.Size = new System.Drawing.Size(55, 37);
			this.progressSpinnerDefault.TabIndex = 45;
			this.progressSpinnerDefault.UseSelectable = true;
			this.progressSpinnerDefault.Visible = false;
			// 
			// buttonSave
			// 
			this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
			this.buttonSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.buttonSave.ForeColor = System.Drawing.Color.White;
			this.buttonSave.Location = new System.Drawing.Point(155, 451);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(123, 35);
			this.buttonSave.TabIndex = 41;
			this.buttonSave.Text = "Select";
			this.buttonSave.UseCustomBackColor = true;
			this.buttonSave.UseCustomForeColor = true;
			this.buttonSave.UseSelectable = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// textBoxPassword
			// 
			// 
			// 
			// 
			this.textBoxPassword.CustomButton.Image = null;
			this.textBoxPassword.CustomButton.Location = new System.Drawing.Point(192, 1);
			this.textBoxPassword.CustomButton.Name = "";
			this.textBoxPassword.CustomButton.Size = new System.Drawing.Size(23, 23);
			this.textBoxPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.textBoxPassword.CustomButton.TabIndex = 1;
			this.textBoxPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.textBoxPassword.CustomButton.UseSelectable = true;
			this.textBoxPassword.CustomButton.Visible = false;
			this.textBoxPassword.Lines = new string[0];
			this.textBoxPassword.Location = new System.Drawing.Point(62, 311);
			this.textBoxPassword.MaxLength = 32767;
			this.textBoxPassword.Name = "textBoxPassword";
			this.textBoxPassword.PasswordChar = '*';
			this.textBoxPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.textBoxPassword.SelectedText = "";
			this.textBoxPassword.SelectionLength = 0;
			this.textBoxPassword.SelectionStart = 0;
			this.textBoxPassword.ShortcutsEnabled = true;
			this.textBoxPassword.Size = new System.Drawing.Size(216, 25);
			this.textBoxPassword.TabIndex = 39;
			this.textBoxPassword.UseSelectable = true;
			this.textBoxPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.textBoxPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			this.textBoxPassword.Leave += new System.EventHandler(this.TextBoxPassword_Leave);
			// 
			// metroLabel2
			// 
			this.metroLabel2.AutoSize = true;
			this.metroLabel2.Location = new System.Drawing.Point(62, 289);
			this.metroLabel2.Name = "metroLabel2";
			this.metroLabel2.Size = new System.Drawing.Size(63, 19);
			this.metroLabel2.TabIndex = 44;
			this.metroLabel2.Text = "Password";
			// 
			// textBoxUsername
			// 
			// 
			// 
			// 
			this.textBoxUsername.CustomButton.Image = null;
			this.textBoxUsername.CustomButton.Location = new System.Drawing.Point(192, 1);
			this.textBoxUsername.CustomButton.Name = "";
			this.textBoxUsername.CustomButton.Size = new System.Drawing.Size(23, 23);
			this.textBoxUsername.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.textBoxUsername.CustomButton.TabIndex = 1;
			this.textBoxUsername.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.textBoxUsername.CustomButton.UseSelectable = true;
			this.textBoxUsername.CustomButton.Visible = false;
			this.textBoxUsername.Lines = new string[0];
			this.textBoxUsername.Location = new System.Drawing.Point(62, 243);
			this.textBoxUsername.MaxLength = 32767;
			this.textBoxUsername.Name = "textBoxUsername";
			this.textBoxUsername.PasswordChar = '\0';
			this.textBoxUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.textBoxUsername.SelectedText = "";
			this.textBoxUsername.SelectionLength = 0;
			this.textBoxUsername.SelectionStart = 0;
			this.textBoxUsername.ShortcutsEnabled = true;
			this.textBoxUsername.Size = new System.Drawing.Size(216, 25);
			this.textBoxUsername.TabIndex = 38;
			this.textBoxUsername.UseSelectable = true;
			this.textBoxUsername.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
			this.textBoxUsername.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			// 
			// metroLabel1
			// 
			this.metroLabel1.AutoSize = true;
			this.metroLabel1.Location = new System.Drawing.Point(62, 221);
			this.metroLabel1.Name = "metroLabel1";
			this.metroLabel1.Size = new System.Drawing.Size(68, 19);
			this.metroLabel1.TabIndex = 43;
			this.metroLabel1.Text = "Username";
			// 
			// labelProject
			// 
			this.labelProject.AutoSize = true;
			this.labelProject.Location = new System.Drawing.Point(62, 155);
			this.labelProject.Name = "labelProject";
			this.labelProject.Size = new System.Drawing.Size(55, 19);
			this.labelProject.TabIndex = 42;
			this.labelProject.Text = "Instance";
			// 
			// buttonTestConnection
			// 
			this.buttonTestConnection.Image = global::Codenesium.DatabaseForgeLib.Properties.Resources.connect;
			this.buttonTestConnection.Location = new System.Drawing.Point(250, 409);
			this.buttonTestConnection.Name = "buttonTestConnection";
			this.buttonTestConnection.Size = new System.Drawing.Size(28, 23);
			this.buttonTestConnection.TabIndex = 50;
			this.buttonTestConnection.UseVisualStyleBackColor = true;
			this.buttonTestConnection.Click += new System.EventHandler(this.ButtonTestConnection_Click);
			// 
			// checkBoxWindowsAuth
			// 
			this.checkBoxWindowsAuth.AutoSize = true;
			this.checkBoxWindowsAuth.Location = new System.Drawing.Point(62, 269);
			this.checkBoxWindowsAuth.Name = "checkBoxWindowsAuth";
			this.checkBoxWindowsAuth.Size = new System.Drawing.Size(163, 17);
			this.checkBoxWindowsAuth.TabIndex = 51;
			this.checkBoxWindowsAuth.Text = "Use Windows Authentication";
			this.checkBoxWindowsAuth.UseVisualStyleBackColor = true;
			this.checkBoxWindowsAuth.CheckedChanged += new System.EventHandler(this.CheckBoxWindowsAuth_CheckedChanged);
			// 
			// FormConnectToDatabase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(352, 539);
			this.Controls.Add(this.checkBoxWindowsAuth);
			this.Controls.Add(this.buttonTestConnection);
			this.Controls.Add(this.comboBoxProvider);
			this.Controls.Add(this.metroLabel4);
			this.Controls.Add(this.textBoxInstance);
			this.Controls.Add(this.labelUserMessage);
			this.Controls.Add(this.comboBoxDatabases);
			this.Controls.Add(this.metroLabel3);
			this.Controls.Add(this.progressSpinnerDefault);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.textBoxPassword);
			this.Controls.Add(this.metroLabel2);
			this.Controls.Add(this.textBoxUsername);
			this.Controls.Add(this.metroLabel1);
			this.Controls.Add(this.labelProject);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormConnectToDatabase";
			this.ShowInTaskbar = false;
			this.Text = "Connect to Database";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox comboBoxProvider;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroTextBox textBoxInstance;
        private MetroFramework.Controls.MetroLabel labelUserMessage;
        private MetroFramework.Controls.MetroComboBox comboBoxDatabases;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroProgressSpinner progressSpinnerDefault;
        private MetroFramework.Controls.MetroButton buttonSave;
        private MetroFramework.Controls.MetroTextBox textBoxPassword;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox textBoxUsername;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel labelProject;
        private System.Windows.Forms.Button buttonTestConnection;
		private System.Windows.Forms.CheckBox checkBoxWindowsAuth;
	}
}