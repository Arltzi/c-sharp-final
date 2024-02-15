using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 Alex Gulewich 
 Feb, 12, 2024
 Slash
 Creates a arc which hurts the target that it hits
 */


namespace DungeonCrawler.Attacks
{
    internal class Slash : Attack
    {
        // Calls the parent constructor to assign the default color
        public Slash() : base() 
        {
            mAffectedTiles = new Tile[9];
        }

        // Calls the parent constructor to assign a custom color
        public Slash(ConsoleColor myColor) : base(myColor) 
        {
        
        }

        
        public override void Action(int x, int y, Direction dir)
        {
            // Application.CurrentMap;

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

        }

        // Gets the referances to the arc and stores then into mAffectedTiles
        void DrawAttack (int x, int y, int horizontalDir, int verticleDir) 
        {

            /*
             * TODO see if this works ( something doesn't quite seem right )
            if (verticleDir == 0) 
            {
                localX = horizontalDir;
            }

            else 
            {
                localX = verticleDir;

            }
            */

            // Draws the attack
            DrawLine(x, y, verticleDir, horizontalDir, 3, 1);
            DrawLine(x + (horizontalDir * 2), y + (verticleDir * 2), verticleDir, horizontalDir, 2, 2);
            DrawLine(x  + -(horizontalDir * 2), y + -(verticleDir * 2), verticleDir, horizontalDir, 2, 3);
            

            
        }

        // x and y are position params, whereas renditionNum is to avoid overwriting information  ( start offset should be default at one )
        void DrawLine(int x, int y, int xDir, int yDir, int startOffset, int renditionNum)
        {
            // Declarations
            int notFirstRun = 0;

            // iterates for those specific indexes
            int xIter = xDir * startOffset;
            int yIter = yDir * startOffset;

            // checks to see if this was the first run
            if (renditionNum > 0)
            {
                notFirstRun = 1;
            }

            // Draws a line of length 3
            for (int iter = 0; iter < 2; iter++)
            {
                // ensures we do not go out of bounds of map array
                if (Application.CurrentMap.Data.GetLength(0)-1 < x + xIter || Application.CurrentMap.Data.GetLength(1)-1 < y + yIter || x + xIter < 0 || y + yIter < 0)
                {
                    break;
                }

                // Draw out the affected stuff
                mAffectedTiles[iter] = Application.CurrentMap.Data[x + xIter, y + yIter];
                Application.CurrentMap.Data[x + xIter, y + yIter].effectColour = ConsoleColor.White;

                // Iterates along proper direction
                yIter += yDir;
                xIter += xDir;
            }

        }

    }
}
