using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DungeonCrawler.Attacks;
using DungeonCrawler.Rendering;

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

// Enum for different menus
enum MenuType
{
    MAIN = 0,
    ABOUT = 1,
}

namespace DungeonCrawler
{
    internal class Application
    {
        // Application context for rendering / input
        // Either rendering / polling for gameplay or menu
        enum AppContext
        {
            GAME = 0,
            MENU = 1
        }

        static private bool m_isRunning;

        static private bool m_Paused;

        static public int mapX = 56;
        static public int mapY = 20;

        public static InputMap inputMap;

        private Thread inputThread;

        static private int m_TickTime;
        
        static public Player player;

        static public int tickCount = 0;

        static private Map currentMap = new Map();

        static public EntityManager entityManager = new EntityManager();

        private Renderer m_Renderer = new Renderer();

        // Currently selected menu class
        static private Menu m_currentMenu;
        // List of all menus in game
        static private Menu[] MenuList = new Menu[2];

        private AppContext context = AppContext.MENU;

        static public bool DEBUG = true;


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

        // Swapping selected menu based off enum
        static public void SwapMenu(MenuType newMenu)
        {
            m_currentMenu = MenuList[(int)newMenu];
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
            m_TickTime = 100;
        }

        public Application()
        {
            Console.CursorVisible = false;

            inputThread = new Thread(HandleInput);

            m_TickTime = 100;

            m_isRunning = true;
            m_Paused = true;

            player = new Player();

            inputMap = new InputMap();
            inputMap = InputMap.NONE;

            currentMap = new Map();

        }

        void HandleInput() 
        {
            while (true)
            {
                InputSystem.instance.GetKeyboardInput();
                System.Threading.Thread.Sleep(10);
            }
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
            // Initializing menu list
            MenuList[0] = new MainMenu();
            MenuList[1] = new AboutMenu();

            // Swapping to main menu for start of game
            SwapMenu(MenuType.MAIN);

            // Map succesfully loaded
            if (currentMap.Load("map_00") == true)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Failed to load map!");
            }

            return false;
        }
        bool started = false;
        private void GameLoop()
        {
            while (m_isRunning)
            {
                //InputSystem.instance.GetKeyboardInput();
                started = true;

                // TODO: Tidy up and implement "render contexts" for more cleanly implemented system for swapping between gameplay / menu
                if (m_Paused)
                    context = AppContext.MENU;
                else
                    context = AppContext.GAME;

                Update();
                Render();

                System.Threading.Thread.Sleep(m_TickTime);

                Console.SetCursorPosition(0,0);

                tickCount++;

            }

        }

        private void Update()
        {
            
            if (context == AppContext.GAME) // Tick handling for GAME
              
            // Checks attack sto clean
            AttackAutoClear.staticRef.CheckAttacksToClean();

            if (inputMap == InputMap.PAUSE)

            {

                if (inputMap == InputMap.PAUSE)
                {
                    Pause();
                }

                if (inputMap >= InputMap.UP || inputMap <= InputMap.LEFT) // Check if input is a move input
                {

                    player.Move(inputMap);

                    inputMap = InputMap.NONE;
                }

            }
            else if (context == AppContext.MENU) // Tick handling for MENU
            {

                switch (inputMap)
                {
                    case InputMap.UP:
                        m_currentMenu.SelectUp();
                        break;
                    case InputMap.DOWN:
                        m_currentMenu.SelectDown();
                        break;
                    case InputMap.SHOOT:
                        m_currentMenu.PressButton();
                        break;
                }
                //mainMenu.Update(inputMap);
                inputMap = InputMap.NONE;

            }

            else 
            {
                player.Move(inputMap);
            }

        }


        private void Render()
        {

            if (context == AppContext.GAME) // Game rendering
            {
                m_Renderer.Draw(CurrentMap);
            }
            else if (context == AppContext.MENU) // Menu rendering
            {
                m_Renderer.Draw(m_currentMenu);
            }

        }

        // Input function handled on independant thread
        
        private void OldHandleInput()
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
