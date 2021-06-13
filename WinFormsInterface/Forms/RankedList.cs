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
using WinFormsInterface.Properties;

namespace WinFormsInterface
{
    public partial class RankedList : Form
    {
        internal List<Player> players;
        internal string FifaCode;

        public RankedList(List<Player> players, string fifaCode)
        {
            this.players = players;
            FifaCode = fifaCode;
            InitializeComponent();
        }

        private void RankedList_Load(object sender, EventArgs e)
        {
            try
            {
                var bigdata = Fetch.FetchJsonFromUrl<List<Match>>(URL.MatchesFiltered(Program.userSettings.GenderedRepresentationUrl(), FifaCode));
                dgRanks.DataSource = SortedData(bigdata);

                dgRanks.Columns[0].Name = "Player Portrait";
                dgRanks.Columns[1].Name = "Player Name";
                dgRanks.Columns[2].Name = "Yellow Cards";
                dgRanks.Columns[2].ValueType = typeof(int);
                dgRanks.Columns[3].Name = "Goals";
                dgRanks.Columns[3].ValueType = typeof(int);

                int width = 0;
                width += dgRanks.Columns[0].Width;
                width += dgRanks.Columns[1].Width;
                width += dgRanks.Columns[2].Width;
                width += dgRanks.Columns[3].Width;
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
            var events = new List<TeamEvent>();
            bigdata.Where(x => x.AwayTeam.Code == FifaCode).ToList().ForEach(x => x.AwayTeamEvents.ForEach(events.Add));
            bigdata.Where(x => x.HomeTeam.Code == FifaCode).ToList().ForEach(x => x.HomeTeamEvents.ForEach(events.Add));

            var sortedResults = new List<SortedResult>();
            foreach (Player player in players)
            {
                sortedResults.Add(new SortedResult
                {
                    Portrait = player.Portrait as Image ?? Resources.defaultpicture,
                    FullName = player.Name,
                    GoalsScored = events.Where(x => x.Player == player.Name && x.TypeOfEvent == TypeOfEvent.Goal).Count(),
                    YellowCards = events.Where(x => x.Player == player.Name && x.TypeOfEvent == TypeOfEvent.YellowCard).Count()
                });
            }

            return sortedResults;
        }
    }
}
