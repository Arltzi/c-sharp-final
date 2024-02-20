using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 This method was craeted in order to allow the ease of clearing out attack effects.
 */

namespace DungeonCrawler.Attacks
{
    internal class AttackAutoClear
    {
        // The singleton for ease of access
        public static AttackAutoClear staticRef = new AttackAutoClear();

        // Class attributes
        List<Attack> triggeredAttacks = new List<Attack>();


        // Adds new item to list
        public Attack attack
        {
            set 
            {
                // makes sure we're not duplicating into the list
                if (triggeredAttacks.Contains(value)) 
                {
                    return;
                }
                triggeredAttacks.Add(value); 
            }
        }

        public void CheckAttackOverlap()
        {

            if (triggeredAttacks.Count == 0)
            {
                return;
            }

            for (int x = 0; x < triggeredAttacks.Count; x++)
            {
                triggeredAttacks[x].CheckForDamage();
            }

        }

        // Checks thorugh all activated attacks to see if they should be cleared
        public void CheckAttacksToClean () 
        {
            // Checks to see if there is anything to clean
            if (triggeredAttacks.Count == 0) 
            {
                return;
            }



            // Checks everything if they should be clears and clears it if it should
            for (int x = 0; x < triggeredAttacks.Count; x++) 
            {
                // chjecks if the tile should be claered and pops from the list if it was
                if ( triggeredAttacks[x].CheckIfClearAffectedTiles() ) 
                {
                    triggeredAttacks.RemoveAt(x);
                }
            }
        }
    }
}
