using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Enemy : Pawn
    {

        public Enemy()
        {
            sprite = '^';
            spriteColour = ConsoleColor.Red;
            health = 5;
        }

    }
}
