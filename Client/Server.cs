using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class TCPServer
{
    private static int port = 8001;

    public static void Main()
    {
        IPAddress ipAddress = IPAddress.Any;
        TcpListener listener = new TcpListener(ipAddress, port);
        listener.Start();
        Console.WriteLine("Server is running");
        Console.WriteLine("Listening on port " + port);
        Console.WriteLine("Waiting for connections...");
        while (true)
        {
            Socket s = listener.AcceptSocket();
            Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
            byte[] b = new byte[65535];
            int k = s.Receive(b);
            Console.WriteLine("Received:");
            for (int i = 0; i < k; i++)
                Console.Write(Convert.ToChar(b[i]));
            Console.Write("\n");
            ASCIIEncoding enc = new ASCIIEncoding();
            s.Send(enc.GetBytes("Hello\n"));
            Console.WriteLine("Sent Response");
            //s.Close();
            if (!IsConnected(s))
            {
                Console.WriteLine(s.RemoteEndPoint + " has disconnected");
                s.Close();
                break;
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
    
}