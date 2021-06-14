using DataHandler;
using DataHandler.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            gridWhole.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"field-horizontal.png"))));
        }

        private async void LoadPlayers()
        {
            try
            {
                List<Player> playersHome = new List<Player>();
                List<Player> playersGuest = new List<Player>();
                Match match = new Match();
                string FifaCode = App.lastTeam.FifaCode;

                List<Match> bigdata = null;
                string uri = App.CACHE + App.userSettings.GenderedRepresentationUrl().Substring(7).Replace('\\', '-').Replace('/', '-') + App.fifaCodeHome + ".json"; //checked 1
                if (File.Exists(uri))
                {
                    bigdata = await Fetch.FetchJsonFromFileAsync<List<Match>>(uri);
                }
                else
                {
                    bigdata = await Fetch.FetchJsonFromUrlAsync<List<Match>>(URL.MatchesFiltered(App.userSettings.GenderedRepresentationUrl(), App.fifaCodeHome));
                    File.WriteAllText(uri, JsonConvert.SerializeObject(bigdata));
                }

                match = bigdata.Find(x => 
                    (x.HomeTeam.Code == App.fifaCodeHome && x.AwayTeam.Code == App.fifaCodeGuest) || 
                    (x.HomeTeam.Code == App.fifaCodeGuest && x.AwayTeam.Code == App.fifaCodeHome)
                );
                
                match.AwayTeamStatistics.Substitutes.ForEach(playersGuest.Add);
                match.AwayTeamStatistics.StartingEleven.ForEach(playersGuest.Add);
                match.HomeTeamStatistics.StartingEleven.ForEach(playersHome.Add);
                match.HomeTeamStatistics.Substitutes.ForEach(playersHome.Add);

                string pathHome = App.userSettings.GenderedRepresentationFilePath() + App.fifaCodeHome + ".json";
                if (File.Exists(pathHome))
                {
                    playersHome = await Fetch.FetchJsonFromFileAsync<List<Player>>(pathHome);
                }
                string pathGuest = App.userSettings.GenderedRepresentationFilePath() + App.fifaCodeGuest + ".json";
                if (File.Exists(pathGuest))
                {
                    playersGuest = await Fetch.FetchJsonFromFileAsync<List<Player>>(pathGuest);
                }

                playersGuest.ForEach(x => AddPlayerControl(x, gridGuest));
                playersHome.ForEach(x => AddPlayerControl(x, gridHome));
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
