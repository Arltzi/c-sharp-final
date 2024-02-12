using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    enum Direction
    {
        UP = 1,
        DOWN = 2,
        RIGHT = 3,
        LEFT = 4
    }

    internal class Pawn : Entity
    {



        protected Direction direction;

        public Pawn()
        {


        }
        protected bool CollisionCheck()
        {

            switch (direction)
            {
                case Direction.UP:
                    if ((y - 1) == -1) // Top bounds check
                        return false;
                    else if (Application.CurrentMap.Data[x, y - 1] == (int)TileTypes.WALL) // Wall tile collision check
                        return false;
                    break;
                case Direction.DOWN:
                    if ((y + 1) == Application.mapY && (x < 20 || x >= 36)) // Bottom bounds check (allowing player to go down in middle for doorway) (temp)
                        return false;
                    else if (Application.CurrentMap.Data[x, y + 1] == (int)TileTypes.WALL) // Wall tile collision check
                        return false;
                    break;
                case Direction.RIGHT:
                    if ((x + 1) == Application.mapX) // Right bounds check
                        return false;
                    else if (Application.CurrentMap.Data[x + 1, y] == (int)TileTypes.WALL) // Wall tile collision check
                        return false;
                    break;
                case Direction.LEFT:
                    if ((x - 1) == -1) // Left bounds check
                        return false;
                    else if (Application.CurrentMap.Data[x - 1, y] == (int)TileTypes.WALL) // Wall tile collision check
                        return false;
                    break;

            }


            return true;

        }


    }
}
