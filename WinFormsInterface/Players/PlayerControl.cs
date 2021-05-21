using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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
        }

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
            this.playerPortrait.Image = playerPortrait;
        }

        public void SetPlayerImage(Image playerPortrait)
        {
            this.playerPortrait.Image = playerPortrait;
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
    }
}
