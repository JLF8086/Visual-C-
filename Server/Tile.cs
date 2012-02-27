using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Tile
    {
        public enum TileStatus
        {
            MINED, CLEAN
        }
        public enum TileAddon
        {
            NONE, DISMANTLED, FLAGGED
        }
        public bool opened;
        private event EventHandler Open;
        public TileStatus status = TileStatus.CLEAN;
        public TileAddon addon = TileAddon.NONE;


    }

    


}
