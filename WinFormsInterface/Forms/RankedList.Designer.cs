
namespace WinFormsInterface
{
    partial class RankedList
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
            this.dgRanks = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgRanks)).BeginInit();
            this.SuspendLayout();
            // 
            // dgRanks
            // 
            this.dgRanks.AllowUserToAddRows = false;
            this.dgRanks.AllowUserToDeleteRows = false;
            this.dgRanks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgRanks.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgRanks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRanks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgRanks.Location = new System.Drawing.Point(0, 0);
            this.dgRanks.MultiSelect = false;
            this.dgRanks.Name = "dgRanks";
            this.dgRanks.ReadOnly = true;
            this.dgRanks.RowHeadersVisible = false;
            this.dgRanks.RowTemplate.Height = 25;
            this.dgRanks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgRanks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRanks.Size = new System.Drawing.Size(800, 450);
            this.dgRanks.TabIndex = 0;
            // 
            // RankedList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgRanks);
            this.Name = "RankedList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RankedList";
            this.Load += new System.EventHandler(this.RankedList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgRanks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgRanks;
    }
}