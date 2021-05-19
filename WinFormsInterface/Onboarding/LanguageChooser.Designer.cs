
namespace WinFormsInterface
{
    partial class LanguageChooser
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
            this.btContinue = new System.Windows.Forms.Button();
            this.rbCroatian = new System.Windows.Forms.RadioButton();
            this.rbEnglish = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btContinue
            // 
            this.btContinue.Location = new System.Drawing.Point(211, 113);
            this.btContinue.Name = "btContinue";
            this.btContinue.Size = new System.Drawing.Size(75, 23);
            this.btContinue.TabIndex = 5;
            this.btContinue.Text = "Dalje";
            this.btContinue.UseVisualStyleBackColor = true;
            this.btContinue.Click += new System.EventHandler(this.btContinue_Click);
            // 
            // rbCroatian
            // 
            this.rbCroatian.AutoSize = true;
            this.rbCroatian.Location = new System.Drawing.Point(15, 41);
            this.rbCroatian.Name = "rbCroatian";
            this.rbCroatian.Size = new System.Drawing.Size(68, 19);
            this.rbCroatian.TabIndex = 4;
            this.rbCroatian.TabStop = true;
            this.rbCroatian.Text = "Hrvatski";
            this.rbCroatian.UseVisualStyleBackColor = true;
            // 
            // rbEnglish
            // 
            this.rbEnglish.AutoSize = true;
            this.rbEnglish.Location = new System.Drawing.Point(15, 16);
            this.rbEnglish.Name = "rbEnglish";
            this.rbEnglish.Size = new System.Drawing.Size(68, 19);
            this.rbEnglish.TabIndex = 3;
            this.rbEnglish.TabStop = true;
            this.rbEnglish.Text = "Engleski";
            this.rbEnglish.UseVisualStyleBackColor = true;
            // 
            // LanguageChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btContinue);
            this.Controls.Add(this.rbCroatian);
            this.Controls.Add(this.rbEnglish);
            this.Name = "LanguageChooser";
            this.Size = new System.Drawing.Size(300, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btContinue;
        private System.Windows.Forms.RadioButton rbCroatian;
        private System.Windows.Forms.RadioButton rbEnglish;
    }
}
