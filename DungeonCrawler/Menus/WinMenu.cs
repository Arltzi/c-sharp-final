using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class WinMenu : Menu
    {

        public WinMenu()
        {

            text = " _    ______________________  ______  __\r\n" +
                "| |  / /  _/ ____/_  __/ __ \\/ __ \\ \\/ /\r\n" +
                "| | / // // /     / / / / / / /_/ /\\  / \r\n" +
                "| |/ // // /___  / / / /_/ / _, _/ / /  \r\n" +
                "|___/___/\\____/ /_/  \\____/_/ |_| /_/   \r\n" +
                "                                        ";

            buttons = new string[] { "Exit" };
            selectedButton = 1;


        }

        public override void PressButton()
        {
            switch (selectedButton)
            {
                case 1:
                    Application.CloseApp();
                    break;

            }
        }

    }
}
