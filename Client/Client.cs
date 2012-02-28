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
        
        public static string IP;
        private static ASCIIEncoding asc = new ASCIIEncoding();
        private static TcpClient tcpclnt;
        private static MinesweeperGUI gui;
        public static int height, width, mines;

        public static void Main()
        {
            try
            {
                while (true)
                {

                    try
                    {
                        Application.Run(new InputIp());
                        tcpclnt = new TcpClient();
                        if (IP == null)
                            return;
                        Console.WriteLine(IP);
                        tcpclnt.Connect(IP, 8001);
                        Console.WriteLine("Connection established");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error : " + e);
                        continue;
                    }
                    break;
                }
                gui = new MinesweeperGUI(tcpclnt);
                Application.Run(gui);

            }

            catch (Exception e)
            {
                MessageBox.Show("Error : " + e);
                tcpclnt.Close();
            }
        }

        public static void SendMessage(string msg)
        {
            string response = String.Empty;
            Stream stream = tcpclnt.GetStream();
            byte[] msgBytes = asc.GetBytes(msg);
            stream.Write(msgBytes, 0, msgBytes.Length);
            byte[] buffer = new byte[gui.width * gui.height * 9];
            int responseLength = stream.Read(buffer, 0, buffer.Length);
            for (int i = 0; i < responseLength; i++)
            {
                response += (char)buffer[i];
            }
            ParseMessage(response);


        }

        public static void ParseMessage(string msg)
        {
            string[] tokens = msg.Split(' ');
            switch (tokens[0])
            {
                case "gameisover":
                    gui.EndGame();
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
                    gui.labelTime.Text = tokens[1];
                    break;
                case "dismantle":
                case "flag":
                case "none":
                    gui.ModifyAddon(msg);
                    break;
                case "minesleft":
                    gui.labelDismantles.Text = tokens[1];
                    break;
            }
            return;
        }

        
    }
}