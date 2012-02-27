using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class Game
    {
        private int height;
        private int width;
        private int mines;
        private int dismantles = 0;
        private Tile[,] tiles;

        public Game(int width, int height, int mines)
        {
            this.height = height;
            this.width = width;
            this.mines = mines;
            tiles = new Tile[width, height];
            for (int i = 0; i < this.width; i++)
                for (int j = 0; j < this.height; j++)
                    tiles[i, j] = new Tile();
            Random rand = new Random();
            for (int i = 0; i < mines; )
            {
                int r1 = rand.Next(width);
                int r2 = rand.Next(height);
                if (tiles[r1, r2].status == Tile.TileStatus.CLEAN)
                {
                    tiles[r1, r2].status = Tile.TileStatus.MINED;
                    i++;
                }
            }
        }

        public string leftclick(int x, int y)
        {
            if (tiles[x, y].opened)
                return "ok";
            if (tiles[x, y].addon == Tile.TileAddon.DISMANTLED || tiles[x, y].addon == Tile.TileAddon.FLAGGED)
                return "ok";
            if (tiles[x, y].status == Tile.TileStatus.MINED)
                return explode() + x + "*" + y + "*" + "r";
            else
            {
                string response = String.Empty;
                //string response = "reveal " + reveal(x, y);
                if (surroundingMineCount(x, y) > 0)
                    return "reveal " + reveal(x, y);
                else
                    response = "reveal " + openTile(x, y);
                response = response.Remove(response.Length - 1);
                return response;
            }
        }

        public string openTile(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                if (!(tiles[x, y].status == Tile.TileStatus.MINED) && !tiles[x, y].opened)
                {
                    tiles[x, y].opened = true;
                    if (surroundingMineCount(x, y) == 0)
                    {
                        
                        // STACK OVERFLOW!!!
                        string response = x + "*" + y + "*" + surroundingMineCount(x, y) + " ";
                        response += openTile(x - 1, y);
                        response += openTile(x + 1, y);
                        response += openTile(x, y + 1);
                        response += openTile(x, y - 1);
                        response += openTile(x - 1, y + 1);
                        response += openTile(x - 1, y - 1);
                        response += openTile(x + 1, y + 1);
                        response += openTile(x + 1, y - 1);
                        return response;
                    }
                    else
                        return (x + "*" + y + "*" + surroundingMineCount(x, y) + " ");
                }
            }
            return String.Empty;
        }

        public string explode()
        {
            string ret = "explode ";
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    ret += reveal(i, j) + " ";
                    tiles[i, j].addon = Tile.TileAddon.NONE;
                }
            return ret;
        }
        public string reveal(int x, int y)
        {
            tiles[x, y].opened = true;
            if (tiles[x, y].status == Tile.TileStatus.MINED)
                return (x + "*" + y + "*" + "m");

            else
            {
                int minesCount = surroundingMineCount(x, y);
                return (x + "*" + y + "*" + minesCount);
            }
            
        }

        private int surroundingMineCount(int x, int y)
        {
            int count = 0;
            if (x > 0 && tiles[x - 1, y].status == Tile.TileStatus.MINED) count++;
            if (x > 0 && y > 0 && tiles[x - 1, y - 1].status == Tile.TileStatus.MINED) count++;
            if (x > 0 && y < height - 1 && tiles[x - 1, y + 1].status == Tile.TileStatus.MINED) count++;
            if (y < height - 1 && tiles[x, y + 1].status == Tile.TileStatus.MINED) count++;
            if (y > 0 && tiles[x, y - 1].status == Tile.TileStatus.MINED) count++;
            if (x < width - 1 && tiles[x + 1, y].status == Tile.TileStatus.MINED) count++;
            if (x < width - 1 && y < height - 1 && tiles[x + 1, y + 1].status == Tile.TileStatus.MINED) count++;
            if (x < width - 1 && y > 0 && tiles[x + 1, y - 1].status == Tile.TileStatus.MINED) count++;
            return count;
        }

        public string rightclick(int x, int y)
        {
            if (!tiles[x, y].opened)
            {
                switch (tiles[x, y].addon)
                {
                    case Tile.TileAddon.NONE:
                        tiles[x, y].addon = Tile.TileAddon.DISMANTLED;
                        dismantles++;
                        return "dismantle " + x + " " + y + " " + (mines - dismantles);
                    case Tile.TileAddon.DISMANTLED:
                        tiles[x, y].addon = Tile.TileAddon.FLAGGED;
                        dismantles--;
                        return "flag " + x + " " + y + " " +  (mines - dismantles);
                    case Tile.TileAddon.FLAGGED:
                        tiles[x, y].addon = Tile.TileAddon.NONE;
                        return "none " + x + " " + y;
                    default:
                        return String.Empty;
                }
            }
            else
                return ("ok");

        }
    }
     
  
}
