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
            tsMenuRankedList.Text = Program.LocalizedString("RankedList");
            tsMenuSettings.Text = Program.LocalizedString("Settings");
            this.Text = Program.LocalizedString("FavoritePlayers");
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

                foreach (Player x in players)
                {
                    PlayerControl playerControl;
                    if (x.PortraitPath != null)
                    {
                        try
                        {
                            Image portrait = Image.FromFile(x.PortraitPath);
                            x.Portrait = portrait;
                            playerControl = PlayerControlFactory(x, portrait);
                        }
                        catch (Exception)
                        {
                            playerControl = PlayerControlFactory(x);
                        }
                    }
                    else
                    {
                        playerControl = PlayerControlFactory(x);
                    }
                    if ((bool)x.Favorite)
                    {
                        flFavorites.Controls.Add(playerControl);
                    }
                    else
                    {
                        flOtherPlayers.Controls.Add(playerControl);
                    }
                }
                SortPlayers(flFavorites);
                SortPlayers(flOtherPlayers);
            }
            catch (HttpStatusException ex)
            {
                MessageBox.Show(ex.Message, Program.LocalizedString("errorRequest"));
                Application.Exit();
            }
            catch (JsonException ex)
            {
                MessageBox.Show(ex.Message, Program.LocalizedString("errorRequest"));
                MessageBox.Show(ex.Message, Program.LocalizedString("errorJson"));
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

        private PlayerControl PlayerControlFactory(Player playerData)
        {
            PlayerControl playerControl = new PlayerControl(playerData);
            List<string> st = new List<string>();
            playerControl.BackColor = SystemColors.Control;
            //playerControl.MouseDown += new MouseEventHandler(PlayerControl_MouseDown); 
            //                      difference between += new MouseEventHandler(Method) and += Method ?
            playerControl.MouseDown += PlayerControl_MouseDown;
            playerControl.updatePlayers += PlayerControl_UpdateImage;
            playerControl.Controls.Cast<Control>()
                                  .ToList()
                                  .ForEach(x => x.MouseDown += Control_MouseDown);

            return playerControl;
        }
        private PlayerControl PlayerControlFactory(Player playerData, Image portrait)
        {
            var playerControl = PlayerControlFactory(playerData);
            playerControl.SetPlayerImage(portrait);
            return playerControl;
        }

        private void PlayerControl_UpdateImage(Image image, long shirtnumber)
        {
            foreach (Player player in players)
            {
                if (player.ShirtNumber == shirtnumber)
                {
                    player.Portrait = image;
                }
            }
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
                SortPlayers(flFavorites);
                SortPlayers(flOtherPlayers);
            }
            else
            {
                playerControl.SetSelectionStatus(!wasSelected);
            }
            ResetPanels();
            SaveState();

        }

        internal void SaveState()
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
            bool favoriteStatus = (goingTo == flFavorites);
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
            RankedList rankedList = new RankedList(players);
            rankedList.Show();
        }
    }
}
