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
        public event EventHandler joinedChannel;
        public event EventHandler leftChannel;
        public event EventHandler networkQuit;

        public string DisplayName { get; private set; }
        public string host { get; private set; }
        public string Nick { get; set; }
        public List<Channel> Channels = new List<Channel>();

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
                SendMessage("USER " + this.user + " 8 *:very good user");
                BackgroundWorker bgworker = new BackgroundWorker();
                bgworker.DoWork += new DoWorkEventHandler(bgworker_DoWork);
                bgworker.RunWorkerAsync();
            }
        }


        void bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            ReceiveMessages();
        }


        public void ParseChannelMessage(Channel chan, string msg)
        {
            if (msg == String.Empty)
                return;
            if (msg[0] != '/')
            {
                SendMessage("PRIVMSG " + chan.Name + " :" + msg);
                chan.WriteChannelMessage(this.Nick, msg);
                return;
            }
            switch(msg.IndexOf(' ') > 0 ? msg.Substring(0, msg.IndexOf(' ')) : msg)
            {
                case "/part":
                    {
                        SendMessage("PART " + chan.Name);
                    }  break;
                case "/mode":
                    {
                        SendMessage("MODE " + chan.Name + ' ' + msg.Remove(0, "/mode".Length));
                        //System.Windows.MessageBox.Show("MODE " + chan.Name + ' ' + msg.Remove(0, msg.IndexOf('#') + chan.Name.Length + 2));
                    }  break;
                case "/topic":
                    {
                        SendMessage("TOPIC " + chan.Name + " :" + msg.Remove(0, "/topic".Length));
                    }  break;
                case "/kick":
                    {
                        SendMessage("KICK " + chan.Name + ' ' + msg.Remove(0, "/kick".Length));
                    }  break;
                default:
                    {
                        ParseCommand(msg);
                    }  break;

            }
        }

        public void ParseCommand(string msg)
        {
            string[] tokens = msg.Split(' ');
            if (tokens[0][0] != '/')
            {
                messageReceived(this, new NetworkMessageEventArgs("Commands must start with '/'!"));
            }
            switch (tokens[0])
            {
                case "/join":
                    if (tokens.Length >= 2)
                        SendMessage("JOIN " + tokens[1]);
                    break;
                case "/quit":
                    SendMessage("QUIT");
                    if (networkQuit != null)
                        networkQuit(this, null);
                    break;
                case "/nick":
                    SendMessage("NICK " + tokens[1]);
                    break;
            }

        }

        public void ParseIncomingMsg(string msg)
        {
            string[] tokens = msg.Split(' ');
            if (tokens[0] == "PING")
                SendMessage("PONG " + tokens[1]);
            switch (tokens[1])
            {
                case "PART":
                    { //:Testuotojas69!~TestUseri@78.63.226.214 PART #yy
                        string name = msg.Substring(1, msg.IndexOf('!') - 1);
                        if (name == this.Nick)
                        {
                            Channels.Single(s => s.Name == tokens[2]).LeaveChannel();
                            Channels.Remove(Channels.Single(s => s.Name == tokens[2]));
                        }
                        else
                            Channels.Single(s => s.Name == tokens[2]).WriteChannelMessage(this.DisplayName, name + " has left " + tokens[2]);

                    }  break;

                case "QUIT":
                    { //:zxcza!~TestUseri@78.63.226.214 QUIT :Quit
                        string name = msg.Substring(1, msg.IndexOf('!') - 1);
                        foreach (Channel chan in Channels)
                            chan.ReportQuit(name);
                    }  break;
                case "MODE":
                    { //:JLF!~b@78.63.226.214 MODE #r2x -o Testuotojas69
                        if (tokens[2][0] == '#')
                            Channels.Single(s => s.Name == tokens[2]).ChangeUserChannelMode(
                                msg.Substring(1, msg.IndexOf('!') - 1), tokens[3], tokens[4]);
                    }  break;

                case "JOIN":
                    {
                        if (msg.Substring(1, msg.IndexOf('!') - 1) == this.Nick)
                            JoinChannel(new Channel(this, tokens[2]));
                        else
                        {
                            this.Channels.Single(s => s.Name == tokens[2]).AddUser(msg.Substring(1, msg.IndexOf('!') - 1));
                            this.Channels.Single(s => s.Name == tokens[2]).WriteChannelMessage(this.DisplayName, msg.Substring(1, msg.IndexOf('!') - 1)
                                + " has joined " + tokens[2]);
                        }
                    } break;
                    case "332":
                    { 
                        Channels.Single(s => s.Name == tokens[3]).DisplayTopic(msg.Split(':')[1]);
                    }  break;
                case "353":
                    {
                        string[] users = msg.Split(':')[2].Split(' ');
                        Channel result = this.Channels.Single(s => s.Name == tokens[4]);
                        result.SetUsers(users);
                    }  break;
                case "PRIVMSG":
                    {
                        if (tokens[2][0] == '#')
                        {
                            Channel result = this.Channels.Single(s => s.Name == tokens[2]);
                            result.WriteChannelMessage(msg.Substring(1, msg.IndexOf('!')-1), msg.Split(':')[2]);
                        }

                    }  break;
                case "NICK":
                    {
                        this.Nick = tokens[1];
                    }  break;
                default:
                    {

                    }  break;
            }
        }



        public void JoinChannel(Channel chan)
        {
            this.Channels.Add(chan);
            if (joinedChannel != null)
                joinedChannel(this, new ChannelEventArgs() { Channel = chan });
        }


        public void SendMessage(string msg)
        {
            try
            {
                writer.WriteLine(msg);
            }
            catch
            {
                return;
            }
        }

        private void ReceiveMessages()
        {
            while (networkConnection.Connected)
            {
                string message = reader.ReadLine();
                if (message == String.Empty || message == null)
                    break;
                ParseIncomingMsg(message);
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
