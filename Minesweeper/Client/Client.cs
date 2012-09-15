using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections;

namespace Client
{
    public class Client
    {
        private delegate void UpdateTimerLabel(string time);
        private delegate void UpdateDismantlesLabel(string dismantles);

        private static string closingReason = String.Empty;
        public static string IP;
        private static int port = 8001;
        private static ASCIIEncoding asc = new ASCIIEncoding();
        //private static TcpClient tcpclnt;
        private static Socket clientSocket;
        private static MinesweeperGUI gui;
        public static int height, width, mines;


        public static void Main()
        {

            while (true)
            {

                try
                {
                    IP = null;
                    Application.Run(new InputIp());
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    if (IP == null)
                        return;
                    clientSocket.Connect(IP, port);
                    Console.WriteLine("Connection established");
                    gui = new MinesweeperGUI();
                    Application.Run(gui);
                    if (closingReason != String.Empty)
                        MessageBox.Show(closingReason);
                    break;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not establish a connection with the server: " + e.Message);
                }
                clientSocket.Close();
            }
        }
        
        /// <summary>
        /// Converts an ascii message into bytes and sends it to a server through the clientSocket.
        /// </summary>
        /// <param name="msg">The message to be sent.</param>
        public static void SendMessage(string msg)
        {
            try
            {
                StringBuilder responseBuilder = new StringBuilder();
                byte[] msgBytes = asc.GetBytes(msg);
                clientSocket.Send(msgBytes, 0, msgBytes.Length, SocketFlags.None);
                //stream.Write(msgBytes, 0, msgBytes.Length);
                byte[] buffer = new byte[gui.width * gui.height * 9];
                int responseLength = clientSocket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                for (int i = 0; i < responseLength; i++)
                {
                    responseBuilder.Append((char)buffer[i]);
                }
                ParseMessage(responseBuilder.ToString());
            }
            catch (Exception e)
            {
                gui.Close();
                closingReason = e.Message;
            }

        }
        /// <summary>
        /// Parses the message received from the server and does the appropriate action.
        /// </summary>
        /// <param name="msg">The message to be parsed.</param>
        public static void ParseMessage(string msg)
        {
            string[] tokens = msg.Split(' ');
            switch (tokens[0])
            {
                case "gameisover":
                    gui.EndGame(tokens[1]);
                    break;
                case "gamenotover":
                case "ok":
                    return;
                case "explode":
                    gui.Explode(tokens);
                    break;
                case "reveal":
                    gui.RevealTiles(tokens);
                    break;
                case "elapsedtime":
                    gui.labelTime.Invoke((Action)delegate { gui.labelTime.Text = tokens[1]; });
                    break;
                case "dismantle":
                case "flag":
                case "none":
                    gui.ModifyAddon(msg);
                    break;
                case "minesleft":
                    gui.labelTime.Invoke((Action)delegate { gui.labelDismantles.Text = tokens[1]; });
                    break;
            }
            return;
        }




        
    }
}