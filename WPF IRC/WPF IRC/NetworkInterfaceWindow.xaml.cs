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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_IRC
{
    /// <summary>
    /// Interaction logic for serverWindow.xaml
    /// </summary>
    public partial class NetworkInterfaceWindow : UserControl
    {
        public IrcNetwork Network { get; private set; }

        public NetworkInterfaceWindow(IrcNetwork network)
        {
            InitializeComponent();
            this.Network = network;
            this.DataContext = network;
            Network.messageReceived += new EventHandler(Network_messageReceived);
            this.inputBox.Focus();
        }

        void Network_messageReceived(object sender, EventArgs e)
        {
            object[] param = new object[1];
            param[0] = (string)((e as NetworkMessageEventArgs).Message + Environment.NewLine);
            this.Dispatcher.Invoke(new updateString(updateTextBox), param);
            //
        }

        void updateTextBox(string msg)
        {
            textBox2.Text += msg;
            textBox2.ScrollToEnd();
        }

        delegate void updateString(string str);

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Network.ParseCommand(inputBox.Text);
                inputBox.Text = String.Empty;
            }
        }
    }
}
