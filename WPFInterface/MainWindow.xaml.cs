using DataHandler;
using DataHandler.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool keepalive = false;
        private List<TeamResult> teamResults;
        private Image defaultImage;
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
            this.Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetRepresentationLabels();
            LoadPlayers();
            this.lbNaziv.Content = System.IO.Path.GetFullPath(@"Resources\field-horizontal.png");
            //defaultImage = Image. Properties.Resources.defaultpicture;
        }

        private async void LoadPlayers()
        {
            string FifaCode = App.lastTeam.FifaCode;
            List<Match> bigdata = await Fetch.FetchJsonFromUrlAsync<List<Match>>(URL.MatchesFiltered(App.userSettings.GenderedRepresentationUrl(), FifaCode));
            List<Player> playersHome = new List<Player>();
            List<Player> playersGuest = new List<Player>();

            var team = bigdata.Find(x => x.HomeTeam.Code == FifaCode);
            if (team == null)
            {
                team = bigdata.Find(x => x.AwayTeam.Code == FifaCode);
            }
            team.AwayTeamStatistics.Substitutes.ForEach(playersGuest.Add);
            team.AwayTeamStatistics.StartingEleven.ForEach(playersGuest.Add);
            team.HomeTeamStatistics.StartingEleven.ForEach(playersHome.Add);
            team.HomeTeamStatistics.Substitutes.ForEach(playersHome.Add);

            playersGuest.ForEach(x => AddPlayerControl(x, gridGuest));
            playersHome.ForEach(x => AddPlayerControl(x, gridHome));
        }
        private void AddPlayerControl(Player player, Grid grid)
        {
            if (player == null)
                return;
            PlayerControl playerControl = new PlayerControl(player);

            switch (player.Position)
            {
                case Position.Defender:
                    if (grid == gridGuest)
                    {
                        stackGuestDefender.Children.Add(playerControl);
                    }
                    else
                    {
                        stackHomeDefender.Children.Add(playerControl);
                    }
                    break;
                case Position.Forward:
                    if (grid == gridGuest)
                    {
                        stackGuestForward.Children.Add(playerControl);
                    }
                    else
                    {
                        stackHomeForward.Children.Add(playerControl);
                    }
                    break;
                case Position.Goalie:
                    if (grid == gridGuest)
                    {
                        stackGuestGoalie.Children.Add(playerControl);
                    }
                    else
                    {
                        stackHomeGoalie.Children.Add(playerControl);
                    }
                    break;
                case Position.Midfield:
                    if (grid == gridGuest)
                    {
                        stackGuestMidfield.Children.Add(playerControl);
                    }
                    else
                    {
                        stackHomeMidfield.Children.Add(playerControl);
                    }
                    break;
                default:
                    MessageBox.Show("helloo");
                    break;
            }
        }

        //private Image LoadImage(Player player)
        //{
        //    if (player.PortraitPath != null)
        //    {
        //        try
        //        {
        //            System.Drawing.Image portrait = System.Drawing.Image.FromFile(player.PortraitPath);
        //            return portrait;
        //        }
        //        catch (Exception)
        //        {
        //            return 
        //        }
        //    }
        //}

        //private object SortedData(List<Match> bigdata)
        //{
        //    var events = new List<Team>();
        //    bigdata.Where(x => x.AwayTeam.Code == FifaCode).ToList().ForEach(x => x.AwayTeamEvents.ForEach(events.Add));
        //    bigdata.Where(x => x.HomeTeam.Code == FifaCode).ToList().ForEach(x => x.HomeTeamEvents.ForEach(events.Add));

        //    var sortedResults = new List<SortedResult>();
        //    foreach (Player player in players)
        //    {
        //        sortedResults.Add(new SortedResult
        //        {
        //            Portrait = player.Portrait as Image ?? Resources.defaultpicture,
        //            FullName = player.Name,
        //            GoalsScored = events.Where(x => x.Player == player.Name && x.TypeOfEvent == TypeOfEvent.Goal).Count(),
        //            YellowCards = events.Where(x => x.Player == player.Name && x.TypeOfEvent == TypeOfEvent.YellowCard).Count()
        //        });
        //    }

        //    return sortedResults;
        //}

        private async void SetRepresentationLabels()
        {
            try
            {
                teamResults = await Fetch.FetchJsonFromUrlAsync<List<TeamResult>>(URL.Teams(App.userSettings.GenderedRepresentationUrl()));
                SetLabels();
            }
            catch (HttpStatusException ex)
            {
                MessageBox.Show(ex.Message, App.LocalizedString("errorRequest"));
                //Environment.Exit(ex.HResult);
            }
            catch (JsonException ex)
            {
                MessageBox.Show(ex.Message, App.LocalizedString("errorRequest"));
                MessageBox.Show(ex.Message, App.LocalizedString("errorJson"));
                Environment.Exit(ex.HResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
                Environment.Exit(ex.HResult);
            }
        }

        private void SetLabels()
        {
            TeamResult myRep = teamResults.Find(x => x.FifaCode == App.lastTeam.FifaCode);
            lbNaziv.Content = myRep.Country;
            lbFifaCode.Content = myRep.FifaCode;
            lbGamesPlayed.Content = myRep.GamesPlayed;
            lbGamesWon.Content = myRep.Wins;
            lbGamesLost.Content = myRep.Losses;
            lbGamesUndecided.Content = myRep.Draws;
            lbGoalsScored.Content = myRep.GoalsFor;
            lbGoalsTaken.Content = myRep.GoalsAgainst;
            lbDiff.Content = myRep.GoalDifferential;
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (keepalive) return;
            Environment.Exit(0);
        }
    }
}
