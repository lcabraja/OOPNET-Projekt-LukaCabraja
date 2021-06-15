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
using WinFormsInterface.Properties;
using Grumsom;
using DGVPrinterHelper;
namespace WinFormsInterface
{
    public partial class RankedList : Form
    {
        internal List<Player> players;
        internal string FifaCode;
        DGVPrinter printer = new DGVPrinter();

        public RankedList(List<Player> players, string fifaCode)
        {
            this.players = players;
            FifaCode = fifaCode;
            InitializeComponent();
            this.tsMenuPrint.Click += tsMenuPrint_Click;
            this.tsMenuPageSetup.Click += tsMenuPageSetup_Click;
            this.tsMenuPreview.Click += tsMenuPreview_Click;

            pageSetupDialog1.Document = printer.printDocument;
        }

        private async void RankedList_Load(object sender, EventArgs e)
        {
            try
            {
                this.Name = Program.LocalizedString("RankedList");
                this.Text = Program.LocalizedString("RankedList");

                this.tsMenuPrint.Text = Program.LocalizedString("tsMenuPrint");
                this.tsMenuPageSetup.Text = Program.LocalizedString("tsMenuPageSetup");
                this.tsMenuPreview.Text = Program.LocalizedString("tsMenuPreview");

                List<Match> bigdata = null;
                string uri = Program.CACHE + Program.userSettings.GenderedRepresentationUrl().Substring(7).Replace('\\', '-').Replace('/', '-') + FifaCode + ".json"; //checked 1
                if (File.Exists(uri))
                {
                    bigdata = await Fetch.FetchJsonFromFileAsync<List<Match>>(uri);
                }
                else
                {
                    bigdata = await Fetch.FetchJsonFromUrlAsync<List<Match>>(URL.MatchesFiltered(Program.userSettings.GenderedRepresentationUrl(), FifaCode));
                    File.WriteAllText(uri, JsonConvert.SerializeObject(bigdata));
                }
                dgRanks.DataSource = SortedData(bigdata);

                dgRanks.Columns[0].HeaderText = Program.LocalizedString("playerPortrait");
                dgRanks.Columns[1].HeaderText = Program.LocalizedString("playerName");
                dgRanks.Columns[2].HeaderText = Program.LocalizedString("yellowCards");
                dgRanks.Columns[2].ValueType = typeof(int);
                dgRanks.Columns[3].HeaderText = Program.LocalizedString("goals");
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
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //printer.printm
        }

        private void tsMenuPrint_Click(object sender, System.EventArgs e)
        {
            printer.PrintDataGridView(dgRanks);
        }
        private void tsMenuPageSetup_Click(object sender, System.EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }
        private void tsMenuPreview_Click(object sender, System.EventArgs e)
        {
            printer.PrintPreviewDataGridView(dgRanks);
        }
    }
}
