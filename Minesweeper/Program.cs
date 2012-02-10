using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Minesweeper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Net.Sockets.Socket sock = new Socket();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
