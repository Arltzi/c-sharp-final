﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Wall : Entity
    {

        public Wall()
        {
            sprite = '#';
            color = 0x0002;
            spriteColour = ConsoleColor.Yellow;
        }

    }
}
