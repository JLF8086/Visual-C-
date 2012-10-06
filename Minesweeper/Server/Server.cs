using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace Server
{
    public class Server
    {
        private static int port = 8001;
        private static byte[] b = new byte[65535];
        private static IPAddress ipAddress = IPAddress.Any;
        private static Socket listener; 

        static Server() {
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            listener.Bind(localEndPoint);
        }


        private static Socket GetConnection()
        {
            listener.Listen(5);
            Socket s = listener.Accept();
            return s;
        }

        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("Waiting for connections...");
                Socket s = GetConnection();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
                new Thread(new ThreadStart(new Player(s).Run)).Start();
            }

        }
    }
}