using System;
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
            logo = "This game was made by absolute goats\n\n\n\n";

            buttons = new string[] { "Back" };
            selectedButton = 1;
        }

        override public void PressButton()
        {
            Application.SwapMenu(MenuType.MAIN);
        }

    }
}
