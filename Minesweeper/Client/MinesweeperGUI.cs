﻿using System;
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
        public int height = Client.height, width = Client.width, mines = Client.mines;
        private MineFieldButton[,] butArray;
        private System.Timers.Timer timer;
        public MinesweeperGUI()
        {
            InitializeComponent();
        }

        public void Explode(string[] param)
        {
            foreach (MineFieldButton but in butArray)
                but.MouseDown -= but_MouseClick;
            for (int i = 1; i < param.Length; i++)
            {
                string[] tokens = param[i].Split('*');
                butArray[Convert.ToInt32(tokens[0]),
                        Convert.ToInt32(tokens[1])]
                        .setAppearance(tokens[2]);

            }
            timer.Close();
        }

        public void EndGame(string victories)
        {

            foreach (MineFieldButton but in butArray)
                but.MouseDown -= but_MouseClick;
            timer.Close();
            MessageBox.Show("You win!\nTotal Victories: " + victories);
        }

        public void RevealTiles(string[] param)
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
            if (timer != null)
                timer.Close();
            this.mineField.Controls.Clear();
            fillPanel(this.mineField);
            labelTime.Text = "0";
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(onTick);
            labelDismantles.Text = mines.ToString();
            timer.Start();


            Client.SendMessage("start " + width + " " + height + " " + mines);
        }

        private void onTick(object sender, EventArgs e)
        {
            Client.SendMessage("elapsedtime");
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
                    but.Left = but.Width * i;
                    but.Top = but.Height * j;
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
            {
                Client.SendMessage(String.Format("leftclick {0} {1}", (sender as MineFieldButton).x, (sender as MineFieldButton).y));
                Client.SendMessage("minesleft");
            }
            else if (e.Button == MouseButtons.Right)
                Client.SendMessage(String.Format("rightclick {0} {1}", (sender as MineFieldButton).x, (sender as MineFieldButton).y));
            Client.SendMessage("isgameover?");
        }


        public void ModifyAddon(string msg)
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
                this.Text = String.Empty;
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
                    this.Text = String.Empty;
                    this.BackColor = SystemColors.ControlLight;
                    this.FlatStyle = FlatStyle.Flat;
                }
                else
                {
                    this.Text = m.ToString();
                    this.BackColor = Color.Empty;
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
                            this.ForeColor = Color.Purple;
                            break;
                    }
                }
            }
        }
    }
}