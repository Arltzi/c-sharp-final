using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    internal class Menu
    {
        public string text = string.Empty;

        // Put buttons here
        public string[] buttons = { };

        public int selectedButton = 0;

        public Menu()
        {

        }

        public void SelectUp()
        {
            if (selectedButton > 1)
                selectedButton--;
        }

        public void SelectDown()
        {
            if (selectedButton < buttons.Length)
                selectedButton++;
        }
        virtual public void PressButton()
        {
            // IMPLEMENT BUTTONS
        }


    }
}
