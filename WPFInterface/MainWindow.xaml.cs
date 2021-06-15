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
using System.Windows.Media.Animation;
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
        private string fifaCodeHome = null;
        private string fifaCodeGuest = null;
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
            this.Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gridWhole.Background = new ImageBrush(new BitmapImage(new Uri(System.IO.Path.GetFullPath(@"field-horizontal.png"))));
            InitializeToNewSettings();
        }
        private void InitializeToNewSettings()
        {
            if (App.screenSize != null)
            {
                if (App.userSettings.SavedLeague == UserSettings.League.Female)
                    fifaCodeHome = App.fifaCodeHomeFemale;
                else
                    fifaCodeHome = App.fifaCodeHomeMale;
                if (App.userSettings.SavedLeague == UserSettings.League.Female)
                    fifaCodeGuest = App.fifaCodeGuestFemale;
                else
                    fifaCodeGuest = App.fifaCodeGuestMale;

                Size size = App.screenSize.GetSize();
                Width = size.Width;
                Height = size.Height;
                if (App.screenSize.ChosenSize == 0)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
            }
            SetRepresentationLabels();
            LoadPlayers();
        }
        private async void LoadPlayers()
        {
            List<Player> playersHome = new List<Player>();
            List<Player> playersGuest = new List<Player>();


            var match = await FindMatch(fifaCodeHome, fifaCodeGuest);

            match.AwayTeamStatistics.StartingEleven.ForEach(playersGuest.Add);
            match.HomeTeamStatistics.StartingEleven.ForEach(playersHome.Add);

            string pathHome = App.userSettings.GenderedRepresentationFilePath() + fifaCodeHome + ".json";
            if (File.Exists(pathHome))
            {
                (await Fetch.FetchJsonFromFileAsync<List<Player>>(pathHome)).ForEach(x =>
                {
                    var index = playersHome.FindIndex(y => y.ShirtNumber == x.ShirtNumber);
                    if (index >= 0 && index < playersHome.Count)
                        playersHome[index] = x;
                });
            }
            string pathGuest = App.userSettings.GenderedRepresentationFilePath() + fifaCodeGuest + ".json";
            if (File.Exists(pathGuest))
            {
                (await Fetch.FetchJsonFromFileAsync<List<Player>>(pathGuest)).ForEach(x =>
                {
                    var index = playersGuest.FindIndex(y => y.ShirtNumber == x.ShirtNumber);
                    if (index >= 0 && index < playersGuest.Count)
                        playersGuest[index] = x;
                });
            }
            ClearPlayers();
            playersGuest.ForEach(x => AddPlayerControl(x, gridGuest));
            playersHome.ForEach(x => AddPlayerControl(x, gridHome));
        }

        private async Task<Match> FindMatch(string fifaCodeHome, string fifaCodeGuest)
        {
            try
            {
                List<Match> bigdata = null;
                string uri = App.CACHE + App.userSettings.GenderedRepresentationUrl().Substring(7).Replace('\\', '-').Replace('/', '-') + fifaCodeHome + ".json"; //checked 1
                if (File.Exists(uri))
                {
                    bigdata = await Fetch.FetchJsonFromFileAsync<List<Match>>(uri);
                }
                else
                {
                    bigdata = await Fetch.FetchJsonFromUrlAsync<List<Match>>(URL.MatchesFiltered(App.userSettings.GenderedRepresentationUrl(), fifaCodeHome));
                    File.WriteAllText(uri, JsonConvert.SerializeObject(bigdata));
                }

                return bigdata.Find(x =>
                    (x.HomeTeam.Code == fifaCodeHome && x.AwayTeam.Code == fifaCodeGuest) ||
                    (x.HomeTeam.Code == fifaCodeGuest && x.AwayTeam.Code == fifaCodeHome)
                 );
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
            return null;
        }

        private void ClearPlayers()
        {
            stackGuestDefender.Children.Clear();
            stackHomeDefender.Children.Clear();
            stackGuestForward.Children.Clear();
            stackHomeForward.Children.Clear();
            stackGuestGoalie.Children.Clear();
            stackHomeGoalie.Children.Clear();
            stackGuestMidfield.Children.Clear();
            stackHomeMidfield.Children.Clear();
        }
        private void AddPlayerControl(Player player, Grid grid)
        {
            if (player == null)
                return;
            PlayerControl playerControl = new PlayerControl(player);
            playerControl.MouseDoubleClick += PlayerControl_MouseDoubleClick;

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
        private async void PlayerControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is PlayerControl pc)
            {
                var animation = new DoubleAnimation
                {
                    From = 0,
                    To = 360,
                    Duration = TimeSpan.FromMilliseconds(300d)
                };
                pc.RenderTransform.BeginAnimation(RotateTransform.AngleProperty, animation, System.Windows.Media.Animation.HandoffBehavior.Compose);
                UserData userData = new UserData(pc.player1, await FindMatch(fifaCodeHome, fifaCodeGuest));
                userData.ShowDialog();
            }
        }
        private async void SetRepresentationLabels()
        {
            try
            {
                teamResults = await Fetch.FetchJsonFromUrlAsync<List<TeamResult>>(URL.Teams(App.userSettings.GenderedRepresentationUrl()));
                SetLabels();
            }
            catch (HttpStatusException ex)
            {
                MessageBox.Show(App.LocalizedString("tooManyRequestsMessage"), App.LocalizedString("tooManyRequests"));
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
            lbNaziv.Content = $"{App.LocalizedString("Naziv")}: {myRep.Country}";
            lbFifaCode.Content = $"{App.LocalizedString("Fifakod")}: {myRep.FifaCode}";
            lbGamesPlayed.Content = $"{App.LocalizedString("GamesPlayed")}: {myRep.GamesPlayed}";
            lbGamesWon.Content = $"{App.LocalizedString("GamesWon")}: {myRep.Wins}";
            lbGamesLost.Content = $"{App.LocalizedString("GamesLost")}: {myRep.Losses}";
            lbGamesUndecided.Content = $"{App.LocalizedString("GamedTied")}: {myRep.Draws}";
            lbGoalsScored.Content = $"{App.LocalizedString("GoalsScored")}: {myRep.GoalsFor}";
            lbGoalsTaken.Content = $"{App.LocalizedString("GoalsTaken")}: {myRep.GoalsAgainst}";
            lbDiff.Content = $"{App.LocalizedString("GoalsDiff")}: {myRep.GoalDifferential}";
            btSettings.Content = App.LocalizedString("Settings");
            this.Title = App.LocalizedString("RepOverview");

        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (keepalive) return;
            Environment.Exit(0);
        }

        private void btSettings_Click(object sender, RoutedEventArgs e)
        {
            Startup startup = new Startup(true);
            startup.ShowDialog();
            InitializeToNewSettings();
        }
    }
}
