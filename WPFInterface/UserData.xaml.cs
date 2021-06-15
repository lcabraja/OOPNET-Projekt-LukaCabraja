using DataHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFInterface
{
    /// <summary>
    /// Interaction logic for UserData.xaml
    /// </summary>
    public partial class UserData : Window
    {
        public Player player1 { get; set; }
        public Match match1 { get; set; }
        public UserData(Player player, Match match)
        {
            InitializeComponent();
            this.Loaded += UserData_Loaded;
            this.KeyDown += UserData_KeyDown;
            player1 = player;
            match1 = match;
        }

        private void UserData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void UserData_Loaded(object sender, RoutedEventArgs e)
        {
            this.gridColumnImage.Background =
                new ImageBrush(
                    new BitmapImage(
                        new Uri(player1.PortraitPath ?? System.IO.Path.GetFullPath("defaultpicture.jpg")
                )));
            SetLabels();
        }

        private void SetLabels()
        {
            lbName.Content = player1.Name;
            lbNumber.Content = $"#{player1.ShirtNumber}";
            lbPosition.Content = player1.Position;
            lbCaptain.Content = player1.Captain ? "Captain" : "Player";
            lbGoals.Content = $"Goals: {EventCounter(TypeOfEvent.Goal)}";
            lbYellowCards.Content = $"Yellow Cards: {EventCounter(TypeOfEvent.YellowCard)}";
        }

        private int EventCounter(TypeOfEvent eventToCount)
        {
            List<TeamEvent> events = null;
            
            if (match1.HomeTeamStatistics.StartingEleven.Contains(player1) || match1.HomeTeamStatistics.Substitutes.Contains(player1))
                events = match1.HomeTeamEvents;
            if (match1.AwayTeamStatistics.StartingEleven.Contains(player1) || match1.AwayTeamStatistics.Substitutes.Contains(player1))
                events = match1.AwayTeamEvents;

            return events.Where(x => x.Player == player1.Name && x.TypeOfEvent == eventToCount).Count();
        }
    }
}