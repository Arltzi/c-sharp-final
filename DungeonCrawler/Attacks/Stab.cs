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
        public Stab() : base() 
        {
            // assigns in accordance to tile usage
            mAffectedTiles = new Tile[9];
        }
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
            for (int iter = 0; iter < 3; iter++) 
            {
                mAffectedTiles[iter] = Application.CurrentMap.Data[x + xIter, y + yIter];
                Application.CurrentMap.Data[x + xIter, y + yIter].effectColour = ConsoleColor.White;

                // iterates across the proper acess in proper direction
                xIter += horizontalDir;
                yIter += verticleDir;
            }

            // Prepare ntuff that needs clearing to be cleared
            affectedtilesNeedClearing = true;
        }

        public void CheckIfClearAffectedTiles () 
        {
            // returns if the timer has ended
            if (timer >= maxTimer)
            {
                // Checks if temp stuff needs clearing out
                if (affectedtilesNeedClearing) 
                {
                    // Clears out vars for later use
                    affectedtilesNeedClearing = true;
                    mAffectedTiles = new Tile[9];
                }
                return;
            } 
            timer++;
        }
    }
}
