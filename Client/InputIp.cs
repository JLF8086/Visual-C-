using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class InputIp : Form
    {
        public InputIp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Client.height = Convert.ToInt32(textBox2.Text);
            Client.width = Convert.ToInt32(textBox3.Text);
            Client.mines = Convert.ToInt32(textBox4.Text);
            if ((Client.height * Client.width) < Client.mines)
            {
                MessageBox.Show("Too many mines!");
                return;
            }

            if (Client.width < 5 || Client.height < 5)
            {
                MessageBox.Show("Width and height must be at least 5!");
                return;
            }
            if (!(textBox1.Text.Length == 0))
                Client.IP = textBox1.Text;
            this.Dispose();
        }

    }
}
