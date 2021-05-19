using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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
            InitializeWizard();
        }

        private void InitializeWizard()
        {
            
        }

        //private Action doNextStep;
        private int currentStep = 0;
        UserSettings us;

        private UserSettings newUserSettings;
        private IDictionary<int, int> results;

        public void nextStep(/*Action next*/)
        {
            results[tcWizard.SelectedIndex] = 
                tcWizard.TabPages[tcWizard.SelectedIndex].Controls.OfType<IHasIntProperty>()
                .ToList()[tcWizard.SelectedIndex].ReturnValue();
            if (tcWizard.SelectedIndex == tcWizard.TabCount - 1) {
                finishOnboarding();
                return;
            }
            MessageBox.Show(tcWizard.SelectedIndex.ToString());
            MessageBox.Show(((tcWizard.SelectedIndex + 1 <= tcWizard.TabCount) ?
                             tcWizard.SelectedIndex + 1 : tcWizard.SelectedIndex).ToString());
            tcWizard.SelectedIndex = (tcWizard.SelectedIndex + 1 <= tcWizard.TabCount) ?
                             tcWizard.SelectedIndex + 1 : tcWizard.SelectedIndex;
        }

        private void finishOnboarding()
        {
            //var leag = (UserSettings.League)results[0];
            //var lang = (UserSettings.Language)results[1];
            var b4 = new UserSettings(results[0], results[1]).ToString();
            var inbetween = JsonConvert.DeserializeObject<UserSettings>(b4);
            var after = inbetween.ToString();
            MessageBox.Show($"{b4}\n{inbetween}\n{after}");

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

        private void tcWizard_Selected(object sender, TabControlEventArgs e)
        {
            
        }
    }
}
