using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class DeathMenu : Menu
    {

        public DeathMenu()
        {
            text = "   _________    __  _________\r\n" +
                "  / ____/   |  /  |/  / ____/\r\n" +
                " / / __/ /| | / /|_/ / __/   \r\n" +
                "/ /_/ / ___ |/ /  / / /___   \r\n" +
                "\\____/_/_ |_/_/__/_/_____/   \r\n" +
                "  / __ \\ |  / / ____/ __ \\   \r\n" +
                " / / / / | / / __/ / /_/ /   \r\n" +
                "/ /_/ /| |/ / /___/ _, _/    \r\n" +
                "\\____/ |___/_____/_/ |_|     \r\n" +
                "                             ";

            buttons = new string[] { "Try Again", "Exit" };
            selectedButton = 1;

        }

        public override void PressButton()
        {
            switch (selectedButton)
            {
                case 1:
                    Application.ReplayLevel();
                    break;
                case 2:
                    Application.CloseApp();
                    break;


            }
        }


    }
}
