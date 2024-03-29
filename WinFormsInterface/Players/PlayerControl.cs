﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WinFormsInterface.Properties;

namespace WinFormsInterface
{
    public partial class PlayerControl : UserControl
    {
        public PlayerControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.StandardClick, true);

            this.tsMoveTo.Text = Program.LocalizedString("Move");
            this.tsToFavorites.Text = Program.LocalizedString("toFavorites");
            this.tsToOther.Text = Program.LocalizedString("toOthers");
            this.tsAddPicture.Text = Program.LocalizedString("AddPicture");
        }

        internal delegate void UpdateImage(Image image, long shirtNumber);
        internal event UpdateImage updatePlayers;

        public DataHandler.Model.Player playerData;
        public bool isSelected = false;

        public PlayerControl(DataHandler.Model.Player playerData) : this()
        {
            this.playerData = playerData;
            lbPlayerName.Text = SplitName();
            lbNumber.Text = playerData.ShirtNumber.ToString();
            cbIsCaptain.Checked = playerData.Captain;
            lbFavorite.Text = string.Empty;
        }
        public PlayerControl(DataHandler.Model.Player playerData, Image playerPortrait) : this(playerData)
        {
            this.pbPlayerPortrait.Image = playerPortrait;
            
        }

        public void SetPlayerImage(Image playerPortrait)
        {
            this.pbPlayerPortrait.Image = playerPortrait;
        }

        private string SplitName()
        {
            var capitalized = playerData.Name.ToLower();
            var split = capitalized.Split(' ');

            string firstline = string.Join(' ', split.SkipLast(1));
            string secondline = split.Last();

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            return $"{textInfo.ToTitleCase(firstline)}\n{textInfo.ToTitleCase(secondline)}";
        }
        public void SetFavoriteStatus(bool value)
        {
            playerData.Favorite = value;
            lbFavorite.Text = playerData.Favorite ? "*" : string.Empty;
        }

        public void SetSelectionStatus(bool value)
        {
            isSelected = value;
            cbSelected.Checked = value;
            BackColor = isSelected ? Color.FromArgb(104, 104, 104) : SystemColors.Control;
        }

        internal void TriggerMouseDown(MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        private void cbSelected_CheckedChanged(object sender, EventArgs e)
        {
            SetSelectionStatus(cbSelected.Checked);
        }
        private void tsToFavorites_Click(object sender, EventArgs e)
        {
            FavoritePlayers parentForm = this.FindForm() as FavoritePlayers;
            parentForm.goingTo = parentForm.flFavorites;
            parentForm.departedFrom = this.Parent as FlowLayoutPanel;
            SetSelectionStatus(true);
            parentForm.MoveSelectedControls();
        }
        private void tsToOther_Click(object sender, EventArgs e)
        {
            FavoritePlayers parentForm = this.FindForm() as FavoritePlayers;
            parentForm.goingTo = parentForm.flOtherPlayers;
            parentForm.departedFrom = this.Parent as FlowLayoutPanel;
            SetSelectionStatus(true);
            parentForm.MoveSelectedControls();
        }
        private void tsAddPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                // little time, bad code
                try
                {
                    var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    var downloads = home + "\\Downloads";
                    fileDialog.InitialDirectory = downloads;
                    fileDialog.Filter = $"{Program.LocalizedString("ImageFiles")} (*.png, *.jpg, *jpeg)|*.jp*g;*.png|{Program.LocalizedString("Allfiles")} (*.*)|*.*";
                    fileDialog.FilterIndex = 1;
                    fileDialog.RestoreDirectory = true;
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string destinationDirectory = Program.userSettings.GenderedRepresentationFilePath() + Program.lastTeam.FifaCode;
                        if (!Directory.Exists(destinationDirectory))
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }
                        string destinationFile = destinationDirectory + $"/{playerData.ShirtNumber}.{fileDialog.FileName.Split('.').Last()}";
                        File.Copy(fileDialog.FileName, destinationFile, true);
                        this.playerData.PortraitPath = destinationFile;
                        Image image = Image.FromFile(destinationFile);
                        updatePlayers?.Invoke(image, playerData.ShirtNumber);
                        pbPlayerPortrait.Image = image;
                        (this.FindForm() as FavoritePlayers).SaveState();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.LocalizedString("IconSetError"));
                }
            }
        }

        private void PlayerControl_Load(object sender, EventArgs e)
        {
            cbIsCaptain.Text = Program.LocalizedString("Captain");
        }
    }
}
