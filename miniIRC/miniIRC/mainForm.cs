using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace miniIRC
{
    public partial class mainForm : Form
    {
        private List<Client> serverConnections = new List<Client>();

        public mainForm()
        {
            InitializeComponent();
        }

        private void mailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ConnectionForm(this).Show();
        }

        public void Connect(string ip, string port, string nick, string user)
        {
            Client client = new Client(ip, port, user, nick);
            treeView1.Nodes.Add(ip);
            ServerWindow serverWindow = new ServerWindow() { Parent = this.windowPanel };
            this.windowPanel.Controls.Add(serverWindow);
            client.messageReceived += new EventHandler(serverWindow.AddMessage);
            client.Connect();
            serverConnections.Add(client);
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Client cl in this.serverConnections)
                cl.Disconnect();
        }


    }
}
