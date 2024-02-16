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

        public Player()
        {
            sprite = '^';
            spriteColour = ConsoleColor.Green;
            color = 0x0010;
        }

    }
}

