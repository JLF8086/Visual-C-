using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF_IRC
{
    public class Channel
    {
        public event EventHandler membersChanged;
        public event EventHandler messageSent;
        public event EventHandler channelLeft;

        private IrcNetwork _network;
        private string _name;
        private List<ChannelUser> _users = new List<ChannelUser>();

        public string Name { get { return _name; } }

        public IrcNetwork Network { get { return _network; } }

        
        
        public Channel(IrcNetwork network, string name)
        {
            _network = network;
            _name = name;
        }

        public void SetUsers(string[] users)
        {
            foreach (string user in users)
            { 
                _users.Add(new ChannelUser(user));
            }
            if (membersChanged != null)
                membersChanged(this, new MembersChangedEventArgs() { Users = _users });
        }

        public void WriteChannelMessage(string sender, string msg)
        {
            if (sender == null)
            {
                if (messageSent != null)
                    messageSent(this, new NetworkMessageEventArgs(msg));
                return;
            }
            if (messageSent != null)
            {
                string timestamp = DateTime.Now.ToString("[HH:mm:ss]");
                msg = timestamp + ' ' + sender + ": " + msg;
                if (messageSent != null)
                    messageSent(this, new NetworkMessageEventArgs(msg));
            }
        }

        public void SubmitMessage(string msg)
        {
            _network.ParseChannelMessage(this, msg);
        }




        public void AddUser(string p)
        {
            _users.Add(new ChannelUser(p));
            membersChanged(this, new MembersChangedEventArgs() { Users = _users });
        }

        public void LeaveChannel()
        {
            if (this.channelLeft != null)
                channelLeft(this, null);
        }

        public void DisplayTopic(string topic)
        {
            this.WriteChannelMessage(null, "TOPIC: " + topic);
        }

        internal void ChangeUserChannelMode(string changer, string change, string changee)
        {
            switch(change)
            {
                case "+v":
                    _users.Single(s => s.SimpleName == changee).ChanModes[ChanMode.VOICE] = true; break;
                case "-v":
                    _users.Single(s => s.SimpleName == changee).ChanModes[ChanMode.VOICE] = false; break;
                case "+o":
                    _users.Single(s => s.SimpleName == changee).ChanModes[ChanMode.OP] = true; break;
                case "-o":
                    _users.Single(s => s.SimpleName == changee).ChanModes[ChanMode.OP] = false; break;
            }
            membersChanged(this, new MembersChangedEventArgs() { Users = _users } );
            string msg = changer + " sets mode " + change + ' ' + changee;
            WriteChannelMessage(_network.DisplayName, msg);
        }


        internal void ReportQuit(string name)
        {
            ChannelUser user = _users.Single(s => s.SimpleName == name);
            if (user != null)
                _users.Remove(user);
            WriteChannelMessage(_network.DisplayName, name + " has quit IRC");
            membersChanged(this, new MembersChangedEventArgs() { Users = _users });
        }
    }

    public class MembersChangedEventArgs : EventArgs
    {
        public List<ChannelUser> Users { get; set; }
    }

    public class ChannelEventArgs : EventArgs
    {
        public Channel Channel { get; set; }
    }
}
