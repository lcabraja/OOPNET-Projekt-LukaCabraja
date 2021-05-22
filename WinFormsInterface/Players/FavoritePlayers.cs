using DataHandler;
using DataHandler.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WinFormsInterface
{
    public partial class FavoritePlayers : Form
    {
        public FavoritePlayers()
        {
            InitializeComponent();
        }

        private List<Player> players = new List<Player>();
        internal FlowLayoutPanel departedFrom;
        internal FlowLayoutPanel goingTo;
        private bool dndSuccessful;

        private async void FavoritePlayers_Load(object sender, EventArgs e)
        {
            var url = URL.MatchesFiltered(
                Program.userSettings.GenderedRepresentation(),
                Program.lastTeam.FifaCode);
            //url = URL.Matches(Program.userSettings.GenderedRepresentation());
            try
            {
                var matches = await Fetch.FetchJsonFromUrlAsync<List<Match>>(url);
                if (matches[0].HomeTeam.Code == Program.lastTeam.FifaCode)
                {
                    matches[0].HomeTeamStatistics.StartingEleven.ForEach(players.Add);
                    matches[0].HomeTeamStatistics.Substitutes.ForEach(players.Add);
                }
                else if (matches[0].AwayTeam.Code == Program.lastTeam.FifaCode)
                {
                    matches[0].AwayTeamStatistics.StartingEleven.ForEach(players.Add);
                    matches[0].AwayTeamStatistics.Substitutes.ForEach(players.Add);
                }
                players.ForEach(x => flOtherPlayers.Controls.Add(PlayerControlFactory(x)));
            }
            catch (HttpStatusException ex)
            {
                MessageBox.Show(ex.Message, "Request Error");
                Application.Exit();
            }
            catch (JsonException ex)
            {
                MessageBox.Show(ex.Message, "Json parser exception");
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
                Application.Exit();
            }
        }

        private Control PlayerControlFactory(Player playerData)
        {
            PlayerControl playerControl = new PlayerControl(playerData);
            List<string> st = new List<string>();
            playerControl.BackColor = SystemColors.Control;
            //playerControl.MouseDown += new MouseEventHandler(PlayerControl_MouseDown); 
            //                      difference between += new MouseEventHandler(Method) and += Method ?
            playerControl.MouseDown += PlayerControl_MouseDown;
            playerControl.Controls.Cast<Control>()
                                  .ToList()
                                  .ForEach(x => x.MouseDown += Control_MouseDown);

            return playerControl;
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            Control control = sender as Control;
            if (control.Parent is PlayerControl player)
            {
                player.TriggerMouseDown(e);
            }
        }

        private void PlayerControl_MouseDown(object sender, MouseEventArgs e)
        {
            PlayerControl playerControl = sender as PlayerControl;
            departedFrom = playerControl.Parent as FlowLayoutPanel;

            bool isFavorite = playerControl.playerData.Favorite;
            bool wasSelected = playerControl.isSelected;
            dndSuccessful = false;
            playerControl.SetSelectionStatus(true);
            playerControl.DoDragDrop(isFavorite, DragDropEffects.Move);
            if (dndSuccessful)
            {
                MoveSelectedControls();
            }
            else
            {
                playerControl.SetSelectionStatus(!wasSelected);
            }
            ResetPanels();

        }
        internal void MoveSelectedControls()
        {
            FindSelectedPlayers().ForEach(MovePlayerControl);
        }
        private void MovePlayerControl(PlayerControl x)
        {
            bool favoriteStatus = (goingTo == flFavorites) ? true : false;
            if (favoriteStatus && flFavorites.Controls.Count > 2)
            {
                x.SetSelectionStatus(false);
                return;
            }
            x.SetFavoriteStatus(favoriteStatus);
            departedFrom.Controls.Remove(x);
            goingTo.Controls.Add(x);
            x.SetSelectionStatus(false);
        }

        private List<PlayerControl> FindSelectedPlayers()
        {
            List<PlayerControl> controls = new List<PlayerControl>();
            flOtherPlayers.Controls.Cast<PlayerControl>()
                                   .ToList()
                                   .FindAll(x => x.isSelected)
                                   .ForEach(controls.Add);
            flFavorites.Controls.Cast<PlayerControl>()
                                   .ToList()
                                   .FindAll(x => x.isSelected)
                                   .ForEach(controls.Add);
            return controls;
        }

        private void ResetPanels()
        {
            flFavorites.BackColor = SystemColors.Control;
            flOtherPlayers.BackColor = SystemColors.Control;
        }
        private void flowLayout_DragDrop(object sender, DragEventArgs e)
        {
            FlowLayoutPanel flowLayoutPanel = sender as FlowLayoutPanel;
            if (flowLayoutPanel == departedFrom)
            {
                return;
            }
            goingTo = flowLayoutPanel;
            dndSuccessful = true;
        }

        private void flowLayout_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            FlowLayoutPanel flowLayoutPanel = sender as FlowLayoutPanel;
            ResetPanels();
            flowLayoutPanel.BackColor = SystemColors.ControlDark;
        }
        private void tsMenuSettings_Click(object sender, EventArgs e)
        {

        }
        private void tsMenuRankedList_Click(object sender, EventArgs e)
        {

        }
    }
}
