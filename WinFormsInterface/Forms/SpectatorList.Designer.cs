
namespace WinFormsInterface
{
    partial class SpectatorList
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
            this.dgSpectators = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgSpectators)).BeginInit();
            this.SuspendLayout();
            // 
            // dgRanks
            // 
            this.dgSpectators.AllowUserToAddRows = false;
            this.dgSpectators.AllowUserToDeleteRows = false;
            this.dgSpectators.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgSpectators.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgSpectators.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSpectators.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSpectators.Location = new System.Drawing.Point(0, 0);
            this.dgSpectators.MultiSelect = false;
            this.dgSpectators.Name = "dgSpectators";
            this.dgSpectators.ReadOnly = true;
            this.dgSpectators.RowHeadersVisible = false;
            this.dgSpectators.RowTemplate.Height = 25;
            this.dgSpectators.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgSpectators.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSpectators.Size = new System.Drawing.Size(800, 450);
            this.dgSpectators.TabIndex = 0;
            // 
            // SpectatorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgSpectators);
            this.Name = "SpectatorList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpectatorList";
            this.Load += new System.EventHandler(this.SpectatorList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgSpectators)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgSpectators;
    }
}