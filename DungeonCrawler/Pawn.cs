using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Pawn : Entity
    {

        protected InputMap movementDir;

        public Pawn()
        {


        }
        protected bool CollisionCheck()
        {

            switch (movementDir)
            {
                case InputMap.UP:
                    if ((y - 1) == -1)
                        return false;
                    break;
                case InputMap.DOWN:
                    if ((y + 1) == Application.windowY && (x < 20 || x >= 36))
                        return false;
                    break;
                case InputMap.RIGHT:
                    if ((x + 1) == Application.windowX)
                        return false;
                    break;
                case InputMap.LEFT:
                    if ((x - 1) == -1)
                        return false;
                    break;

            }

            return true;

        }


    }
}
