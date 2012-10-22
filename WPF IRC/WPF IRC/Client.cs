using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;

namespace WPF_IRC
{
    public class Client
    {
        public event EventHandler onConnect;

        public List<IrcNetwork> networks = new List<IrcNetwork>();

        private IrcNetwork networkToConnectTo;

        public void Connect(string host, int port, string user, string nick)
        {
            IrcNetwork network = new IrcNetwork(host, port, user, nick);
            networks.Add(network);
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
