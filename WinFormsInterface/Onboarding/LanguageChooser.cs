using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            return rbCroatian.Checked ? 1 : 0;
        }

        private void btContinue_Click(object sender, EventArgs e)
        {
            ((IWizard)this.FindForm()).nextStep();
        }
    }
}
