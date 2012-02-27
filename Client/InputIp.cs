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
            if (!(textBox1.Text.Length == 0))
                Client.IP = textBox1.Text;
            this.Dispose();
        }

    }
}
