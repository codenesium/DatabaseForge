namespace Codenesium.DatabaseForgeLib.UserControls
{
    partial class UserControlSchemaEntry
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
            this.textBoxSchemaName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // textBoxSchemaName
            // 
            // 
            // 
            // 
            this.textBoxSchemaName.CustomButton.Image = null;
            this.textBoxSchemaName.CustomButton.Location = new System.Drawing.Point(204, 1);
            this.textBoxSchemaName.CustomButton.Name = "";
            this.textBoxSchemaName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBoxSchemaName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxSchemaName.CustomButton.TabIndex = 1;
            this.textBoxSchemaName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxSchemaName.CustomButton.UseSelectable = true;
            this.textBoxSchemaName.CustomButton.Visible = false;
            this.textBoxSchemaName.Lines = new string[0];
            this.textBoxSchemaName.Location = new System.Drawing.Point(38, 48);
            this.textBoxSchemaName.MaxLength = 32767;
            this.textBoxSchemaName.Name = "textBoxSchemaName";
            this.textBoxSchemaName.PasswordChar = '\0';
            this.textBoxSchemaName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxSchemaName.SelectedText = "";
            this.textBoxSchemaName.SelectionLength = 0;
            this.textBoxSchemaName.SelectionStart = 0;
            this.textBoxSchemaName.ShortcutsEnabled = true;
            this.textBoxSchemaName.Size = new System.Drawing.Size(226, 23);
            this.textBoxSchemaName.TabIndex = 14;
            this.textBoxSchemaName.UseSelectable = true;
            this.textBoxSchemaName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxSchemaName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
          
            this.textBoxSchemaName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSchemaName_KeyPress);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(37, 23);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(95, 19);
            this.metroLabel1.TabIndex = 15;
            this.metroLabel1.Text = "Schema Name";
            // 
            // UserControlSchemaEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxSchemaName);
            this.Controls.Add(this.metroLabel1);
            this.Name = "UserControlSchemaEntry";
            this.Size = new System.Drawing.Size(327, 137);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox textBoxSchemaName;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
