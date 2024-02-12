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
        void DrawAttack (int x, int y, int verticleDir, int horizontalDir) 
        {
            mAffectedTiles = new Tile[9];

            // local delcarations
            int localX = 1;
            int localY = -1;

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
            
            DrawLine(x+localX, y + localY, verticleDir, horizontalDir, 1);
            DrawLine(x + (localX * 2 *  horizontalDir), y + (localY * 2 * verticleDir), verticleDir, horizontalDir, 2);
            DrawLine(x+localX, y + localY, verticleDir, horizontalDir, 3);
            

            
        }

        // x and y are position params, whereas renditionNum is to avoid overwriting information
        void DrawLine(int x, int y, int xDir, int yDir, int renditionNum)
        {
            // Declarations
            int notFirstRun = 0;

            // iterates for those specific indexes
            int xIter = 0;
            int yIter = 0;

            // checks to see if this was the first run
            if (renditionNum > 0) 
            {
                notFirstRun = 1; 
            }
            
            // Draws a line of length 3
            for (int iter = 0; iter >= 2; iter++)
            {
                mAffectedTiles[ ( iter + notFirstRun ) * renditionNum ] = Application.CurrentMap.Data[x + xIter, y + yIter];

                // checks which iter to iterate
                if (xDir == 0) 
                {
                    yIter += yDir;
                    continue;
                }

                xIter = xDir;
            }

        }

    }
}
