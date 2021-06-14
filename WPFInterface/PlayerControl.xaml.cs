using DataHandler.Model;
using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        public Player player1 { get; set; }
        public PlayerControl(Player player)
        {
            InitializeComponent();
            player1 = player;
            this.Loaded += PlayerControl_Loaded;
        }

        private void PlayerControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.lbName.Content = player1.Name;
            this.lbNumber.Content = player1.ShirtNumber;
        }
    }
}
