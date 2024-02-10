using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonCrawler
{
    internal class MainMenu
    {
        private string logo = "       __                                 \r\n" +
            "  ____/ /_  ______  ____ ____  ____  ____ \r\n" +
            " / __  / / / / __ \\/ __ `/ _ \\/ __ \\/ __ \\\r\n" +
            "/ /_/ / /_/ / / / / /_/ /  __/ /_/ / / / /\r\n" +
            "\\__,_/\\__,_/_/ /_/\\__, /\\___/\\____/_/ /_/ \r\n" +
            "  ______________ /____/  __/ /__  _____   \r\n" +
            " / ___/ ___/ __ `/ | /| / / / _ \\/ ___/   \r\n" +
            "/ /__/ /  / /_/ /| |/ |/ / /  __/ /       \r\n" +
            "\\___/_/   \\__,_/ |__/|__/_/\\___/_/        \r\n" +
            "                                          ";

        private const int buttonCount = 3;

        private string[] buttons = new string[buttonCount] { "Play", "About", "Exit" };
        private int selectedButton = 1;

        public void Render()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(logo);

            Console.ResetColor();

            for (int i = 0; i < buttonCount; i++)
            {
                if (i == selectedButton - 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(">>>");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(buttons[i]);
                }
                else
                {
                    Console.WriteLine("   " + buttons[i]);
                }

                Console.ResetColor();
            }

        }

        public void Update(InputMap map)
        {
            switch (map)
            {
                case InputMap.UP:
                    if (selectedButton > 1)
                        selectedButton--;
                    break;
                case InputMap.DOWN:
                    if (selectedButton < buttonCount)
                        selectedButton++;
                    break;
                case InputMap.SHOOT:
                    switch (selectedButton)
                    {
                        case 1:
                            Application.UnPause();
                            break;
                        case 2:
                            break;
                        case 3:
                            Application.CloseApp();
                            break;

                    }
                    break;
            }


        }

    }
}
