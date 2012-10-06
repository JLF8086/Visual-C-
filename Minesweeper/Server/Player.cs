using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Server
{
    class Player
    {
        private Socket connectionSocket;
        private static int port = 8001;
        private static ASCIIEncoding enc = new ASCIIEncoding();
        private Game game;
        private static byte[] buffer = new byte[32];

        public Player(Socket s)
        {
            this.connectionSocket = s;
            s = null;
        }

        public void Run()
        {
            try
            {
                while (true)
                {
                    ReceiveMessage(connectionSocket);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection stopped: " + e.Message);
            }
            finally
            {
                if (!IsConnected(connectionSocket))
                {
                    Console.WriteLine(connectionSocket.RemoteEndPoint + " has disconnected");
                    connectionSocket.Close();
                }
            }
        }

        public void ReceiveMessage(Socket s)
        {
            int k = s.Receive(buffer);
            StringBuilder receivedMessageBuilder = new StringBuilder();
            for (int i = 0; i < k; i++)
                receivedMessageBuilder.Append((char)buffer[i]);
            string receivedMessage = receivedMessageBuilder.ToString();
            if (receivedMessage != "elapsedtime" && receivedMessage != "isgameover?" && receivedMessage != "minesleft")
            {
                Console.Write("Received [" + s.RemoteEndPoint + "]: ");
                Console.WriteLine(receivedMessage);
            }

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
                            Console.WriteLine(e.Message);
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
        }

        public bool IsConnected(Socket socket)
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
    }
}
