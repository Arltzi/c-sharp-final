using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Map
    {
        // Types used for loading
        enum TileType 
        {
            EMPTY = 0,
            WALL = 1,
            PLAYER = 2,
            ENEMY = 3
        }

        // Reused wall instance for all wall tiles
        private Wall wall = new Wall();

        public const int mapSizeX = 56;
        public const int mapSizeY = 20;

        private Tile[,] data;

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

        public Map()
        {
            data = new Tile[mapSizeX, mapSizeY];
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

                        char currentChar = mapString[i + (j * mapSizeX)];

                        TileType currentType = (TileType)int.Parse(currentChar.ToString());

                        Tile currentTile = new Tile();

                        // Assigning tiles occupant
                        if (currentType == TileType.PLAYER)
                        {
                            currentTile.SetOccupant(Application.player);
                            Application.player.x = i;
                            Application.player.y = j;
                        }
                        else if (currentType == TileType.ENEMY)
                        {
                            Enemy enemy = Application.entityManager.CreateEnemy();
                            currentTile.SetOccupant(enemy);
                        }
                        else if (currentType == TileType.WALL)
                        {
                            currentTile.SetOccupant(wall);
                        }

                        data[i, j] = currentTile;
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
