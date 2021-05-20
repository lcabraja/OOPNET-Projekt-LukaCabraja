using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataHandler;
using DataHandler.Model;
using Newtonsoft.Json;
using RestSharp;

namespace WinFormsInterface
{
    public partial class Onboarding : Form, IWizard
    {
        public Onboarding()
        {
            InitializeComponent();
        }

        //private Action doNextStep;
        private int currentStep = 0;
        UserSettings us;

        private UserSettings newUserSettings;
        private IDictionary<int, int> results;

        public void nextStep(/*Action next*/)
        {
            var page = tcWizard.TabPages[tcWizard.SelectedIndex].Controls.OfType<IHasIntProperty>().First();
            var newValue = page.ReturnValue();
            results[tcWizard.SelectedIndex] = newValue;
            if (tcWizard.SelectedIndex == tcWizard.TabCount - 1)
            {
                finishOnboarding();
                return;
            }
            tcWizard.SelectedIndex = (tcWizard.SelectedIndex + 1 <= tcWizard.TabCount) ?
                             tcWizard.SelectedIndex + 1 : tcWizard.SelectedIndex;
        }

        private void finishOnboarding()
        {
            var user = new UserSettings(results[0], results[1]).ToString();
            try
            {
                File.WriteAllText(Program.DIR, user.ToString());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private async void Onboarding_Load(object sender, EventArgs e)
        {
            var tp = new TabPage();
            tp.Controls.Add(new ChampionshipChooser());
            tp.Text = "Championship"; // TODO: localization
            tcWizard.TabPages.Add(tp);
            tp = new TabPage();
            tp.Controls.Add(new LanguageChooser());
            tp.Text = "Language"; // TODO: localization
            tcWizard.TabPages.Add(tp);

            results = new Dictionary<int, int>();
        }

        private void tcWizard_Deselecting(object sender, TabControlCancelEventArgs e)
        {
            if (-1 == tcWizard.TabPages[tcWizard.SelectedIndex].Controls
                              .OfType<IHasIntProperty>()
                              .First()
                              .ReturnValue())
            {
                e.Cancel = true;
            }
        }
    }
}
