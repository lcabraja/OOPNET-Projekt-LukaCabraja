
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
            this.lbFavoriteTeam = new System.Windows.Forms.Label();
            this.tcWizard = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // lbFavoriteTeam
            // 
            this.lbFavoriteTeam.AutoSize = true;
            this.lbFavoriteTeam.Location = new System.Drawing.Point(13, 13);
            this.lbFavoriteTeam.Name = "lbFavoriteTeam";
            this.lbFavoriteTeam.Size = new System.Drawing.Size(0, 15);
            this.lbFavoriteTeam.TabIndex = 1;
            // 
            // tcWizard
            // 
            this.tcWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcWizard.Location = new System.Drawing.Point(0, 0);
            this.tcWizard.Name = "tcWizard";
            this.tcWizard.SelectedIndex = 0;
            this.tcWizard.Size = new System.Drawing.Size(304, 171);
            this.tcWizard.TabIndex = 2;
            this.tcWizard.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcWizard_Selected);
            // 
            // Onboarding
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 171);
            this.Controls.Add(this.tcWizard);
            this.Controls.Add(this.lbFavoriteTeam);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(320, 210);
            this.Name = "Onboarding";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Onboarding";
            this.Load += new System.EventHandler(this.Onboarding_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbFavoriteTeam;
        private System.Windows.Forms.TabControl tcWizard;
    }
}

