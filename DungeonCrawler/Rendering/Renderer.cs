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
        bool useOldDraw = false;
        public void Draw(Map map)
        {
            if (useOldDraw)
            {
                OldDraw(map);
                return;
            }

            // Setup render tiles
            export = new RenderTile[map.Data.GetLength(0) + 68, map.Data.GetLength(1) + 12];
            for (int y = 0; y < export.GetLength(1); y++)
            {
                for (int x = 0; x < export.GetLength(0); x++)
                {
                    export[x, y] = new RenderTile();
                }
            }

            // draws top and bottom of room
            DrawRoomSection(0, roomTop);
            DrawRoomSection(Map.mapHeight + 6, roomBottom);
            for (int i = 0; i < Map.mapHeight; i++)
            {

                // draws side of room
                DrawRoomSide(0, i + 6);


                // parses map
                for (int j = 0; j < Map.mapWidth; j++)
                {
                    // declarations
                    Tile currentTile = map.Data[j, i];

                    // Checks if ther is an occupant
                    if (currentTile.Occupant == null)
                    {
                        // draws an empty spot
                        export[j + 8, i + 6].sprite = ' ';
                        export[j + 8, i + 6].color = 4;
                        export[j + 4, i + 6].effectColor = currentTile.newEffectColor;
                    }
                    else
                    {
                        // draws the occupant
                        export[j + 8, i + 6].sprite = currentTile.Occupant.sprite;
                        export[j + 8, i + 6].color = currentTile.Occupant.color;
                        export[j + 8, i + 6].effectColor = currentTile.newEffectColor;
                    }


                }

                DrawRoomSide(Map.mapWidth + 2, i + 6);
            }


            GameRenderer.WriteToBuffer(export);

            /*
            if (Application.DEBUG)
            {
                Console.WriteLine($"\nPlayer X: {Application.player.x}, Y: {Application.player.y}    ");
                Console.WriteLine($"Health : {Application.player.Health}    ");
                Console.WriteLine($"Current map: {Application.CurrentMap.Name}");
                Console.WriteLine($"Tick time: {Application.TickTime}     ");
                Console.WriteLine($"Tick count:{Application.tickCount}    ");
                //Console.WriteLine($"Input: {inputMap}   ");
            }
            */

        }


        void DrawRoomSection(int height, string toPrint)
        {
            // declarations
            char currentCharater;
            int y = height;
            int x = 0;

            // draw the room section provided
            for (int iterator = 0; iterator < toPrint.Length; iterator++)
            {
                currentCharater = toPrint[iterator];

                if (currentCharater == '\n')
                {
                    y++;
                    x = 0;
                    continue;
                }
                x++;
                export[x, y] = new RenderTile(currentCharater);
                export[x, y].color = 15;
            }
        }



        // Draws room side
        void DrawRoomSide(int x, int y)
        {
            // draw the border
            export[x + 1, y].sprite = '#';
            export[x + 1, y].color = 15;

            // draws the spaces
            for (int iter = 0; iter < 4; iter++)
            {
                x += iter;
                export[x, y].sprite = ' ';
            }

            // draw the border
            export[x + 1, y].sprite = '▓';
            export[x + 1, y].color = 15;
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
        }


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

            Console.WriteLine(roomBottom);

            Console.Write("\nHealth:");
            for (int i = 0; i < Application.player.MaxHealth; i++)
            {
                if (i < Application.player.Health)
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