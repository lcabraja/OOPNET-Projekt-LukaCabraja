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
using DataHandler.Model;
using RestSharp;

namespace WinFormsInterface
{
    public partial class Onboarding : Form
    {
        public Onboarding()
        {
            InitializeComponent();
            
        }

        private async void Onboarding_Load(object sender, EventArgs e)
        {
            //var client = new RestClient(Team.PrimaryURL);
            //var response = client.Execute(new RestRequest());
            List<TeamResult> teams = await Fetch.FetchJsonFromUrlAsync<List<TeamResult>>(TeamResult.F_URL);
            cbTeamSelector.DataSource = teams.Select(x => x.Country);
        }
    }
}
