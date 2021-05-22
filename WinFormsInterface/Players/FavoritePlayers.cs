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
using System.IO;

namespace WinFormsInterface
{
    public partial class FavoritePlayers : Form
    {
        public FavoritePlayers()
        {
            InitializeComponent();
        }

        private Match match;
        private List<Player> players = new List<Player>();
        internal FlowLayoutPanel departedFrom;
        internal FlowLayoutPanel goingTo;
        private bool dndSuccessful;

        private async void FavoritePlayers_Load(object sender, EventArgs e)
        {
            flOtherPlayers.Controls.Clear();
            flFavorites.Controls.Clear();
            players.Clear();
            try
            {
                string path = Program.userSettings.GenderedRepresentationFilePath() + Program.lastTeam.FifaCode + ".json";
                if (File.Exists(path))
                {
                    players = (await Fetch.FetchJsonFromFileAsync<List<Player>>(path));
                }
                else
                {
                    var url = URL.MatchesFiltered(Program.userSettings.GenderedRepresentationUrl(), Program.lastTeam.FifaCode);
                    match = (await Fetch.FetchJsonFromUrlAsync<List<Match>>(url)).First();
                    if (match.HomeTeam.Code == Program.lastTeam.FifaCode)
                    {
                        match.HomeTeamStatistics.StartingEleven.ForEach(players.Add);
                        match.HomeTeamStatistics.Substitutes.ForEach(players.Add);
                    }
                    else if (match.AwayTeam.Code == Program.lastTeam.FifaCode)
                    {
                        match.AwayTeamStatistics.StartingEleven.ForEach(players.Add);
                        match.AwayTeamStatistics.Substitutes.ForEach(players.Add);
                    }
                }

                players.ForEach(x =>
                {
                    var playerControl = PlayerControlFactory(x);
                    if ((bool)x.Favorite)
                    {
                        flFavorites.Controls.Add(playerControl);
                    }
                    else
                    {
                        flOtherPlayers.Controls.Add(playerControl);
                    }
                });
                SortPlayers(flFavorites);
                SortPlayers(flOtherPlayers);
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

        private void SortPlayers(FlowLayoutPanel playerPanel)
        {
            List<PlayerControl> playerControls = playerPanel.Controls.Cast<PlayerControl>().ToList();
            playerControls.Sort((a, b) =>
            {
                if (a.playerData.Captain || b.playerData.Captain)
                {
                    return -a.playerData.Captain.CompareTo(b.playerData.Captain);
                }
                return a.playerData.Name.Split(' ').Last().CompareTo(b.playerData.Name.Split(' ').Last());
            });
            playerPanel.Controls.Clear();
            playerControls.ForEach(playerPanel.Controls.Add);
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
            SortPlayers(flFavorites);
            SortPlayers(flOtherPlayers);
            SaveState();

        }

        private void SaveState()
        {
            try
            {
                string path = Program.userSettings.GenderedRepresentationFilePath() + Program.lastTeam.FifaCode + ".json";
                File.WriteAllText(path, JsonConvert.SerializeObject(players));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Could not save positions...");
            }
        }

        internal void MoveSelectedControls()
        {
            FindSelectedPlayers().ForEach(MovePlayerControl);
        }
        private void MovePlayerControl(PlayerControl movingPlayerControl)
        {
            bool favoriteStatus = (goingTo == flFavorites) ? true : false;
            if (favoriteStatus && flFavorites.Controls.Count > 2)
            {
                movingPlayerControl.SetSelectionStatus(false);
                return;
            }
            movingPlayerControl.SetFavoriteStatus(favoriteStatus);
            players.Find(x => x.ShirtNumber == movingPlayerControl.playerData.ShirtNumber).Favorite = favoriteStatus;
            departedFrom.Controls.Remove(movingPlayerControl);
            goingTo.Controls.Add(movingPlayerControl);
            movingPlayerControl.SetSelectionStatus(false);
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
            Onboarding onboarding = new Onboarding();
            onboarding.ShowDialog();
            this.OnLoad(new EventArgs());

        }
        private void tsMenuRankedList_Click(object sender, EventArgs e)
        {

        }
    }
}
