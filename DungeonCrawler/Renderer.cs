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

        public void Render()
        {
            Console.ResetColor();

            Console.Write(roomTop);

            for (int i = 0; i < Map.mapSizeY; i++)
            {

                Console.Write(roomSide);

                for (int j = 0; j < Map.mapSizeX; j++)
                {
                    Console.ResetColor();

                    Tile currentTile = Application.CurrentMap.Data[j, i];

                    //This line of code slows execution down by like 100%
                    //Console.BackgroundColor = currentTile.effectColour;

                    if(currentTile.Occupant == null)
                    {
                        Console.Write(' ');
                    }
                    else
                    {
                        Console.ForegroundColor = currentTile.Occupant.SpriteColour;
                        Console.Write(currentTile.Occupant.sprite);
                    }

                }

                Console.WriteLine(roomSide);
            }

            Console.WriteLine(roomBottom);
            if (Application.DEBUG)
            {
                Console.WriteLine($"\nPlayer X: {Application.player.x}, Y: {Application.player.y}    ");
                Console.WriteLine($"Current map: {Application.CurrentMap.Name}");
                Console.WriteLine($"Tick time: {Application.TickTime}     ");
                Console.WriteLine($"Tick count:{Application.tickCount}    ");
                //Console.WriteLine($"Input: {inputMap}   ");
            }

        }

    }
}
