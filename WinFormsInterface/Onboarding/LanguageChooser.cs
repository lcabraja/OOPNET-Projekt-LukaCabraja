using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsInterface
{
    public partial class LanguageChooser : UserControl, IHasIntProperty
    {
        public LanguageChooser()
        {
            InitializeComponent();
        }

        public int ReturnValue()
        {
            return rbCroatian.Checked ? 1 : rbEnglish.Checked ? 0 : -1;
        }

        private void btContinue_Click(object sender, EventArgs e)
        {
            if (ReturnValue() == -1)
            {
                return;
            }
            ((IWizard)this.FindForm()).nextStep();
        }

        private void LanguageChooser_Load(object sender, EventArgs e)
        {
            Controls.OfType<RadioButton>()
                    .ToList()
                    .ForEach(p => p.TabStop = false);
        }
    }
}
