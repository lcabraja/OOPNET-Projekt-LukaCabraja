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
    public partial class ChampionshipChooser : UserControl, IHasIntProperty
    {
        public ChampionshipChooser()
        {
            InitializeComponent();
        }

        public int ReturnValue()
        {
            return rbMale.Checked ? 1 : rbFemale.Checked ? 0 : -1;
        }

        private void btContinue_Click(object sender, EventArgs e)
        {
            if (ReturnValue() == -1)
            {
                return;
            }
            ((IWizard)this.FindForm()).nextStep();
        }

        private void ChampionshipChooser_Load(object sender, EventArgs e)
        {
            rbFemale.Text = Program.LocalizedString("Female");
            rbMale.Text = Program.LocalizedString("Male");
            Controls.OfType<RadioButton>()
                    .ToList()
                    .ForEach(p => { p.Checked = false; p.TabStop = false; });
        }
    }
}
