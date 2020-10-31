namespace Codenesium.DatabaseForgeLib.UserControls
{
    partial class UserControlGenerate
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
            this.textBoxOutput = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // textBoxOutput
            // 
            // 
            // 
            // 
            this.textBoxOutput.CustomButton.Image = null;
            this.textBoxOutput.CustomButton.Location = new System.Drawing.Point(59, 1);
            this.textBoxOutput.CustomButton.Name = "";
            this.textBoxOutput.CustomButton.Size = new System.Drawing.Size(375, 375);
            this.textBoxOutput.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxOutput.CustomButton.TabIndex = 1;
            this.textBoxOutput.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxOutput.CustomButton.UseSelectable = true;
            this.textBoxOutput.CustomButton.Visible = false;
            this.textBoxOutput.Lines = new string[0];
            this.textBoxOutput.Location = new System.Drawing.Point(0, 0);
            this.textBoxOutput.MaxLength = 32767;
            this.textBoxOutput.Multiline = true;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.PasswordChar = '\0';
            this.textBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxOutput.SelectedText = "";
            this.textBoxOutput.SelectionLength = 0;
            this.textBoxOutput.SelectionStart = 0;
            this.textBoxOutput.ShortcutsEnabled = true;
            this.textBoxOutput.Size = new System.Drawing.Size(435, 377);
            this.textBoxOutput.TabIndex = 28;
            this.textBoxOutput.UseSelectable = true;
            this.textBoxOutput.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxOutput.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // UserControlGenerate
            // 
            this.Controls.Add(this.textBoxOutput);
            this.Name = "UserControlGenerate";
            this.Size = new System.Drawing.Size(435, 377);
            this.ResumeLayout(false);

        }


        #endregion
        private MetroFramework.Controls.MetroTextBox textBoxOutput;
    }
}
