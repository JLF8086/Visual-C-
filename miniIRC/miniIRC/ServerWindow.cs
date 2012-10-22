using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace miniIRC
{
    public partial class ServerWindow : UserControl
    {

        public ServerWindow()
        {
            InitializeComponent();
        }

        public void AddMessage(Object sender, EventArgs e)
        {
            textBox2.Text += (e as ReceivedMessageEventArgs).Message;
            textBox2.Text += Environment.NewLine;
        }
    }
}
