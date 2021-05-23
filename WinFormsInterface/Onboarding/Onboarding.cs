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

        private IDictionary<int, int> results;
        private bool keepAlive = false;

        public void nextStep()
        {
            var page = tcWizard.TabPages[tcWizard.SelectedIndex].Controls.OfType<IHasIntProperty>().First();
            var newValue = page.ReturnValue();
            if (newValue == -1)
            {
                return;
            }
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
            UserSettings user;
            try
            {
                user = new UserSettings(results[0], results[1]);
                Program.UpdateUser(user);
                try
                {
                    File.WriteAllText(Program.USER, user.ToString());
                    keepAlive = true;
                }
                catch (Exception)
                {
                    EndOnboarding();
                }
            }
            catch (Exception)
            {
                keepAlive = false;
                EndOnboarding();
            }
            finally
            {
                this.Close();
            }
        }

        private static void EndOnboarding()
        {
            MessageBox.Show("Error during onboarding.");
            try
            {
                File.Delete(Program.USER);
            }
            catch
            {
                MessageBox.Show("Please manually delete user.json", "User settings error.");
            }
            finally
            {
                Application.Exit();
            }
        }

        private void Onboarding_Load(object sender, EventArgs e)
        {
            this.Text = Program.LocalizedString("Onboarding");
            var tp = new TabPage();
            tp.Controls.Add(new ChampionshipChooser());
            tp.Text = Program.LocalizedString("Championship");
            tcWizard.TabPages.Add(tp);
            tp = new TabPage();
            tp.Controls.Add(new LanguageChooser());
            tp.Text = Program.LocalizedString("Language");
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
            var page = tcWizard.TabPages[tcWizard.SelectedIndex].Controls.OfType<IHasIntProperty>().First();
            var newValue = page.ReturnValue();
            if (newValue == -1)
            {
                return;
            }
            results[tcWizard.SelectedIndex] = newValue;
            if (tcWizard.SelectedIndex == tcWizard.TabCount - 1)
            {
                finishOnboarding();
                return;
            }
        }
        private void onboarding_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (!Program.firstOnboarding)
                        keepAlive = true;
                    this.Close();
                    break;
                case Keys.Enter:
                    nextStep();
                    break;
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (keepAlive) return;

            // Confirm user wants to close
            Application.Exit();
        }
    }
}
