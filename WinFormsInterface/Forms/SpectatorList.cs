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
    public partial class SpectatorList : Form
    {
        internal string FifaCode;
        public SpectatorList(string fifaCode)
        {
            FifaCode = fifaCode;
            InitializeComponent();
            this.tsMenuPrint.Click += tsMenuPrint_Click;
            this.tsMenuPageSetup.Click += tsMenuPageSetup_Click;
            this.tsMenuPreview.Click += tsMenuPreview_Click;
        }

        private async void SpectatorList_Load(object sender, EventArgs e)
        {
            try
            {
                this.Name = Program.LocalizedString("SpectatorList");
                this.Text = Program.LocalizedString("SpectatorList");
                this.CenterToScreen();

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
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int height = dgSpectators.Height;
            dgSpectators.Height = dgSpectators.RowCount * dgSpectators.Rows[1].Height;
            var bitmap = new Bitmap(this.dgSpectators.Width, this.dgSpectators.Height);
            dgSpectators.DrawToBitmap(bitmap, new Rectangle(0, 0, this.dgSpectators.Width, this.dgSpectators.Height));
            dgSpectators.Height = height;
            e.Graphics.DrawImage(bitmap, new Point(e.MarginBounds.X, e.MarginBounds.Y));
        }

        private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
        private void tsMenuPrint_Click(object sender, System.EventArgs e)
        {
            printDialog1.ShowDialog();
        }
        private void tsMenuPageSetup_Click(object sender, System.EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }
        private void tsMenuPreview_Click(object sender, System.EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }
    }
}
