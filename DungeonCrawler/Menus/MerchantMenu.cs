using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class MerchantMenu : Menu
    {
        public MerchantMenu()
        {
            text = "Merchant page";

            buttons = new string[] { "btn1", "btn2", "Back" };
            selectedButton = 1;
        }
        override public void PressButton()
        {
            switch (selectedButton)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    Application.SwapMenu(MenuType.LVLCOMPLETE);
                    break;

            }
        }
    }
}
