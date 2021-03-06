using System;
using System.Collections.Generic;
using System.Text;

namespace DataHandler.Model
{
    public partial class Team
    {
        public override string ToString()
            => $"ID: {Id}, Country: {Country}, AlternateName: {AlternateName}, FifaCode: {FifaCode}, GroupID: {GroupId}, GroupLetter: {GroupLetter}";
    }
    public partial class Result
    {
        public override string ToString()
            => $"Team, Wins: {Wins}, Draws: {Draws}, Losses: {Losses}, GamesPlayed: {GamesPlayed}, Points: {Points}, GoalsFor: {GoalsFor}, GoalsAgainst: {GoalsAgainst}, GoalDifferential: {GoalDifferential}";
    }
    public partial class GroupResult
    {
        public override string ToString() => $"Id: {Id}, Letter: {Letter}, OrderedTeams.Count: {OrderedTeams.Count}";
    }
    public partial class Match
    {
        public override string ToString()
            => $"Venue: {Venue}, Location: {Location}, Status: {Status}, Time: {Time}, FifaID: {FifaId}, Weather: {Weather}, Attendance: {Attendance}, Officials.Count: {Officials.Count}, StageName: {StageName}, HomeTeamCountry: {HomeTeamCountry}, AwayTeamCountry: {AwayTeamCountry}, Datetime: {Datetime.DateTime}, Winner: {Winner}, WinnerCode: {WinnerCode}, HomeTeam: [{HomeTeam}], AwayTeam: [{AwayTeam}], HomeTeamEvents.Count: {HomeTeamEvents.Count}, AwayTeamEvents.Count: {AwayTeamEvents.Count}, LastEventUpdateAt: {LastEventUpdateAt.DateTime}, LastScoreUpdateAt: {LastScoreUpdateAt.DateTime}";
    }

    public partial class MatchTeam
    {
        public override string ToString() 
            => $"Country: {Country}, Code: {Code}, Goals: {Goals}, Penalties: {Penalties}";
    }

    public partial class TeamEvent
    {
        public override string ToString() 
            => $"ID: {Id}, TypeOfEvent: {TypeOfEvent}, Player: {Player}, Time: {Time}";
    }

    public partial class TeamStatistics
    {
        public override string ToString() 
            => $"Country: {Country}, AttemptsOnGoal: {AttemptsOnGoal}, OnTarget: {OnTarget}, OffTarget: {OffTarget}, Blocked: {Blocked}, Corners: {Corners}, Offsides: {Offsides}, BallPossession: {BallPossession}, PassAccuracy: {PassAccuracy}, NumPasses: {NumPasses}, PassesCompleted: {PassesCompleted}, DistanceCovered: {DistanceCovered}, Tackles: {Tackles}, Clearances: {Clearances}, YellowCards: {YellowCards}, RedCards: {RedCards}, FoulsCommitted: {FoulsCommitted}, Tactics: {Tactics}";
    }

    public partial class Player
    {
        public override string ToString() 
            => $"Name: {Name}, Captain: {Captain}, ShirtNumber: {ShirtNumber}, Position: {Position}";
    }

    public partial class Weather
    {
        public override string ToString() 
            => $"Humidity: {Humidity}, TempCelsius: {TempCelsius}, TempFarenheit: {TempFarenheit}, WindSpeed: {WindSpeed}, Description: {Description}";
    }
}
