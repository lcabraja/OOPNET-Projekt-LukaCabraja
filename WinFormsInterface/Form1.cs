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

namespace WinFormsInterface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += async (o, e) =>
            {
                JsonFromUrl json = new JsonFromUrl("http://worldcup.sfg.io/teams");
                var str = await json.GetJsonStringDataAsync();
                MessageBox.Show(str);
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
    }
}
