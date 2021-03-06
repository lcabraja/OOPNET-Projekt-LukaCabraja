using System;
using System.Collections.Generic;
using System.Text;

namespace DataHandler.Model
{
    public partial class Team
    {
        public static readonly string PrimaryURL = "https://worldcup.sfg.io/teams";
    }
    public partial class Result
    {
        public static readonly string PrimaryURL = "http://worldcup.sfg.io/teams/results";
    }

    public partial class GroupResult
    {
        public static readonly string PrimaryURL = "http://worldcup.sfg.io/teams/group_results";
    }

    public partial class Match
    {
        public static readonly string PrimaryURL = "http://worldcup.sfg.io/matches";
    }
}