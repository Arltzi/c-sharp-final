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
        private int iFrames = 0;

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

        public void Heal(int amount)
        {
            health += amount;
            if(health > maxHealth)
            {
                health = maxHealth;
            }
        }

        public override void TakeDamage()
        {
            if(iFrames == 0)
            {
                iFrames = 15;
                base.TakeDamage();
            }

        }

        public void TickIFrame()
        {
            if(iFrames > 0)
                iFrames--;
        }

        public bool IsNextToEnemy()
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    // Checking within map bounds
                    if(y + j <= Map.mapHeight && x + i <= Map.mapWidth && y - 1 + j >= 0 && x - 1 + i >= 0)
                    {
                        if(Application.CurrentMap.Data[(x - 1) + i, (y - 1) + j].Occupant != null)
                        {
                            if (Application.CurrentMap.Data[(x - 1) + i, (y - 1) + j].Occupant.GetType() == typeof(Enemy))
                                return true;

                        }

                    }
                }

            }

            return false;
        }

    }

    
}

