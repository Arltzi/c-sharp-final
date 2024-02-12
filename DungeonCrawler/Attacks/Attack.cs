using System;
using System.Collections.Generic;
using System.Linq;
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
        protected Tile[] mAffectedTiles;
        protected ConsoleColor mColor;

        // The constructors
        public Attack() 
        {
            mColor = ConsoleColor.DarkRed;
        }

        public Attack(ConsoleColor myColor) 
        {
            mColor = myColor;
        }

        // Does the attack as per described
        public abstract void Action(int x, int y, Direction dir);
    }
}
