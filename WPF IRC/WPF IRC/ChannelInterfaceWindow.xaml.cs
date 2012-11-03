using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_IRC
{
    /// <summary>
    /// Interaction logic for serverWindow.xaml
    /// </summary>
    public partial class ChannelInterfaceWindow : UserControl
    {
        private List<string> _users = new List<string>();

        public Channel Channel { get; private set; }
        public List<string> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        public ChannelInterfaceWindow(Channel channel)
        {
            InitializeComponent();
            this.Channel = channel;
            Channel.messageSent += new EventHandler(Channel_messageReceived);
            Channel.membersChanged += new EventHandler(updateUsers);
        }

        void Channel_messageReceived(object sender, EventArgs e)
        {
            object[] param = new object[1];
            param[0] = (string)((e as NetworkMessageEventArgs).Message + Environment.NewLine);
            this.Dispatcher.Invoke(new updateString(updateTextBox), param);
            //
        }

        void updateUsers(object sender, EventArgs e)
        {
            var users = (e as MembersChangedEventArgs).Users;
            string compiledString = string.Empty;
            foreach (ChannelUser user in users)
            {
                compiledString += Dispatcher.Invoke(new getString(user.ToString), null);
                compiledString += Environment.NewLine;
            }
            object[] param = new object [1] { compiledString };
            this.Dispatcher.Invoke(new updateString(updateUsersTextBox), param);
        }

        void updateTextBox(string msg)
        {
            textBox2.Text += msg;
            textBox2.ScrollToEnd();
        }

        void updateUsersTextBox(string msg)
        {
            usersTextBox.Text = msg;
        }

        delegate void updateString(string str);
        delegate string getString();

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Channel.SubmitMessage(inputBox.Text);
                inputBox.Text = String.Empty;
            }
        }
    }
}
