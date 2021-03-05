using System;
using System.Collections.Generic;
using System.Text;

namespace DataHandler.Model
{
    public class Match
    {
        public string Venue { get; set; }
        public string Location { get; set; }
        public Status Status { get; set; }
        public string Time { get; set; }
        public int FifaId { get; set; }
        public Weather Weather { get; set; }
        public int Attendance { get; set; }
        public List<string> Officials { get; set; }
        public StageName StageName { get; set; }
        public string HomeTeamCountry { get; set; }
        public string AwayTeamCountry { get; set; }
        public DateTimeOffset Datetime { get; set; }
        public string Winner { get; set; }
        public string WinnerCode { get; set; }
        public MatchTeam HomeTeam { get; set; }
        public MatchTeam AwayTeam { get; set; }
        public List<TeamEvent> HomeTeamEvents { get; set; }
        public List<TeamEvent> AwayTeamEvents { get; set; }
        public TeamStatistics HomeTeamStatistics { get; set; }
        public TeamStatistics AwayTeamStatistics { get; set; }
        public DateTimeOffset LastEventUpdateAt { get; set; }
        public DateTimeOffset LastScoreUpdateAt { get; set; }
        public override string ToString() => $"Venue: {Venue}, Location: {Location}, Status: {Status}, Time: {Time}, FifaId: {FifaId}, Weather: {Weather}, Attendance: {Attendance}, StageName: {StageName}, HomeTeamCountry: {HomeTeamCountry}, AwayTeamCountry: {AwayTeamCountry}, Datetime: {Datetime}, Winner: {Winner}, WinnerCode: {WinnerCode}, HomeTeam: {HomeTeam}, AwayTeam: {AwayTeam}";
    }

    public class MatchTeam
    {
        public string Country { get; set; }
        public string Code { get; set; }
        public int Goals { get; set; }
        public int Penalties { get; set; }
        public override string ToString() => $"Country: {Country}, Code: {Code}, Goals: {Goals}, Penalties: {Penalties}";
    }

    public class TeamEvent
    {
        public int Id { get; set; }
        public TypeOfEvent TypeOfEvent { get; set; }
        public string Player { get; set; }
        public string Time { get; set; }
        public override string ToString() => $"Id: {Id}, TypeOfEvent: {TypeOfEvent}, Player: {Player}, Time: {Time}";
    }

    public class TeamStatistics
    {
        public string Country { get; set; }
        public int AttemptsOnGoal { get; set; }
        public int OnTarget { get; set; }
        public int OffTarget { get; set; }
        public int Blocked { get; set; }
        public int Corners { get; set; }
        public int Offsides { get; set; }
        public int BallPossession { get; set; }
        public int PassAccuracy { get; set; }
        public int NumPasses { get; set; }
        public int PassesCompleted { get; set; }
        public int DistanceCovered { get; set; }
        public int Tackles { get; set; }
        public int? Clearances { get; set; }
        public int? YellowCards { get; set; }
        public int RedCards { get; set; }
        public int? FoulsCommitted { get; set; }
        public Tactics Tactics { get; set; }
        public List<Player> StartingEleven { get; set; }
        public List<Player> Substitutes { get; set; }
        public override string ToString() => $"Country: {Country}, AttemptsOnGoal: {AttemptsOnGoal}, OnTarget: {OnTarget}, OffTarget: {OffTarget}, Blocked: {Blocked}, Corners: {Corners}, Offsides: {Offsides}, BallPossession: {BallPossession}, PassAccuracy: {PassAccuracy}, NumPasses: {NumPasses}, PassesCompleted: {PassesCompleted}, DistanceCovered: {DistanceCovered}, Tackles: {Tackles}, Clearances: {Clearances}, YellowCards: {YellowCards}, RedCards: {RedCards}, FoulsCommitted: {FoulsCommitted}, Tactics: {Tactics}";
    }

    public class Player
    {
        public string Name { get; set; }
        public bool Captain { get; set; }
        public int ShirtNumber { get; set; }
        public Position Position { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}, Captain: {Captain}, ShirtNumber: {ShirtNumber}, Position: {Position}";
        }
    }

    public class Weather
    {
        public int Humidity { get; set; }
        public int TempCelsius { get; set; }
        public int TempFarenheit { get; set; }
        public int WindSpeed { get; set; }
        public Description Description { get; set; }
        public override string ToString() => $"Humidity: {Humidity}, TempCelsius: {TempCelsius}, TempFarenheit: {TempFarenheit}, WindSpeed: {WindSpeed}, Description: {Description}";
    }

    public enum TypeOfEvent { Goal, GoalOwn, GoalPenalty, RedCard, SubstitutionIn, SubstitutionOut, YellowCard, YellowCardSecond };

    public enum Position { Defender, Forward, Goalie, Midfield };

    public enum Tactics { The433, The442, The451, The532, The541 };

    public enum StageName { Final, FirstStage, MatchForThirdPlace, QuarterFinal, RoundOf16, SemiFinal };

    public enum Status { Completed };

    public enum Description { Cloudy, CloudyNight, PartlyCloudy, PartlyCloudyNight, Sunny };
}
