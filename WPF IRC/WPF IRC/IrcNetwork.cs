using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.ComponentModel;

namespace WPF_IRC
{
    public class IrcNetwork
    {
        public event EventHandler messageReceived;

        public string DisplayName { get; private set; }
        public string host { get; private set; }
        public string Nick { get; set; }

        private TcpClient networkConnection;
        private string user;
        private int port;

        private StreamWriter writer;
        private StreamReader reader;

        public override string ToString()
        {
            return this.DisplayName;
        }

        public IrcNetwork(string host, int port, string user, string nick)
        {
            this.DisplayName = host;
            this.host = host;
            this.Nick = nick;
            this.user = user;
            this.port = port;
        }

        public void Connect()
        {
            networkConnection = new TcpClient(host, port);
            if (networkConnection.Connected)
            {
                writer = new StreamWriter(networkConnection.GetStream());
                writer.NewLine = "\r\n";
                writer.AutoFlush = true;
                reader = new StreamReader(networkConnection.GetStream());
                SendMessage("NICK " + this.Nick);
                SendMessage("USER " + this.user + " 8 *:Bomzhas ledynmetis");
                BackgroundWorker bgworker = new BackgroundWorker();
                bgworker.DoWork += new DoWorkEventHandler(bgworker_DoWork);
                bgworker.RunWorkerAsync();
            }
        }

        void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            ReceiveMessages();
        }

        public void SubmitMessage(string msg)
        {
            SendMessage(msg);
        }

        public void SendMessage(string msg)
        {
            writer.WriteLine(msg);
        }

        private void ReceiveMessages()
        {
            while (networkConnection.Connected)
            {
                string message = reader.ReadLine();
                if (message == String.Empty || message == null)
                    break;
                if (messageReceived != null)
                    messageReceived(this, new NetworkMessageEventArgs(message));
            }
        }
    }

    public class NetworkMessageEventArgs : EventArgs
    {
        public string Message { get; set; }

        public NetworkMessageEventArgs(string msg)
        {
            this.Message = msg;
        }
    }
}
