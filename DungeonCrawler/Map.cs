using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Map
    {

        public const int mapSizeX = 56;
        public const int mapSizeY = 20;

        private Tile[,] data;

        private int enemyCount = 0;

        private string name = "No name";

        

        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        public Tile[,] Data
        {
            get { return data; }
            private set { data = value; }
        }

        private int playerX, playerY;

        public int PlayerX
        {
            get { return playerX; }
            private set { playerX = value; }
        }

        public int PlayerY
        {
            get { return playerY; }
            private set { playerY = value; }
        }


        public Map()
        {
            data = new Tile[mapSizeX, mapSizeY];
        }
        
        public void UpdatePlayerLocation(Player p)
        {
            // Store old location
            int oldX = playerX;
            int oldY = playerY;

            // Update current
            data[p.x, p.y] = Tile.PLAYER;
            data[oldX, oldY] = Tile.EMPTY;

            // Set old location to empty
            playerX = p.x;
            playerY = p.y;

        }

        public bool Load(string name)
        {
            this.name = name;
            string path = name + ".txt";

            try
            {
                // Reading in file
                string fileContent = File.ReadAllText(path);
                // Stripping newlines
                string mapString = fileContent.Replace(Environment.NewLine, "");

                for (int i = 0; i < mapSizeX; i++)
                {

                    for (int j = 0; j < mapSizeY; j++)
                    {

                        char currentChar = mapString[i + (j * 56)];
                        data[i, j] = (Tile)int.Parse(currentChar.ToString());

                        // Assigning players class it's X & Y if found
                        if (data[i, j] == Tile.PLAYER)
                        {
                            playerX = i;
                            playerY = j;
                        }
                        else if (data[i,j] == Tile.ENEMY)
                        {

                        }

                    }

                }

                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }

        }


    }
}
