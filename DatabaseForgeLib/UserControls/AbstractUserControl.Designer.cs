namespace Codenesium.DatabaseForgeLib.UserControls
{
    partial class AbstractUserControl
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
            this.labelUserMessage = new MetroFramework.Controls.MetroLabel();
            this.progressSpinnerDefault = new MetroFramework.Controls.MetroProgressSpinner();
            this.SuspendLayout();
            // 
            // labelUserMessage
            // 
            this.labelUserMessage.AutoSize = true;
            this.labelUserMessage.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.labelUserMessage.Location = new System.Drawing.Point(20, 526);
            this.labelUserMessage.Name = "labelUserMessage";
            this.labelUserMessage.Size = new System.Drawing.Size(85, 25);
            this.labelUserMessage.TabIndex = 1;
            this.labelUserMessage.Text = "Messages";
            this.labelUserMessage.UseCustomForeColor = true;
            this.labelUserMessage.Visible = false;
            // 
            // progressSpinnerDefault
            // 
            this.progressSpinnerDefault.Location = new System.Drawing.Point(742, 3);
            this.progressSpinnerDefault.Maximum = 100;
            this.progressSpinnerDefault.Name = "progressSpinnerDefault";
            this.progressSpinnerDefault.Size = new System.Drawing.Size(55, 37);
            this.progressSpinnerDefault.TabIndex = 2;
            this.progressSpinnerDefault.UseSelectable = true;
            this.progressSpinnerDefault.Visible = false;
            // 
            // AbstractUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressSpinnerDefault);
            this.Controls.Add(this.labelUserMessage);
            this.Name = "AbstractUserControl";
            this.Size = new System.Drawing.Size(800, 500);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroLabel labelUserMessage;
        private MetroFramework.Controls.MetroProgressSpinner progressSpinnerDefault;
    }
}
