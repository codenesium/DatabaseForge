namespace Codenesium.DatabaseForgeLib.UserControls
{
    partial class UserControlTableEntry
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
            this.textBoxTableName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // textBoxTableName
            // 
            // 
            // 
            // 
            this.textBoxTableName.CustomButton.Image = null;
            this.textBoxTableName.CustomButton.Location = new System.Drawing.Point(204, 1);
            this.textBoxTableName.CustomButton.Name = "";
            this.textBoxTableName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBoxTableName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxTableName.CustomButton.TabIndex = 1;
            this.textBoxTableName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxTableName.CustomButton.UseSelectable = true;
            this.textBoxTableName.CustomButton.Visible = false;
            this.textBoxTableName.Lines = new string[0];
            this.textBoxTableName.Location = new System.Drawing.Point(38, 48);
            this.textBoxTableName.MaxLength = 32767;
            this.textBoxTableName.Name = "textBoxTableName";
            this.textBoxTableName.PasswordChar = '\0';
            this.textBoxTableName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxTableName.SelectedText = "";
            this.textBoxTableName.SelectionLength = 0;
            this.textBoxTableName.SelectionStart = 0;
            this.textBoxTableName.ShortcutsEnabled = true;
            this.textBoxTableName.Size = new System.Drawing.Size(226, 23);
            this.textBoxTableName.TabIndex = 0;
            this.textBoxTableName.UseSelectable = true;
            this.textBoxTableName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxTableName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.textBoxTableName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxTableName_KeyPress);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(37, 23);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(81, 19);
            this.metroLabel1.TabIndex = 15;
            this.metroLabel1.Text = "Table Name";
            // 
            // UserControlTableEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxTableName);
            this.Controls.Add(this.metroLabel1);
            this.Name = "UserControlTableEntry";
            this.Size = new System.Drawing.Size(327, 137);
            this.VisibleChanged += new System.EventHandler(this.UserControlTableEntry_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox textBoxTableName;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
