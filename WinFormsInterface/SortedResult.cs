using DataHandler.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace WinFormsInterface
{
    public class SortedResult
    {
        public Image Portrait { get; set; }
        public string FullName{ get; set; }
        public int YellowCards { get; set; }
        public int GoalsScored { get; set; }
    }
}
