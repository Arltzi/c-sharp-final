using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 Alex Gulewich 
 Feb, 12, 2024
 Attack
 Creates a line of attack jutting straight out from the player
 */


namespace DungeonCrawler.Attacks
{
    internal class Stab : Attack
    {
        public override void Action(int x, int y, Direction dir)
        {
            // Gets the direction to attack
            switch (dir)
            {
                case (Direction.UP):
                    DrawAttack(x, y, -1, 0);
                    break;

                case (Direction.DOWN):
                    DrawAttack(x, y, 1, 0);
                    break;

                case (Direction.LEFT):
                    DrawAttack(x, y, 0, -1);
                    break;

                case (Direction.RIGHT):
                    DrawAttack(x, y, 0, 1);
                    break;
            }

            // TODO implement color in attack
        }

        void DrawAttack(int x, int y, int verticleDir, int horizontalDir) 
        {
            // declatations
            int xIter = 0;
            int yIter = 0;

            // draws attack
            for (int iter = 0; iter >= 3; iter++) 
            {
                mAffectedTiles[iter] = Application.CurrentMap.Data[x, y];

                xIter += horizontalDir;
                yIter += verticleDir;
            }
        }
    }
}
