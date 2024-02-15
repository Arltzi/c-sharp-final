using DungeonCrawler;
using DungeonCrawler.Attacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Pawn : Entity
    {

        public int x, y;
        public Attack attack = new Slash();

        protected InputMap direction;

        public Pawn()
        {


        }

        public bool Move(InputMap input)
        {
            int oldX = x;
            int oldY = y;

            direction = input;
            if ((int)direction != 0)
            {
                attack.Action(x, y, (Direction)((int)input));
            }

            switch (direction)
            {
                case InputMap.UP:

                    sprite = '^';

                    if (CollisionCheck() == true)
                    {
                        y--;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }
                    break;

                case InputMap.DOWN:

                    sprite = 'v';

                    if (CollisionCheck() == true)
                    {
                        y++;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }

                    break;

                case InputMap.RIGHT:

                    sprite = '>';

                    if (CollisionCheck() == true)
                    {
                        x++;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }

                    break;

                case InputMap.LEFT:

                    sprite = '<';

                    if (CollisionCheck() == true)
                    {
                        x--;
                        Application.CurrentMap.Data[oldX, oldY].EmptyTile();
                        Application.CurrentMap.Data[x, y].SetOccupant(this);
                        return true;
                    }
                    break;

            }

            return false;

        }

        protected bool CollisionCheck()
        {

            switch (direction)
            {
                case InputMap.UP:
                    if ((y - 1) == -1) // Top bounds check
                        return false;
                    else if (Application.CurrentMap.Data[x, y - 1].Occupant != null) // Wall tile collision check
                        return false;
                    break;
                case InputMap.DOWN:
                    if ((y + 1) == Application.mapY && (x < 20 || x >= 36)) // Bottom bounds check (allowing player to go down in middle for doorway) (temp)
                        return false;
                    else if (Application.CurrentMap.Data[x, y + 1].Occupant != null) // Wall tile collision check
                        return false;
                    break;  
                case InputMap.RIGHT:
                    if ((x + 1) == Application.mapX) // Right bounds check
                        return false;
                    else if (Application.CurrentMap.Data[x + 1, y].Occupant != null) // Wall tile collision check
                        return false;
                    break;
                case InputMap.LEFT:
                    if ((x - 1) == -1) // Left bounds check
                        return false;
                    else if (Application.CurrentMap.Data[x - 1, y].Occupant != null) // Wall tile collision check
                        return false;
                    break;

            }


            return true;

        }


    }
}
