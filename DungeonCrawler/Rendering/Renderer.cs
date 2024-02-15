using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

// TODO Implement class
namespace DungeonCrawler.Rendering
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

        // Export char array is passed to GameRenderer
        private RenderTile[,] export;
        public Renderer()
        {



        }

        // Draw map
        public void Draw(Map map)
        {
            // Setup render tiles
            export = new RenderTile[ map.Data.GetLength(0) + 68, map.Data.GetLength(1)+12 ];
            for (int y = 0; y < export.GetLength(1); y++) 
            {
                for (int x = 0; x < export.GetLength(0); x++)
                {
                    export[x, y] = new RenderTile();
                }
            }
            

            //Console.ResetColor();

            Console.Write(roomTop);

            for (int i = 0; i < Map.mapHeight; i++)
            {
              
                DrawRoomSide(0, i);
                // bool thereWasEffectColor = false;     old unnecessary counter variable

                for (int j = 0; j < Map.mapWidth; j++)
                {

                    Tile currentTile = map.Data[j, i];

                    //This line of code slows execution down by like 100%
                    //Console.BackgroundColor = currentTile.effectColour;

                    /*
                    if (currentTile.effectColour != ConsoleColor.Black)
                    {
                        Console.BackgroundColor = currentTile.effectColour;
                        thereWasEffectColor = true;
                    }*/

                    if (currentTile.Occupant == null)
                    {
                        //Console.Write(' ');
                        export[j + 8, i /* +6 */].sprite = ' ';
                    }
                    else
                    {
                        //Console.ForegroundColor = currentTile.Occupant.SpriteColour;
                        //Console.Write(currentTile.Occupant.sprite);
                        export[j + 8, i/* + 6*/].sprite = currentTile.Occupant.sprite;
                    }

                    /*
                    if (thereWasEffectColor)
                    {
                        thereWasEffectColor = false;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    */

                }

                //Console.ResetColor();

                //Console.WriteLine(roomSide);
                DrawRoomSide(Map.mapWidth , i);
            }

            //Console.WriteLine(roomBottom);

            GameRenderer.WriteToBuffer(export);

            /*
            if (Application.DEBUG)
            {
                Console.WriteLine($"\nPlayer X: {Application.player.x}, Y: {Application.player.y}    ");
                Console.WriteLine($"Current map: {Application.CurrentMap.Name}");
                Console.WriteLine($"Tick time: {Application.TickTime}     ");
                Console.WriteLine($"Tick count:{Application.tickCount}    ");
                //Console.WriteLine($"Input: {inputMap}   ");
            }
            */

        }


        // Draws room side
        void DrawRoomSide (int x, int y)
        {
            export[x, y].sprite = '▓';

            for (int iter = 0; iter < 4; iter++) 
            {
                x += iter;
                export[x, y].sprite = ' ';
            }
            export[x, y].sprite = '▓';

        }


        // Draw menu
        public void Draw(Menu menu)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(menu.logo);

            Console.ResetColor();

            for (int i = 0; i < menu.buttons.Length; i++)
            {
                if (i == menu.selectedButton - 1)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(">>>");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(menu.buttons[i]);
                }
                else
                {
                    Console.WriteLine("   " + menu.buttons[i]);
                }

                Console.ResetColor();
            }

            // debug
            //Console.WriteLine($"{menu.selectedButton}");

        }

    }
}





/*
 This is the old game drawing method,
 I'm just storing it here in case I break it


 public void Draw(Map map)
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
                    //Console.BackgroundColor = currentTile.effectColour;

                    if (currentTile.effectColour != ConsoleColor.Black)
                    {
                        Console.BackgroundColor = currentTile.effectColour;
                        thereWasEffectColor = true;
                    }

                    if (currentTile.Occupant == null)
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
 
 */