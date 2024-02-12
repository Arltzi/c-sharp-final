using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Player : Pawn
    {

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;

            sprite = '^';
        }


        public bool Move(InputMap input)
        {
            direction = (Direction)input;
            switch (direction)
            {
                case Direction.UP:

                    sprite = '^';

                    if (CollisionCheck() == true)
                    {
                        y--;
                        return true;
                    }

                    break;

                case Direction.DOWN:

                    sprite = 'v';

                    if (CollisionCheck() == true)
                    {
                        y++;
                        return true;
                    }

                    break;

                case Direction.RIGHT:

                    sprite = '>';

                    if (CollisionCheck() == true)
                    {
                        x++;
                        return true;
                    }

                    break;

                case Direction.LEFT:

                    sprite = '<';

                    if (CollisionCheck() == true)
                    {
                        x--;
                        return true;
                    }
                    break;

            }

            return false;

        }
    }
}

