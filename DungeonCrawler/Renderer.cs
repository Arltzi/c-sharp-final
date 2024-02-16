using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

// TODO Implement class
namespace DungeonCrawler
{
    internal class Renderer
    {
        private string roomTop = "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓\n" +
                                 "▓▓                                                                ▓▓\n" +
                                 "▓ ▓                                                              ▓ ▓\n" +
                                 "▓  ▓                                                            ▓  ▓\n" +
                                 "▓   ▓                                                          ▓   ▓\n" +
                                 "▓    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    ▓\n";

        private string roomSide = "▓    ▓";

        private string roomBottom = "▓    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    ▓\n" +
                                    "▓   ▓                    ▓                ▓                    ▓   ▓\n" +
                                    "▓  ▓                    ▓▓                ▓▓                    ▓  ▓\n" +
                                    "▓ ▓                    ▓ ▓                ▓ ▓                    ▓ ▓\n" +
                                    "▓▓                    ▓  ▓                ▓  ▓                    ▓▓\n" +
                                    "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓";

        public Renderer()
        {



        }

        // Draw map
        public void OldDraw(Map map)
        {
            Console.ResetColor();

            Console.Write(roomTop);

            for (int i = 0; i < Map.mapHeight; i++)
            {
                Console.ResetColor();

                Console.Write(roomSide);
                bool thereWasEffectColor = false;

                for (int j = 0; j < Map.mapWidth; j++)
                {

                    Tile currentTile = map.Data[j, i];

                    //This line of code slows execution down by like 100%
                    Console.BackgroundColor = currentTile.effectColour;

                    

                    if(currentTile.Occupant == null)
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.ForegroundColor = currentTile.Occupant.SpriteColour;
                        Console.Write(currentTile.Occupant.sprite);
                    }

                    if (thereWasEffectColor)
                    {
                        thereWasEffectColor = false;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                }

                Console.ResetColor();

                Console.WriteLine(roomSide);
            }

            Console.WriteLine(roomBottom);

            Console.Write("\nHealth:");
            for(int i = 0; i < Application.player.MaxHealth; i++)
            {
                if(i < Application.player.Health)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write('▓');
            }

            if (Application.DEBUG)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\n\n\nPlayer X: {Application.player.x}, Y: {Application.player.y}    ");
                Console.WriteLine($"Health : {Application.player.Health}    ");
                Console.WriteLine($"Current map: {Application.CurrentMap.Name}");
                Console.WriteLine($"Tick time: {Application.TickTime}     ");
                Console.WriteLine($"Tick count:{Application.tickCount}    ");
                //Console.WriteLine($"Input: {inputMap}   ");
            }

        }

        // Draw menu
        public void Draw(Menu menu)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(menu.text);

            Console.ResetColor();

            for (int i = 0; i < menu.buttons.Length; i++)
            {
                if (i == menu.selectedButton - 1)
                {
                    Console.Write("\t    ");
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(">>>");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(menu.buttons[i]);
                }
                else
                {
                    Console.WriteLine("\t       " + menu.buttons[i]);
                }

                Console.ResetColor();
            }

            // debug
            //Console.WriteLine($"{menu.selectedButton}");

        }

    }
}
