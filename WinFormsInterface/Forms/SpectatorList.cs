using DataHandler;
using DataHandler.Model;
using Newtonsoft.Json;
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
    public partial class SpectatorList : Form
    {
        internal string FifaCode;
        public SpectatorList(string fifaCode)
        {
            FifaCode = fifaCode;
            InitializeComponent();
        }

        private async void SpectatorList_Load(object sender, EventArgs e)
        {
            try
            {
                this.Name = Program.LocalizedString("SpectatorList");
                this.Text = Program.LocalizedString("SpectatorList");
                this.CenterToScreen();
                var bigdata = await Fetch.FetchJsonFromUrlAsync<List<Match>>(URL.MatchesFiltered(Program.userSettings.GenderedRepresentationUrl(), FifaCode));
                dgSpectators.DataSource = SortedData(bigdata);

                dgSpectators.Columns[0].HeaderText = Program.LocalizedString("Venue");
                dgSpectators.Columns[1].HeaderText = Program.LocalizedString("Spectators");
                dgSpectators.Columns[2].HeaderText = Program.LocalizedString("HomeTeam");
                dgSpectators.Columns[3].HeaderText = Program.LocalizedString("GuestTeam");

                int width = 0;
                width += dgSpectators.Columns[0].Width;
                width += dgSpectators.Columns[1].Width;
                width += dgSpectators.Columns[2].Width;
                width += dgSpectators.Columns[3].Width;
                this.Width = width += 40;
            }
            catch (HttpStatusException ex)
            {
                MessageBox.Show(ex.Message, Program.LocalizedString("errorRequest"));
                Application.Exit();
            }
            catch (JsonException ex)
            {
                MessageBox.Show(ex.Message, Program.LocalizedString("errorRequest"));
                MessageBox.Show(ex.Message, Program.LocalizedString("errorJson"));
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
                Application.Exit();
            }
        }

        private object SortedData(List<Match> bigdata)
        {
            var spectatorData = new List<SortedSpectator>();
            foreach (var match in bigdata)
            {
                spectatorData.Add(new SortedSpectator
                {
                    Location = match.Location,
                    Visitors = (int)match.Attendance,
                    Guest = match.AwayTeam.Country,
                    Home = match.HomeTeamCountry
                });
            }

            return spectatorData;
        }

    }
}
