using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;


public class clnt
{

    public static void Main()
    {

        try
        {
            TcpClient tcpclnt = new TcpClient();
            Console.WriteLine("Connecting.....");

            tcpclnt.Connect("78.63.226.214", 8001);
            // use the ipaddress as in the server program

            Console.WriteLine("Connected");
            while (true)
            {
                //if (!tcpclnt.Client.Poll(-1, SelectMode.SelectError))
                    //break;
                Console.Write("Enter the string to be transmitted : ");

                String str = Console.ReadLine();
                if (str == "quit")
                    break;
                Stream stm = tcpclnt.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                Console.WriteLine("Transmitting.....");

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 25);

                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(bb[i]));
                Console.WriteLine();
                
            }
            tcpclnt.Close();
        }
        
        catch (Exception e)
        {
            Console.WriteLine("Error..... " + e.Message);
        }
        Console.WriteLine("opa");
    }
}
