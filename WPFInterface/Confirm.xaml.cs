using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
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
    /// Interaction logic for Confirm.xaml
    /// </summary>
    public partial class Confirm : Window
    {
        public Confirm()
        {
            InitializeComponent();
            lbTitle.Content = App.LocalizedString("ConfirmTitle");
            lbMessage.Content = App.LocalizedString("Confirm");
            btOK.Content = App.LocalizedString("okButton");
            btCancel.Content = App.LocalizedString("CancelButton");
            this.btCancel.Click += BtCancel_Click;
            this.KeyUp += Confirm_KeyUp;
        }

        private void Confirm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
                    break;
                case Key.Enter:
                    ButtonAutomationPeer peer = new ButtonAutomationPeer(btOK);
                    IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                    invokeProv.Invoke();
                    break;
            }
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
