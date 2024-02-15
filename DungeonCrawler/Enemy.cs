using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Enemy : Entity
    {

        public Enemy()
        {
            sprite = '^';
            spriteColour = ConsoleColor.Red;
        }

    }
}
