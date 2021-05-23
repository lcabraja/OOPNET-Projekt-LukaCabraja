
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavoriteRepresentation));
            this.cbRepresentation = new System.Windows.Forms.ComboBox();
            this.btFinish = new System.Windows.Forms.Button();
            this.lbTooltip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbRepresentation
            // 
            resources.ApplyResources(this.cbRepresentation, "cbRepresentation");
            this.cbRepresentation.FormattingEnabled = true;
            this.cbRepresentation.Name = "cbRepresentation";
            // 
            // btFinish
            // 
            resources.ApplyResources(this.btFinish, "btFinish");
            this.btFinish.Name = "btFinish";
            this.btFinish.UseVisualStyleBackColor = true;
            this.btFinish.Click += new System.EventHandler(this.btFinish_Click);
            // 
            // lbTooltip
            // 
            resources.ApplyResources(this.lbTooltip, "lbTooltip");
            this.lbTooltip.Name = "lbTooltip";
            // 
            // FavoriteRepresentation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbTooltip);
            this.Controls.Add(this.btFinish);
            this.Controls.Add(this.cbRepresentation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FavoriteRepresentation";
            this.Load += new System.EventHandler(this.FavoriteRepresentation_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRepresentation;
        private System.Windows.Forms.Button btFinish;
        private System.Windows.Forms.Label lbTooltip;
    }
}