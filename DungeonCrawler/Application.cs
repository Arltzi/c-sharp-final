using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DungeonCrawler.Attacks;

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

namespace DungeonCrawler
{
    internal class Application
    {

        static private bool m_isRunning;

        static private bool m_Paused;

        static public int mapX = 56;
        static public int mapY = 20;

        private InputMap inputMap;

        private Thread inputThread;

        static private int m_TickTime;

        static public Player player;

        MainMenu mainMenu;

        static public int tickCount = 0;

        static private Map currentMap = new Map();

        static public bool DEBUG = true;

        static public EntityManager entityManager = new EntityManager();

        private Renderer m_Renderer = new Renderer();

        public static int TickTime
        {
            get { return m_TickTime; }
            private set { m_TickTime = value; }
        }

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
            m_TickTime = 1;
        }

        static public void Pause()
        {
            Console.Clear();
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

            player = new Player();

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
            if (currentMap.Load("map_01") == true)
            {
                return true;
            }

            return false;
        }
        bool started = false;
        private void GameLoop()
        {
            while (m_isRunning)
            {
                started = true;

                // TODO: Tidy up and implement "render contexts" for more cleanly implemented system for swapping between gameplay / menu
                if (m_Paused)
                {
                    mainMenu.Update(inputMap);
                    mainMenu.Render();
                }
                else
                {
                    Update();
                    m_Renderer.Render();

                }

                System.Threading.Thread.Sleep(m_TickTime);

                Console.SetCursorPosition(0,0);

                tickCount++;

            }

        }

        private void Update()
        {
            // Checks attack sto clean
            AttackAutoClear.staticRef.CheckAttacksToClean();

            if (inputMap == InputMap.PAUSE)
            {
                Pause();
            }

            if (inputMap >= InputMap.UP || inputMap <= InputMap.LEFT) // Check if input is a move input
            {

                if (player.Move(inputMap) == true) // If move was successful, update location in map
                {
                    //currentMap.UpdatePlayerLocation(player);
                }

                inputMap = InputMap.NONE;
            }

        }


        // Input function handled on independant thread
        private void HandleInput()
        {
            if (started) { return; }
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
