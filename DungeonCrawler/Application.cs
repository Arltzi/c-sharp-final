using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

enum InputMap
{

    NONE = 0,
    UP = 1,
    DOWN = 2,
    RIGHT = 3,
    LEFT = 4,
    SHOOT = 5,
    PAUSE = 6

}

enum Tile
{
    EMPTY = 0,
    WALL = 1,
    PLAYER = 2,
    ENEMY = 3
}

namespace DungeonCrawler
{
    internal class Application
    {

        private string roomTop =    "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓\n" +
                                    "▓▓                                                                ▓▓\n" +
                                    "▓ ▓                                                              ▓ ▓\n" +
                                    "▓  ▓                                                            ▓  ▓\n" +
                                    "▓   ▓                                                          ▓   ▓\n" +
                                    "▓    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    ▓\n";

        private string roomSide =   "▓    ▓";

        private string roomBottom = "▓    ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓                ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓    ▓\n" +
                                    "▓   ▓                    ▓                ▓                    ▓   ▓\n" +
                                    "▓  ▓                    ▓▓                ▓▓                    ▓  ▓\n" +
                                    "▓ ▓                    ▓ ▓                ▓ ▓                    ▓ ▓\n" +
                                    "▓▓                    ▓  ▓                ▓  ▓                    ▓▓\n" +
                                    "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓";


        static private bool m_isRunning;

        static private bool m_Paused;

        static public int mapX = 56;
        static public int mapY = 20;

        private InputMap inputMap;

        private Thread inputThread;

        static private int m_TickTime;
        
        Player player;

        MainMenu mainMenu;

        private int tickCount = 0;

        static private Map currentMap = new Map();

        private bool DEBUG = true;

        private static char[] TileSheet = { ' ', '#' };

        public static Map CurrentMap
        {
            get { return currentMap; }
            private set { currentMap = value; }
        }


        static public void CloseApp()
        {
            m_isRunning = false;
        }

        static public void UnPause()
        {
            m_Paused = false;
            m_TickTime = 50;
        }

        static public void Pause()
        {
            m_Paused = true;
            m_TickTime = 250;
        }

        public Application()
        {
            Console.CursorVisible = false;

            inputThread = new Thread(HandleInput);

            m_TickTime = 250;

            m_isRunning = true;
            m_Paused = true;

            player = new Player(mapX / 2, mapY / 2);

            mainMenu = new MainMenu();

            inputMap = new InputMap();
            inputMap = InputMap.NONE;

            currentMap = new Map();

        }

        public void Run()
        {
            // Start input thread
            inputThread.Start();

            // Check if game succesfully initialized
            if (Init() == true)
            {
                GameLoop();
            }

            Console.Clear();
        }

        private bool Init()
        {
            // Map succesfully loaded
            if (currentMap.Load("map_02") == true)
            {
                player.x = currentMap.PlayerX;
                player.y = currentMap.PlayerY;

                return true;

            }

            return false;
        }

        private void GameLoop()
        {

            while (m_isRunning)
            {
                // TODO: Tidy up and implement "render contexts" for more cleanly implemented system for swapping between gameplay / menu
                if (m_Paused)
                {
                    mainMenu.Update(inputMap);
                    mainMenu.Render();
                }
                else
                {
                    Update();
                    Render();
                }



                System.Threading.Thread.Sleep(m_TickTime);

                Console.SetCursorPosition(0,0);

                tickCount++;

            }

        }

        private void Update()
        {

            if (inputMap == InputMap.PAUSE)
            {
                Pause();
            }

            if (inputMap >= InputMap.UP || inputMap <= InputMap.LEFT) // Check if input is a move input
            {

                if (player.Move(inputMap) == true) // If move was successful, update location in map
                {
                    currentMap.UpdatePlayerLocation(player);
                }

                inputMap = InputMap.NONE;
            }

        }

        private void Render()
        {

            Console.Write(roomTop);

            for (int i = 0; i < mapY; i++)
            {

                Console.Write(roomSide);

                for (int j = 0; j < mapX; j++)
                {

                    Tile currentTile = (currentMap.Data[j, i]);

                    switch (currentTile)
                    {

                        case Tile.EMPTY:
                            Console.Write(' '); 
                            break;
                        case Tile.WALL:
                            Console.Write('#');
                            break;
                        case Tile.PLAYER:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(player.sprite);
                            Console.ResetColor();
                            break;

                    }
                    
                }

                Console.WriteLine(roomSide);
            }

            Console.WriteLine(roomBottom);

            if (DEBUG)
            {
                Console.WriteLine($"\nPlayer X: {player.x}, Y: {player.y}    ");
                Console.WriteLine($"Current map: {currentMap.Name}");
                Console.WriteLine($"Tick time: {m_TickTime}");
                Console.WriteLine($"Tick count:{tickCount}");
                Console.WriteLine($"Input: {inputMap}   ");
            }

        }
        

        // Input function handled on independant thread
        private void HandleInput()
        {

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.W:
                            inputMap = InputMap.UP;
                            break;
                        case ConsoleKey.S:
                            inputMap = InputMap.DOWN;
                            break;
                        case ConsoleKey.D:
                            inputMap = InputMap.RIGHT;
                            break;
                        case ConsoleKey.A:
                            inputMap = InputMap.LEFT;
                            break;
                        case ConsoleKey.Enter:
                            inputMap = InputMap.SHOOT;
                            break;
                        case ConsoleKey.Escape:
                            inputMap = InputMap.PAUSE;
                            break;
                        case ConsoleKey.X:
                            m_isRunning = false;
                            return;

                    }

                }

                if (m_isRunning == false)
                    return;

                // Small sleep to avoid maxing out thread
                System.Threading.Thread.Sleep(10);
            }

        }


    }
}
