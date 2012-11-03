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
using System.Threading;
using System.ComponentModel;

namespace WPF_IRC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Client client = new Client();
        private Dictionary<object, object> networkTabControls = new Dictionary<object, object>();
        private Dictionary<object, object> networkToTabItem = new Dictionary<object, object>();
        public MainWindow()
        {
            InitializeComponent();
            client.onConnect += new EventHandler(handleNewNetwork);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            client.Connect(hostTextBox.Text, Convert.ToInt32(portTextBox.Text), userTextBox.Text, nickTextBox.Text);
        }

        private void handleNewNetwork(object sender, EventArgs e)
        {
            TabItem newNetworkTab = new TabItem();
            newNetworkTab.Header = (e as IrcEventArgs).Network.DisplayName;
            TabControl newNetworkTabControl = new TabControl();
            newNetworkTab.Content = newNetworkTabControl;
            newNetworkTabControl.Items.Add(new TabItem() { Content = new NetworkInterfaceWindow((e as IrcEventArgs).Network),
                                                           Header = (e as IrcEventArgs).Network.DisplayName });
            networkTabControls.Add((e as IrcEventArgs).Network, newNetworkTabControl);
            (e as IrcEventArgs).Network.joinedChannel += new EventHandler(handleNewChannel);
            (e as IrcEventArgs).Network.leftChannel += new EventHandler(leaveChannel);
            (e as IrcEventArgs).Network.networkQuit += new EventHandler(Network_networkQuit);
            
            //TabItem networkTab
            networkToTabItem.Add((e as IrcEventArgs).Network, newNetworkTab);
            networksTabControl.Items.Add(newNetworkTab);
            newNetworkTab.IsSelected = true;
        }

        void Network_networkQuit(object sender, EventArgs e)
        {
            object[] param = new object[2];
            param[1] = sender as IrcNetwork;
            this.Dispatcher.Invoke(new updateString(deleteNetworkTab), param);
        }

        void deleteNetworkTab(Channel chan, IrcNetwork network)
        {
            //System.Windows.MessageBox.Show(networksTabControl.Items.Contains(networkTabControls[network as IrcNetwork]).ToString());
            TabItem requiredItem = networkToTabItem[network] as TabItem;
            networksTabControl.Items.Remove(requiredItem);
            //networkToTabItem.Remove(network);
            //networkTabControls.Remove((network as IrcNetwork));
        }

        private void handleNewChannel(object sender, EventArgs e)
        {
            IrcNetwork network = (sender as IrcNetwork);

            Channel chan = (e as ChannelEventArgs).Channel;
            object[] param = new object[2];
            param[0] = chan;
            param[1] = network;
            this.Dispatcher.Invoke(new updateString(updateNetworkTab), param);
            
        }

        void updateNetworkTab(Channel chan, IrcNetwork network)
        {
            TabItem item = new TabItem() { Header = chan.Name };
            (networkTabControls[network] as TabControl).Items.Add(item);
            item.Content = new ChannelInterfaceWindow(chan);
            chan.channelLeft += new EventHandler(leaveChannel);
            item.IsSelected = true;
        }

        void deleteChannelTab(Channel chan, IrcNetwork network)
        {
            TabControl networkTabControl = networkTabControls[network] as TabControl;
            TabItem item = null;
            foreach (TabItem tab in networkTabControl.Items)
                if (tab.Header.Equals(chan.Name))
                    item = tab;
            if (item != null)
                networkTabControl.Items.Remove(item);
        }

        delegate void updateString(Channel chan, IrcNetwork network);

        private void leaveChannel(object sender, EventArgs e)
        {
            object[] param = new object[2];
            param[0] = (sender as Channel);
            param[1] = (sender as Channel).Network;
            this.Dispatcher.Invoke(new updateString(deleteChannelTab), param);
        }




    }
}
