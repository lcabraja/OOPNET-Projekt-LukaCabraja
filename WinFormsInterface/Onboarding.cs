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

        private void Onboarding_Load(object sender, EventArgs e)
        {
            //var url = URL.Teams(URL.F_BASE_URL);
            //var client = new RestClient(url);
            //client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            //var req = new RestRequest(Method.GET);
            //var res = client.Execute(req);
            //Console.WriteLine(res);


            //return;
            _ = Task.Run(async () =>
            {
                List<TeamResult> teams = await Fetch.FetchJsonFromUrlAsync<List<TeamResult>>(TeamResult.F_URL);
                cbTeamSelector.DataSource = teams.Select(x => x.Country);
            });
        }
    }
}
