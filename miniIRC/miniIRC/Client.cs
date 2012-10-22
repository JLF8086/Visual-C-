using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace miniIRC
{
    public class Client
    {
        public event EventHandler messageReceived;
        private TcpClient connection;
        private string username, ip, port, nickname;
        StreamReader reader;
        StreamWriter writer;

        public Client(string ip, string port, string user, string nick)
        {
            this.nickname = nick;
            this.username = user;
            this.ip = ip;
            this.port = port;
            connection = new TcpClient();
        }

        public void Connect()
        {
            connection.Connect(ip, Convert.ToInt32(port));
            reader = new StreamReader(connection.GetStream());
            writer = new StreamWriter(connection.GetStream());
            writer.NewLine = "\r\n";
            SendMessage("NICK " + nickname);
            SendMessage("USER " + username + " 8 *:Test Testovski");
            new Thread(new ThreadStart(ReceiveMessages)).Start();
        }

        public void Disconnect()
        {
            connection.Close();
        }


        public void SendMessage(string msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
        }

        public void ReceiveMessages()
        {
            string receivedMessage;
            while ((receivedMessage = reader.ReadLine()) != null)
            {
                if (receivedMessage.Split(' ')[0] == "PING")
                    SendMessage("PONG " + receivedMessage.Split(' ')[1]);
                messageReceived(this, new ReceivedMessageEventArgs(receivedMessage) { });
            }
        }
    }

    public class ReceivedMessageEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public ReceivedMessageEventArgs(string msg)
            : base()
        {
            this.Message = msg;
        }
    }
}
