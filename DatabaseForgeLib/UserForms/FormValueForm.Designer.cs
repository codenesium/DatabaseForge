namespace Codenesium.DatabaseForgeLib.UserForms
{
    partial class FormValueForm
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
            this.textBoxValue = new MetroFramework.Controls.MetroTextBox();
            this.labelValue = new System.Windows.Forms.Label();
            this.buttonSave = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // textBoxValue
            // 
            // 
            // 
            // 
            this.textBoxValue.CustomButton.Image = null;
            this.textBoxValue.CustomButton.Location = new System.Drawing.Point(136, 1);
            this.textBoxValue.CustomButton.Name = "";
            this.textBoxValue.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.textBoxValue.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.textBoxValue.CustomButton.TabIndex = 1;
            this.textBoxValue.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.textBoxValue.CustomButton.UseSelectable = true;
            this.textBoxValue.CustomButton.Visible = false;
            this.textBoxValue.Lines = new string[0];
            this.textBoxValue.Location = new System.Drawing.Point(23, 84);
            this.textBoxValue.MaxLength = 32767;
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.PasswordChar = '\0';
            this.textBoxValue.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.textBoxValue.SelectedText = "";
            this.textBoxValue.SelectionLength = 0;
            this.textBoxValue.SelectionStart = 0;
            this.textBoxValue.ShortcutsEnabled = true;
            this.textBoxValue.Size = new System.Drawing.Size(158, 23);
            this.textBoxValue.TabIndex = 28;
            this.textBoxValue.UseSelectable = true;
            this.textBoxValue.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.textBoxValue.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(20, 68);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(56, 13);
            this.labelValue.TabIndex = 29;
            this.labelValue.Text = "labelValue";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(97, 119);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(84, 23);
            this.buttonSave.TabIndex = 30;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseSelectable = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormValueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 165);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.textBoxValue);
            this.Name = "FormValueForm";
            this.Text = "ValueForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox textBoxValue;
        private System.Windows.Forms.Label labelValue;
        private MetroFramework.Controls.MetroButton buttonSave;
    }
}