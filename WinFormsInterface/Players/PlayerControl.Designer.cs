
namespace WinFormsInterface
{
    partial class PlayerControl
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
            this.components = new System.ComponentModel.Container();
            this.lbPlayerName = new System.Windows.Forms.Label();
            this.lbNumber = new System.Windows.Forms.Label();
            this.cbIsCaptain = new System.Windows.Forms.CheckBox();
            this.pnIconPanel = new System.Windows.Forms.Panel();
            this.cbSelected = new System.Windows.Forms.CheckBox();
            this.playerPortrait = new System.Windows.Forms.PictureBox();
            this.lbFavorite = new System.Windows.Forms.Label();
            this.cmMove = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMoveTo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsToFavorites = new System.Windows.Forms.ToolStripMenuItem();
            this.tsToOther = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.pnIconPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerPortrait)).BeginInit();
            this.cmMove.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbPlayerName
            // 
            this.lbPlayerName.AutoSize = true;
            this.lbPlayerName.Location = new System.Drawing.Point(18, 109);
            this.lbPlayerName.Margin = new System.Windows.Forms.Padding(0);
            this.lbPlayerName.Name = "lbPlayerName";
            this.lbPlayerName.Size = new System.Drawing.Size(60, 15);
            this.lbPlayerName.TabIndex = 1;
            this.lbPlayerName.Text = "Pero Peric";
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Location = new System.Drawing.Point(1, 109);
            this.lbNumber.Margin = new System.Windows.Forms.Padding(0);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(19, 15);
            this.lbNumber.TabIndex = 2;
            this.lbNumber.Text = "00";
            // 
            // cbIsCaptain
            // 
            this.cbIsCaptain.AutoSize = true;
            this.cbIsCaptain.Enabled = false;
            this.cbIsCaptain.Location = new System.Drawing.Point(18, 146);
            this.cbIsCaptain.Margin = new System.Windows.Forms.Padding(0);
            this.cbIsCaptain.Name = "cbIsCaptain";
            this.cbIsCaptain.Size = new System.Drawing.Size(69, 19);
            this.cbIsCaptain.TabIndex = 5;
            this.cbIsCaptain.Text = "Kapetan";
            this.cbIsCaptain.UseVisualStyleBackColor = true;
            // 
            // pnIconPanel
            // 
            this.pnIconPanel.Controls.Add(this.cbSelected);
            this.pnIconPanel.Controls.Add(this.playerPortrait);
            this.pnIconPanel.Location = new System.Drawing.Point(0, 0);
            this.pnIconPanel.Margin = new System.Windows.Forms.Padding(0);
            this.pnIconPanel.MaximumSize = new System.Drawing.Size(100, 100);
            this.pnIconPanel.MinimumSize = new System.Drawing.Size(100, 100);
            this.pnIconPanel.Name = "pnIconPanel";
            this.pnIconPanel.Size = new System.Drawing.Size(100, 100);
            this.pnIconPanel.TabIndex = 0;
            // 
            // cbSelected
            // 
            this.cbSelected.AutoSize = true;
            this.cbSelected.Location = new System.Drawing.Point(75, 8);
            this.cbSelected.Name = "cbSelected";
            this.cbSelected.Size = new System.Drawing.Size(15, 14);
            this.cbSelected.TabIndex = 1;
            this.cbSelected.UseVisualStyleBackColor = true;
            this.cbSelected.CheckedChanged += new System.EventHandler(this.cbSelected_CheckedChanged);
            // 
            // playerPortrait
            // 
            this.playerPortrait.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerPortrait.ErrorImage = null;
            this.playerPortrait.Image = global::WinFormsInterface.Properties.Resources.defaultpicture;
            this.playerPortrait.InitialImage = null;
            this.playerPortrait.Location = new System.Drawing.Point(0, 0);
            this.playerPortrait.MaximumSize = new System.Drawing.Size(100, 100);
            this.playerPortrait.MinimumSize = new System.Drawing.Size(100, 100);
            this.playerPortrait.Name = "playerPortrait";
            this.playerPortrait.Size = new System.Drawing.Size(100, 100);
            this.playerPortrait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playerPortrait.TabIndex = 0;
            this.playerPortrait.TabStop = false;
            // 
            // lbFavorite
            // 
            this.lbFavorite.AutoSize = true;
            this.lbFavorite.Location = new System.Drawing.Point(8, 147);
            this.lbFavorite.Margin = new System.Windows.Forms.Padding(0);
            this.lbFavorite.Name = "lbFavorite";
            this.lbFavorite.Size = new System.Drawing.Size(12, 15);
            this.lbFavorite.TabIndex = 6;
            this.lbFavorite.Text = "*";
            // 
            // cmMove
            // 
            this.cmMove.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMoveTo,
            this.tsAddPicture});
            this.cmMove.Name = "cmMove";
            this.cmMove.Size = new System.Drawing.Size(133, 48);
            // 
            // tsMoveTo
            // 
            this.tsMoveTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsToFavorites,
            this.tsToOther});
            this.tsMoveTo.Name = "tsMoveTo";
            this.tsMoveTo.Size = new System.Drawing.Size(132, 22);
            this.tsMoveTo.Text = "Pomakni";
            // 
            // tsToFavorites
            // 
            this.tsToFavorites.Name = "tsToFavorites";
            this.tsToFavorites.Size = new System.Drawing.Size(126, 22);
            this.tsToFavorites.Text = "u Favorite";
            this.tsToFavorites.Click += new System.EventHandler(this.tsToFavorites_Click);
            // 
            // tsToOther
            // 
            this.tsToOther.Name = "tsToOther";
            this.tsToOther.Size = new System.Drawing.Size(126, 22);
            this.tsToOther.Text = "u Ostale";
            this.tsToOther.Click += new System.EventHandler(this.tsToOther_Click);
            // 
            // tsAddPicture
            // 
            this.tsAddPicture.Name = "tsAddPicture";
            this.tsAddPicture.Size = new System.Drawing.Size(132, 22);
            this.tsAddPicture.Text = "Dodaj sliku";
            // 
            // PlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.cmMove;
            this.Controls.Add(this.cbIsCaptain);
            this.Controls.Add(this.lbFavorite);
            this.Controls.Add(this.lbNumber);
            this.Controls.Add(this.lbPlayerName);
            this.Controls.Add(this.pnIconPanel);
            this.Name = "PlayerControl";
            this.Size = new System.Drawing.Size(98, 168);
            this.pnIconPanel.ResumeLayout(false);
            this.pnIconPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerPortrait)).EndInit();
            this.cmMove.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbPlayerName;
        private System.Windows.Forms.Label lbNumber;
        private System.Windows.Forms.CheckBox cbIsCaptain;
        private System.Windows.Forms.Panel pnIconPanel;
        private System.Windows.Forms.PictureBox playerPortrait;
        private System.Windows.Forms.Label lbFavorite;
        private System.Windows.Forms.CheckBox cbSelected;
        private System.Windows.Forms.ContextMenuStrip cmMove;
        private System.Windows.Forms.ToolStripMenuItem tsMoveTo;
        private System.Windows.Forms.ToolStripMenuItem tsToFavorites;
        private System.Windows.Forms.ToolStripMenuItem tsToOther;
        private System.Windows.Forms.ToolStripMenuItem tsAddPicture;
    }
}
