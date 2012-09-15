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
            if (!ValidateInput())
                return;
            Client.height = Convert.ToInt32(heightBox.Text);
            Client.width = Convert.ToInt32(widthBox.Text);
            Client.mines = Convert.ToInt32(minesBox.Text);
            if (!(ipBox.Text.Length == 0))
                Client.IP = ipBox.Text;
            this.Dispose();
        }

        private bool ValidateInput()
        {
            bool validated = true;
            int height = 0;
            int width = 0;
            errorProvider1.SetError(heightBox, string.Empty);
            errorProvider1.SetError(widthBox, string.Empty);
            errorProvider1.SetError(minesBox, string.Empty);
            if (ipBox.Text.Length == 0)
            {
                errorProvider1.SetError(ipBox, "Can't leave this field empty!");
                validated = false;
            }
            try
            {
                height = Convert.ToInt32(heightBox.Text);
                if (height < 5 || height > 50)
                {
                    validated = false;
                    errorProvider1.SetError(heightBox, "Must be between 5 and 50!");
                }
            }
            catch
            {
                errorProvider1.SetError(heightBox, "Must be a number!");
                validated = false;
            }
            try
            {
                width = Convert.ToInt32(widthBox.Text);
                if (width < 5 || width > 50)
                {
                    validated = false;
                    errorProvider1.SetError(widthBox, "Must be between 5 and 50!");
                }
            }
            catch
            {
                errorProvider1.SetError(widthBox, "Must be a number!");
                validated = false;
            }
            if (!validated)
                return validated;
            try
            {
                int mines = Convert.ToInt32(minesBox.Text);
                if (mines < 0 || mines > height * width)
                {
                    validated = false;
                    errorProvider1.SetError(minesBox, "Must be between 0 and height*width!");
                }
            }
            catch
            {
                errorProvider1.SetError(minesBox, "Must be a number!");
                validated = false;
            }
            return validated;
        }

    }
}
