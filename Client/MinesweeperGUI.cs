using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace Client
{
    public partial class MinesweeperGUI : Form
    {
        public int height = 20, width = 20, mines = 50;
        private MineFieldButton[,] butArray;
        private TcpClient tcpclnt;
        public MinesweeperGUI(TcpClient tcpclnt)
        {
            this.tcpclnt = tcpclnt;
            InitializeComponent();
        }

        public void explode(string[] param)
        {
            foreach (MineFieldButton but in butArray)
                but.MouseDown -= but_MouseClick;
            for (int i = 1; i < param.Length; i++)
            {
                string[] tokens = param[i].Split('*');
                try
                {
                    butArray[Convert.ToInt32(tokens[0]),
                        Convert.ToInt32(tokens[1])]
                        .setAppearance(tokens[2]);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error : " + e);
                }

            }


        }

        public void revealTiles(string[] param)
        {
            for (int i = 1; i < param.Length; i++)
            {
                string[] tokens = param[i].Split('*');
                    butArray[Convert.ToInt32(tokens[0]),
                        Convert.ToInt32(tokens[1])]
                        .setAppearance(tokens[2]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.mineField.Controls.Clear();
            fillPanel(this.mineField);
            string response = Client.sendMessage("start " + height + " " + width + " " + mines);
        }

        void fillPanel(Panel pan)
        {
            butArray = new MineFieldButton[width, height];
            for (int i = 0; i < this.width; i++)
                for (int j = 0; j < this.height; j++)
                {

                    MineFieldButton but = new MineFieldButton();
                    but.x = i;
                    but.y = j;
                    but.Height = pan.Height / this.height;
                    but.Width = pan.Width / this.width;
                    but.Left = but.Width * j;
                    but.Top = but.Height * i;
                    but.MouseDown += new MouseEventHandler(but_MouseClick);
                    butArray[i, j] = but;
                    but.Visible = false;
                    pan.Controls.Add(but);
                }
            foreach (MineFieldButton but in butArray)
                but.Visible = true;

        }


        void but_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Client.sendMessage(String.Format("leftclick {0} {1}", (sender as MineFieldButton).x, (sender as MineFieldButton).y));
            if (e.Button == MouseButtons.Right)
                Client.sendMessage(String.Format("rightclick {0} {1}", (sender as MineFieldButton).x, (sender as MineFieldButton).y));
        }


        public void modifyAddon(string msg)
        {
            string[] tokens = msg.Split(' ');
            int x = Convert.ToInt32(tokens[1]);
            int y = Convert.ToInt32(tokens[2]);
            if (tokens[0] == "dismantle")
            {
                butArray[x, y].BackColor = Color.Green;
                labelDismantles.Text = tokens[3];
            }
            else if (tokens[0] == "flag")
            {
                butArray[x, y].BackColor = Color.Empty;
                butArray[x, y].Text = "?";
                labelDismantles.Text = tokens[3];
            }
            else
                butArray[x, y].Text = String.Empty;
                
                
        }

    }

    public class MineFieldButton : Button
    {
        public int x, y;
        public void setAppearance(string str)
        {
            if (str == "m")
            {
                this.BackColor = Color.Yellow;
                this.FlatStyle = FlatStyle.Flat;
                this.Text = "*";
            }
            else if (str == "e")
            {
                this.BackColor = SystemColors.ControlLight;
                this.FlatStyle = FlatStyle.Flat;
                this.Enabled = false;
            }
            else if (str == "r")
            {
                this.BackColor = Color.Red;
                this.Text = "*";
                this.FlatStyle = FlatStyle.Flat;
            }
            else
            {
                int m = Convert.ToInt32(str);
                if (m == 0)
                {
                    this.BackColor = SystemColors.ControlLight;
                    this.FlatStyle = FlatStyle.Flat;
                }
                else
                {
                    this.Text = m.ToString();
                    switch (m)
                    {
                        case 1:
                            this.ForeColor = Color.Blue;
                            break;
                        case 2:
                            this.ForeColor = Color.Green;
                            break;
                        case 3:
                            this.ForeColor = Color.Red;
                            break;
                        case 4:
                            this.ForeColor = Color.DarkBlue;
                            break;
                        case 5:
                            this.ForeColor = Color.DarkRed;
                            break;
                        case 6:
                            this.ForeColor = Color.LightBlue;
                            break;
                        case 7:
                            this.ForeColor = Color.Orange;
                            break;
                        case 8:
                            this.ForeColor = Color.Ivory;
                            break;
                    }
                }
            }
        }
    }
}