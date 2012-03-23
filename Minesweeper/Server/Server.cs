using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public class Server
    {
        private static int port = 8001;
        private static ASCIIEncoding enc = new ASCIIEncoding();
        private static Game game;
        private static byte[] b = new byte[65535];
        private static IPAddress ipAddress = IPAddress.Any;
        private static TcpListener listener = new TcpListener(ipAddress, port);

        private static Socket GetConnection()
        {
            
            listener.Start();
            Socket s = listener.AcceptSocket();
            listener.Stop();
            return s;
        }

        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Waiting for connections...");
                Socket s = GetConnection();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                try
                {
                    while (true)
                    {

                        ReceiveMessage(s);

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Connection stopped: " + e);
                }
                finally
                {
                    if (!IsConnected(s))
                    {
                        Console.WriteLine(s.RemoteEndPoint + " has disconnected");
                        s.Close();
                    }
                }
            }

        }

        public static bool IsConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public static void ReceiveMessage(Socket s)
        {
            int k = s.Receive(b);
            Console.WriteLine("Received:");
            string receivedMessage = "";
            for (int i = 0; i < k; i++)
                receivedMessage += Convert.ToChar(b[i]);
            Console.WriteLine(receivedMessage);

            string[] tokens = receivedMessage.Split(' ');

            switch (tokens[0])
            {
                case "isgameover?":
                {
                    if (game.IsOver())
                    {
                        Stats.AddVictory();
                        s.Send(enc.GetBytes("gameisover " + Stats.Victories));
                    }
                    else
                        s.Send(enc.GetBytes("gamenotover"));
                    break;
                }
                case "start": 
                {
                    try
                    {
                        game = new Game(Convert.ToInt32(tokens[1]), Convert.ToInt32(tokens[2]), Convert.ToInt32(tokens[3]));
                        s.Send(enc.GetBytes("ok"));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break; 
                }

                case "leftclick":
                {

                    string response = game.LeftClick(
                        Convert.ToInt32(tokens[1]), 
                        Convert.ToInt32(tokens[2]));
                    //Console.WriteLine(response);
                    s.Send(enc.GetBytes(response));
                    break;
                }

                case "elapsedtime":
                {
                    s.Send(enc.GetBytes("elapsedtime " + game.time.ToString()));
                    break;
                }
                case "rightclick":
                {

                    string response = game.RightClick(Convert.ToInt32(tokens[1]), Convert.ToInt32(tokens[2]));
                    //Console.WriteLine(response);
                    s.Send(enc.GetBytes(response));
                    break;
                }

                case "minesleft":
                {
                    string response = "minesleft " + game.MinesLeft;
                    s.Send(enc.GetBytes(response));
                    break;
                }


            }


            //s.Send(enc.GetBytes(receivedMessage.Split(' ')[0]));
            //Console.WriteLine("Sent Response");
        }

    }
}