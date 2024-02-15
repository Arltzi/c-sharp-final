using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class LevelCompleteMenu : Menu
    {
        public LevelCompleteMenu()
        {
            text = "Congratulations! You have completed the level.";

            buttons = new string[] { "Continue", "Merchant", "Exit" };
            selectedButton = 1;
        }
        override public void PressButton()
        {
            switch (selectedButton)
            {
                case 1:
                    Application.GoNextLevel();
                    break;
                case 2:
                    Application.SwapMenu(MenuType.MERCHANT);
                    Console.Clear();
                    break;
                case 3:
                    Application.CloseApp();
                    break;

            }
        }
    }
}
