using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

/*
 Alex Gulewich 
 Feb, 12, 2024
 Attack
 The attack base class to be inherited later on
 */

namespace DungeonCrawler.Attacks
{
    internal abstract class Attack
    {
        // The attributes for attack
        protected int[,] mAffectedTiles;
        protected ConsoleColor mColor;
        protected int timer = 2;
        protected int maxTimer = 2;
        protected bool affectedtilesNeedClearing = false;

        // the parent
        protected Pawn parent;

        // The constructors
        public Attack(Pawn newParent) 
        {
            parent = newParent;
            mColor = ConsoleColor.DarkRed;
        }

        public Attack(Pawn newParent, ConsoleColor myColor) 
        {
            parent = newParent;
            mColor = myColor;
        }

        // Does the attack as per described
        public abstract void Action( Direction dir );


        // Allows removal of effects data
        public bool CheckIfClearAffectedTiles()
        {
            // returns if the timer has ended
            if (timer >= maxTimer)
            {
                // Checks if temp stuff needs clearing out
                if (affectedtilesNeedClearing)
                {
                    // Cleans up the tiles on map
                    for (int x = 0; x < mAffectedTiles.GetLength(0); x++) 
                    {
                        // ensures we do not go out of bounds of map array
                        if (Application.CurrentMap.Data.GetLength(0) - 1 < x || x < 0 )
                        {
                            break;
                        }

                        Application.CurrentMap.Data[mAffectedTiles[x, 0], mAffectedTiles[x, 1]].effectColour = ConsoleColor.Black ;
                    }

                    // Clears out vars for later use
                    affectedtilesNeedClearing = false;
                    mAffectedTiles = new int[9, 9];
                }
                return true;
            }

            // Increments the timer
            timer++;
            return false;
        }
    }
}

