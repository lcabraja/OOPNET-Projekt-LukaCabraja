using DataHandler;
using DataHandler.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsInterface
{
    public partial class RankedList : Form
    {
        public RankedList()
        {
            InitializeComponent();
        }

        private void RankedList_Load(object sender, EventArgs e)
        {
            dgRanks.DataSource = Fetch.FetchJsonFromUrl<List<Match>>(URL.MatchesFiltered(Program.userSettings.GenderedRepresentationUrl(), "ENG"));
        }
    }
}
