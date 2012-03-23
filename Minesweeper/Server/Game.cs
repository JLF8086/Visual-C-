using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    class Game
    {
        private int height;
        private int width;
        private int mines;
        private int dismantles = 0;
        private Tile[,] tiles;
        public int time = 0;
        private System.Timers.Timer timer;
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
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(onTick);
            timer.Start();
        }

        private void onTick(object sender, EventArgs e)
        {
            time++;
        }

        public string LeftClick(int x, int y)
        {
            if (tiles[x, y].opened || IsOver())
                return "ok";
            if (tiles[x, y].addon == Tile.TileAddon.DISMANTLED || tiles[x, y].addon == Tile.TileAddon.FLAGGED)
                return "ok";
            if (tiles[x, y].status == Tile.TileStatus.MINED)
            {
                Console.WriteLine("EXPLOSION");
                return Explode() + x + "*" + y + "*" + "r";
            }
            else
            {
                string response = String.Empty;
                //string response = "reveal " + reveal(x, y);
                if (SurroundingMineCount(x, y) > 0)
                    response = "reveal " + Reveal(x, y);
                else
                {
                    response = "reveal " + OpenTile(x, y);
                    response = response.Remove(response.Length - 1);
                }
                return response;
            }
        }

        public string RightClick(int x, int y)
        {
            if (!tiles[x, y].opened && !IsOver())
            {
                string response = String.Empty;
                switch (tiles[x, y].addon)
                {
                    case Tile.TileAddon.NONE:
                        tiles[x, y].addon = Tile.TileAddon.DISMANTLED;
                        dismantles++;
                        response = "dismantle " + x + " " + y + " " + (mines - dismantles);
                        break;
                    case Tile.TileAddon.DISMANTLED:
                        tiles[x, y].addon = Tile.TileAddon.FLAGGED;
                        dismantles--;
                        response = "flag " + x + " " + y + " " + (mines - dismantles);
                        break;
                    case Tile.TileAddon.FLAGGED:
                        tiles[x, y].addon = Tile.TileAddon.NONE;
                        response = "none " + x + " " + y;
                        break;

                }
                return response;
            }
            else
                return ("ok");
        }

        public string OpenTile(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
            {
                if (!(tiles[x, y].status == Tile.TileStatus.MINED) && !tiles[x, y].opened)
                {
                    tiles[x, y].opened = true;
                    if (tiles[x, y].addon == Tile.TileAddon.DISMANTLED)
                        dismantles--;
                    if (SurroundingMineCount(x, y) == 0)
                    {  
                        // STACK OVERFLOW!!!
                        string response = x + "*" + y + "*" + SurroundingMineCount(x, y) + " ";
                        response += OpenTile(x - 1, y);
                        response += OpenTile(x + 1, y);
                        response += OpenTile(x, y + 1);
                        response += OpenTile(x, y - 1);
                        response += OpenTile(x - 1, y + 1);
                        response += OpenTile(x - 1, y - 1);
                        response += OpenTile(x + 1, y + 1);
                        response += OpenTile(x + 1, y - 1);
                        return response;
                    }
                    else
                        return (x + "*" + y + "*" + SurroundingMineCount(x, y) + " ");
                }
            }
            return String.Empty;
        }

        public string Explode()
        {
            timer.Close();
            string ret = "explode ";
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    ret += Reveal(i, j) + " ";
                    tiles[i, j].addon = Tile.TileAddon.NONE;
                }
                
            return ret;
        }
        public string Reveal(int x, int y)
        {
            tiles[x, y].opened = true;
            if (tiles[x, y].status == Tile.TileStatus.MINED)
                return (x + "*" + y + "*" + "m");

            else
            {
                int minesCount = SurroundingMineCount(x, y);
                return (x + "*" + y + "*" + minesCount);
            }
            
        }

        private int SurroundingMineCount(int x, int y)
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

        

        public bool IsOver()
        {
            
            foreach (Tile t in tiles)
            {
                //Console.WriteLine(t.status + " " + t.addon + " " + t.opened + " ");
                if (!((t.status == Tile.TileStatus.MINED && t.addon == Tile.TileAddon.DISMANTLED)
                    || (t.opened && t.status == Tile.TileStatus.CLEAN)))
                    return false;
            }
            timer.Close();
            return true;
        }

        public int MinesLeft { get { return mines - dismantles; } }
    }
     
  
}
