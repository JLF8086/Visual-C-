using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace WPF_IRC
{
    public class Client
    {
        public event EventHandler onConnect;

        public ObservableCollection<IrcNetwork> Networks
        {
            get;
            set;
        }

        private IrcNetwork networkToConnectTo;

        public void Connect(string host, int port, string user, string nick)
        {
            Networks = new ObservableCollection<IrcNetwork>();
            IrcNetwork network = new IrcNetwork(host, port, user, nick);
            Networks.Add(network);
            onConnect(this, new IrcEventArgs(network));
            networkToConnectTo = network;
            BackgroundWorker bgworker = new BackgroundWorker();
            bgworker.DoWork += new DoWorkEventHandler(bgworker_DoWork);
            bgworker.RunWorkerAsync();
            
        }

        void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            networkToConnectTo.Connect();
        }
    }


    public class IrcEventArgs : EventArgs
    {
        public IrcEventArgs(IrcNetwork network)
        {
            this.Network = network;
        }

        public IrcNetwork Network { get; private set; }
    }

}
