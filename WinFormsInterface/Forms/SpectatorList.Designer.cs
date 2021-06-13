
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpectatorList));
            this.dgSpectators = new System.Windows.Forms.DataGridView();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMenuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuPageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuPreview = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgSpectators)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgSpectators
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
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_EndPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuPrint,
            this.tsMenuPageSetup,
            this.tsMenuPreview});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(144, 70);
            // 
            // tsMenuPrint
            // 
            this.tsMenuPrint.Name = "tsMenuPrint";
            this.tsMenuPrint.Size = new System.Drawing.Size(143, 22);
            this.tsMenuPrint.Text = "Print";
            // 
            // tsMenuPageSetup
            // 
            this.tsMenuPageSetup.Name = "tsMenuPageSetup";
            this.tsMenuPageSetup.Size = new System.Drawing.Size(143, 22);
            this.tsMenuPageSetup.Text = "Page Setup";
            // 
            // tsMenuPreview
            // 
            this.tsMenuPreview.Name = "tsMenuPreview";
            this.tsMenuPreview.Size = new System.Drawing.Size(143, 22);
            this.tsMenuPreview.Text = "Preview Print";
            // 
            // SpectatorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.dgSpectators);
            this.Name = "SpectatorList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpectatorList";
            this.Load += new System.EventHandler(this.SpectatorList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgSpectators)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgSpectators;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsMenuPrint;
        private System.Windows.Forms.ToolStripMenuItem tsMenuPageSetup;
        private System.Windows.Forms.ToolStripMenuItem tsMenuPreview;
    }
}