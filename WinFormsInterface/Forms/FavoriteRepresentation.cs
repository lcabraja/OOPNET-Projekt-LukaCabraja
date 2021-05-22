using DataHandler;
using DataHandler.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsInterface
{
    public partial class FavoriteRepresentation : Form
    {
        private bool dataLoaded = false;
        private List<TeamResult> teams;
        public FavoriteRepresentation()
        {
            InitializeComponent();
        }
        private async void FavoriteRepresentation_Load(object sender, EventArgs e)
        {
            lbTooltip.Text = "Fetching...";
            try
            {
                var representations = await Fetch.FetchJsonFromUrlAsync<List<TeamResult>>(URL.Teams(Program.userSettings.GenderedRepresentationUrl()));

                teams = representations;
                cbRepresentation.DataSource = representations.OrderBy(x => x.FifaCode)
                                                             .Select(x => $"{x.Country} ({x.FifaCode})")
                                                             .ToList();
                dataLoaded = true;
                lbTooltip.Text = "Done.";
                if (Program.lastTeam != null)
                {
                    cbRepresentation.SelectedItem =
                        $"{Program.lastTeam.Country} ({Program.lastTeam.FifaCode})";
                }
            }
            catch (HttpStatusException ex)
            {
                lbTooltip.Text = "Aborting...";
                MessageBox.Show(ex.Message, ex.GetType().Name);
                Application.Exit();
            }
            catch (JsonException ex)
            {
                lbTooltip.Text = "Aborting...";
                MessageBox.Show(ex.Message, ex.GetType().Name);
                Application.Exit();
            }
            catch (Exception ex)
            {
                lbTooltip.Text = "Aborting...";
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
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Could not save current representation");
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

        private void lbTooltip_Click(object sender, EventArgs e)
        {

        }
    }
}
