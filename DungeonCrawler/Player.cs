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

        public void Move(InputMap dir)
        {
            movementDir = dir;
            switch (movementDir)
            {
                case InputMap.UP:
                    if (CollisionCheck() == true)
                        y--;
                    sprite = '^';
                    break;
                case InputMap.DOWN:
                    if (CollisionCheck() == true)
                        y++;
                    sprite = 'v';
                    break;
                case InputMap.RIGHT:
                    if (CollisionCheck() == true)
                        x++;
                    sprite = '>';
                    break;
                case InputMap.LEFT:
                    if (CollisionCheck() == true)
                        x--;
                    sprite = '<';
                    break;

            }

        }
    }
}

