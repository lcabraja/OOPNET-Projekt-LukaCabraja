using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataHandler;
using DataHandler.DataSources;
using DataHandler.Model;
using RestSharp;

namespace WinFormsInterface
{
    public partial class Onboarding : Form
    {
        public Onboarding()
        {
            InitializeComponent();
            //var asinc = async (o, e) =>
            //{
            //    JsonFromUrl json = new JsonFromUrl("http://worldcup.sfg.io/teams");
            //    var str = await json.GetJsonStringDataAsync();
            //    MessageBox.Show(str);
            //};
            //Locale locale = new Locale(System.IO.File.ReadAllText("locals.json"), "en");

            //lbFavoriteTeam.Text = locale["lbOnboardCombo"];
        }

        private void Onboarding_Load(object sender, EventArgs e)
        {
            //var client = new RestClient(Team.PrimaryURL);
            //var response = client.Execute(new RestRequest());
            //IList<string> teams = Team.FromJson(response.Content).Select(t => $"{t.Country} ({t.FifaCode})").ToList();
            //cbTeamSelector.DataSource = teams;

        }
    }
}
