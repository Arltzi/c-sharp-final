using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Enemy : Pawn
    {
        public Vector2[]? pathToPlayer;

        public Enemy()
        {
            sprite = '^';
            spriteColour = ConsoleColor.Red;
            health = 5;
        }

    }
}
