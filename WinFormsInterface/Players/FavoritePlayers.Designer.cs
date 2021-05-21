
namespace WinFormsInterface
{
    partial class FavoritePlayers
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
            this.tlMainDock = new System.Windows.Forms.TableLayoutPanel();
            this.msMenuBar = new System.Windows.Forms.MenuStrip();
            this.tlPlayers = new System.Windows.Forms.TableLayoutPanel();
            this.flFavorites = new System.Windows.Forms.FlowLayoutPanel();
            this.flOtherPlayers = new System.Windows.Forms.FlowLayoutPanel();
            this.tsMenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuRankedList = new System.Windows.Forms.ToolStripMenuItem();
            this.tlMainDock.SuspendLayout();
            this.msMenuBar.SuspendLayout();
            this.tlPlayers.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlMainDock
            // 
            this.tlMainDock.ColumnCount = 1;
            this.tlMainDock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMainDock.Controls.Add(this.msMenuBar, 0, 0);
            this.tlMainDock.Controls.Add(this.tlPlayers, 0, 1);
            this.tlMainDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlMainDock.Location = new System.Drawing.Point(0, 0);
            this.tlMainDock.Margin = new System.Windows.Forms.Padding(0);
            this.tlMainDock.Name = "tlMainDock";
            this.tlMainDock.RowCount = 2;
            this.tlMainDock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlMainDock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlMainDock.Size = new System.Drawing.Size(840, 742);
            this.tlMainDock.TabIndex = 0;
            // 
            // msMenuBar
            // 
            this.msMenuBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msMenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuSettings,
            this.tsMenuRankedList});
            this.msMenuBar.Location = new System.Drawing.Point(0, 0);
            this.msMenuBar.Name = "msMenuBar";
            this.msMenuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.msMenuBar.Size = new System.Drawing.Size(840, 40);
            this.msMenuBar.TabIndex = 1;
            this.msMenuBar.Text = "menuStrip1";
            // 
            // tlPlayers
            // 
            this.tlPlayers.ColumnCount = 2;
            this.tlPlayers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tlPlayers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 742F));
            this.tlPlayers.Controls.Add(this.flFavorites, 0, 0);
            this.tlPlayers.Controls.Add(this.flOtherPlayers, 1, 0);
            this.tlPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlPlayers.Location = new System.Drawing.Point(0, 40);
            this.tlPlayers.Margin = new System.Windows.Forms.Padding(0);
            this.tlPlayers.Name = "tlPlayers";
            this.tlPlayers.RowCount = 1;
            this.tlPlayers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlPlayers.Size = new System.Drawing.Size(840, 702);
            this.tlPlayers.TabIndex = 2;
            // 
            // flFavorites
            // 
            this.flFavorites.AllowDrop = true;
            this.flFavorites.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flFavorites.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flFavorites.Location = new System.Drawing.Point(0, 0);
            this.flFavorites.Margin = new System.Windows.Forms.Padding(0);
            this.flFavorites.Name = "flFavorites";
            this.flFavorites.Size = new System.Drawing.Size(106, 702);
            this.flFavorites.TabIndex = 0;
            this.flFavorites.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowLayout_DragDrop);
            this.flFavorites.DragOver += new System.Windows.Forms.DragEventHandler(this.flowLayout_DragOver);
            // 
            // flOtherPlayers
            // 
            this.flOtherPlayers.AllowDrop = true;
            this.flOtherPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flOtherPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flOtherPlayers.Location = new System.Drawing.Point(106, 0);
            this.flOtherPlayers.Margin = new System.Windows.Forms.Padding(0);
            this.flOtherPlayers.Name = "flOtherPlayers";
            this.flOtherPlayers.Size = new System.Drawing.Size(742, 702);
            this.flOtherPlayers.TabIndex = 1;
            this.flOtherPlayers.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowLayout_DragDrop);
            this.flOtherPlayers.DragOver += new System.Windows.Forms.DragEventHandler(this.flowLayout_DragOver);
            // 
            // tsMenuSettings
            // 
            this.tsMenuSettings.Name = "tsMenuSettings";
            this.tsMenuSettings.Size = new System.Drawing.Size(66, 36);
            this.tsMenuSettings.Text = "Postavke";
            this.tsMenuSettings.Click += tsMenuSettings_Click;

            // 
            // tsMenuRankedList
            // 
            this.tsMenuRankedList.Name = "tsMenuRankedList";
            this.tsMenuRankedList.Size = new System.Drawing.Size(70, 36);
            this.tsMenuRankedList.Text = "Rang lista";
            this.tsMenuRankedList.Click += tsMenuRankedList_Click;
            // 
            // FavoritePlayers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 742);
            this.Controls.Add(this.tlMainDock);
            this.MainMenuStrip = this.msMenuBar;
            this.MinimumSize = new System.Drawing.Size(856, 781);
            this.Name = "FavoritePlayers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FavoritePlayers";
            this.Load += new System.EventHandler(this.FavoritePlayers_Load);
            this.tlMainDock.ResumeLayout(false);
            this.tlMainDock.PerformLayout();
            this.msMenuBar.ResumeLayout(false);
            this.msMenuBar.PerformLayout();
            this.tlPlayers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlMainDock;
        private System.Windows.Forms.MenuStrip msMenuBar;
        private System.Windows.Forms.TableLayoutPanel tlPlayers;
        internal System.Windows.Forms.FlowLayoutPanel flFavorites;
        internal System.Windows.Forms.FlowLayoutPanel flOtherPlayers;
        private System.Windows.Forms.ToolStripMenuItem tsMenuSettings;
        private System.Windows.Forms.ToolStripMenuItem tsMenuRankedList;
    }
}