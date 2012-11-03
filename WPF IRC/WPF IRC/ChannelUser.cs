using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPF_IRC
{
    public class ChannelUser
    {
        public ChannelUser(string name)
        {
            if (name[0] == '@')
            {
                ChanModes[ChanMode.OP] = true;
                _name = name.Substring(1);
            }
            else if (name[0] == '+')
            {
                ChanModes[ChanMode.VOICE] = true;
                _name = name.Substring(1);
            }
            else
                this._name = name;
        }

        private string _name;
        public Dictionary<ChanMode, bool> ChanModes = new Dictionary<ChanMode, bool>()
            {
                { ChanMode.OP, false }, { ChanMode.VOICE, false }
            };

        public override string ToString()
        {
           string ret = String.Empty;
            if (ChanModes[ChanMode.OP])
                ret += "@";
            else if (ChanModes[ChanMode.VOICE] == true)
                ret += "+";
            return ret + _name;
        }

        public string SimpleName
        {
            get { return _name; }
        }

    }

    public enum ChanMode
    {
        VOICE, OP
    }

    
}
