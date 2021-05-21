
namespace WinFormsInterface
{
    partial class FavoriteRepresentation
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
            this.cbRepresentation = new System.Windows.Forms.ComboBox();
            this.btFinish = new System.Windows.Forms.Button();
            this.lbTooltip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbRepresentation
            // 
            this.cbRepresentation.FormattingEnabled = true;
            this.cbRepresentation.Location = new System.Drawing.Point(13, 13);
            this.cbRepresentation.Name = "cbRepresentation";
            this.cbRepresentation.Size = new System.Drawing.Size(279, 23);
            this.cbRepresentation.TabIndex = 0;
            // 
            // btFinish
            // 
            this.btFinish.Location = new System.Drawing.Point(217, 136);
            this.btFinish.Name = "btFinish";
            this.btFinish.Size = new System.Drawing.Size(75, 23);
            this.btFinish.TabIndex = 1;
            this.btFinish.Text = "Nastavi";
            this.btFinish.UseVisualStyleBackColor = true;
            this.btFinish.Click += new System.EventHandler(this.btFinish_Click);
            // 
            // lbTooltip
            // 
            this.lbTooltip.AutoSize = true;
            this.lbTooltip.Location = new System.Drawing.Point(13, 43);
            this.lbTooltip.Name = "lbTooltip";
            this.lbTooltip.Size = new System.Drawing.Size(140, 15);
            this.lbTooltip.TabIndex = 2;
            this.lbTooltip.Text = "Connection interrupted...";
            this.lbTooltip.Click += new System.EventHandler(this.lbTooltip_Click);
            // 
            // FavoriteRepresentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 171);
            this.Controls.Add(this.lbTooltip);
            this.Controls.Add(this.btFinish);
            this.Controls.Add(this.cbRepresentation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(320, 210);
            this.Name = "FavoriteRepresentation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FavoriteRepresentation";
            this.Load += new System.EventHandler(this.FavoriteRepresentation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRepresentation;
        private System.Windows.Forms.Button btFinish;
        private System.Windows.Forms.Label lbTooltip;
    }
}