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

        private static Socket getConnection()
        {
            IPAddress ipAddress = IPAddress.Any;
            TcpListener listener = new TcpListener(ipAddress, port);
            listener.Start();
            Socket s = listener.AcceptSocket();
            return s;
        }

        public static void Main()
        {
            Console.WriteLine("Waiting for connections...");
            Socket s = getConnection();
            Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
            
            try
            {
                while (true)
                {

                    receiveMessage(s);
                    if (!IsConnected(s))
                    {
                        Console.WriteLine(s.RemoteEndPoint + " has disconnected");
                        s.Close();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection stopped: " + e);

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

        public static void receiveMessage(Socket s)
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

                    string response = game.leftclick(
                        Convert.ToInt32(tokens[1]), 
                        Convert.ToInt32(tokens[2]));
                    //Console.WriteLine(response);
                    s.Send(enc.GetBytes(response));
                    break;
                }

                case "rightclick":
                {

                    string response = game.rightclick(Convert.ToInt32(tokens[1]), Convert.ToInt32(tokens[2]));
                    //Console.WriteLine(response);
                    s.Send(enc.GetBytes(response));
                    break;
                } 


            }

            //s.Send(enc.GetBytes(receivedMessage.Split(' ')[0]));
            //Console.WriteLine("Sent Response");
        }

    }
}