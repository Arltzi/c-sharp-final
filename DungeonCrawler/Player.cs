using DungeonCrawler.Attacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Player : Pawn
    {
        private int maxHealth = 10;

        public int MaxHealth { 
            get { return maxHealth; }
            private set { maxHealth = value; }
        }


        public Player()
        {
            sprite = '^';
            spriteColour = ConsoleColor.Green;
            color = 0x0010;
            health = MaxHealth;
        }

        public override void Die() { 
        

        }

    }

    
}

