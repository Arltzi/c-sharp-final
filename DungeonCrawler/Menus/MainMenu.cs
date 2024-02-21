using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonCrawler
{
    internal class MainMenu : Menu
    {
        public MainMenu()
        {
            //text = "       __                                 \r\n" +
            //    "  ____/ /_  ______  ____ ____  ____  ____ \r\n" +
            //    " / __  / / / / __ \\/ __ `/ _ \\/ __ \\/ __ \\\r\n" +
            //    "/ /_/ / /_/ / / / / /_/ /  __/ /_/ / / / /\r\n" +
            //    "\\__,_/\\__,_/_/ /_/\\__, /\\___/\\____/_/ /_/ \r\n" +
            //    "  ______________ /____/  __/ /__  _____   \r\n" +
            //    " / ___/ ___/ __ `/ | /| / / / _ \\/ ___/   \r\n" +
            //    "/ /__/ /  / /_/ /| |/ |/ / /  __/ /       \r\n" +
            //    "\\___/_/   \\__,_/ |__/|__/_/\\___/_/        \r\n" +
            //    "                                          ";

            text = "    ____  __  ___   __________________  _   __\r\n" +
                "   / __ \\/ / / / | / / ____/ ____/ __ \\/ | / /\r\n" +
                "  / / / / / / /  |/ / / __/ __/ / / / /  |/ / \r\n" +
                " / /_/ / /_/ / /|  / /_/ / /___/ /_/ / /|  /  \r\n" +
                "/_____/\\____/_/ |_/\\____/_____/\\____/_/ |_/   \r\n" +
                "   / __ )/   |  / /   / /   / ____/ __ \\      \r\n" +
                "  / __  / /| | / /   / /   / __/ / /_/ /      \r\n" +
                " / /_/ / ___ |/ /___/ /___/ /___/ _, _/       \r\n" +
                "/_____/_/  |_/_____/_____/_____/_/ |_|        \r\n" +
                "                                              ";

            buttons = new string[] { "Play", "About", "Exit" };
            selectedButton = 1;

        }

        override public void PressButton()
        {
            switch (selectedButton)
            {
                case 1:
                    Application.UnPause();
                    Console.CursorVisible = false;
                    Console.Clear();
                    break;
                case 2:
                    Application.SwapMenu(MenuType.ABOUT);
                    Console.Clear();
                    break;
                case 3:
                    Application.CloseApp();
                    break;

            }
        }


    }
}
