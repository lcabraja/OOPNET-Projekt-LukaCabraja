
namespace WinFormsInterface
{
    partial class Onboarding
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbTeamSelector = new System.Windows.Forms.ComboBox();
            this.lbFavoriteTeam = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbTeamSelector
            // 
            this.cbTeamSelector.FormattingEnabled = true;
            this.cbTeamSelector.Location = new System.Drawing.Point(12, 80);
            this.cbTeamSelector.Name = "cbTeamSelector";
            this.cbTeamSelector.Size = new System.Drawing.Size(121, 23);
            this.cbTeamSelector.TabIndex = 0;
            // 
            // lbFavoriteTeam
            // 
            this.lbFavoriteTeam.AutoSize = true;
            this.lbFavoriteTeam.Location = new System.Drawing.Point(13, 13);
            this.lbFavoriteTeam.Name = "lbFavoriteTeam";
            this.lbFavoriteTeam.Size = new System.Drawing.Size(0, 15);
            this.lbFavoriteTeam.TabIndex = 1;
            // 
            // Onboarding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbFavoriteTeam);
            this.Controls.Add(this.cbTeamSelector);
            this.Name = "Onboarding";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Onboarding_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbTeamSelector;
        private System.Windows.Forms.Label lbFavoriteTeam;
    }
}

