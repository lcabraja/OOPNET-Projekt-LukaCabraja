using DataHandler;
using DataHandler.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsInterface
{
    public partial class FavoriteRepresentation : Form
    {
        private bool dataLoaded = false;
        private bool keepAlive = false;
        private List<TeamResult> teams;

        public FavoriteRepresentation()
        {
            InitializeComponent();
        }
        private async void FavoriteRepresentation_Load(object sender, EventArgs e)
        {
            btFinish.Text = Program.LocalizedString("Finish");
            this.Text = Program.LocalizedString("FavoriteRepresentation");
            this.Name = Program.LocalizedString("FavoriteRepresentation");
            try
            {
                lbTooltip.Text = Program.LocalizedString("Fetching");
                var representations = await Fetch.FetchJsonFromUrlAsync<List<TeamResult>>(URL.Teams(Program.userSettings.GenderedRepresentationUrl()));

                teams = representations;
                cbRepresentation.DataSource = representations.OrderBy(x => x.FifaCode)
                                                             .Select(x => $"{x.Country} ({x.FifaCode})")
                                                             .ToList();
                dataLoaded = true;
                lbTooltip.Text = Program.LocalizedString("Done");
                if (Program.lastTeam != null)
                {
                    cbRepresentation.SelectedItem =
                        $"{Program.lastTeam.Country} ({Program.lastTeam.FifaCode})";
                }
            }
            catch (HttpStatusException ex)
            {
                lbTooltip.Text = Program.LocalizedString("Aborting");
                MessageBox.Show(ex.Message, ex.GetType().Name);
                Application.Exit();
            }
            catch (JsonException ex)
            {
                lbTooltip.Text = Program.LocalizedString("Aborting");
                MessageBox.Show(ex.Message, ex.GetType().Name);
                Application.Exit();
            }
            catch (Exception ex)
            {
                lbTooltip.Text = Program.LocalizedString("Aborting");
                MessageBox.Show(ex.Message, ex.GetType().Name);
                Application.Exit();
            }

        }

        private void btFinish_Click(object sender, EventArgs e)
        {
            if (dataLoaded)
                try
                {
                    File.WriteAllText(Program.REPRESENTATION, FindSelectedRepresentation());
                    Program.tryFifa_code();
                    keepAlive = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.LocalizedString("errorSaveRepresentation"));
                }
        }

        private string FindSelectedRepresentation()
        {
            var fifa_code = cbRepresentation.SelectedItem.ToString()
                                                         .Split(' ')
                                                         .Last()
                                                         .Substring(1, 3);
            return teams.Find(x => x.FifaCode == fifa_code).ToString();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
            if (keepAlive) return;

            // Application.Exit() did in fact not exit the application, only the form.
            System.Environment.Exit(0);
        }
    }
}
