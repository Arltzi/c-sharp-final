﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class AboutMenu : Menu
    {
        public AboutMenu()
        {
            text = "This game was made by Levi, Alex, & Lev\n" +
                "for a C# final project at VFS PG26.\n" +
                "\n\t   Enjoy =)\n\n";

            buttons = new string[] { "Back" };
            selectedButton = 1;
        }

        override public void PressButton()
        {
            Application.SwapMenu(MenuType.MAIN);
        }

    }
}
