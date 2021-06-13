using DataHandler;
using DataHandler.Model;
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

        private void SpectatorList_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            var bigdata = Fetch.FetchJsonFromUrl<List<Match>>(URL.MatchesFiltered(Program.userSettings.GenderedRepresentationUrl(), FifaCode));
            dgSpectators.DataSource = SortedData(bigdata);

            dgSpectators.Columns[0].Name = "Venue";
            dgSpectators.Columns[1].Name = "Spectators";
            dgSpectators.Columns[2].Name = "Home Team";
            dgSpectators.Columns[3].Name = "Guest Team";

            int width = 0;
            width += dgSpectators.Columns[0].Width;
            width += dgSpectators.Columns[1].Width;
            width += dgSpectators.Columns[2].Width;
            width += dgSpectators.Columns[3].Width;
            this.Width = width += 40;
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
