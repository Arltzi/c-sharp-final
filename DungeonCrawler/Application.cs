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
    ATTACK = 5,
    PAUSE = 6,
    DEVBTN = 7,
}

// Enum for different menus
enum MenuType
{
    MAIN = 0,
    ABOUT = 1,
    LVLCOMPLETE = 2,
    MERCHANT = 3,
    DEATH = 4
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
        static private Menu[] MenuList = new Menu[5];

        private AppContext context = AppContext.MENU;

        static private int levelNum = 0;

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
            m_TickTime = 20;
        }

        static public void Pause()
        {
            Console.Clear();
            m_Paused = true;
            m_TickTime = 200;
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

        static public void ReplayLevel()
        {
            entityManager.entityList.Clear();
            currentMap.Load(currentMap.Name);
            player.Heal(player.MaxHealth);
            UnPause();
        }

        static public void GoNextLevel()
        {
            entityManager.entityList.Clear();
            levelNum++;
            currentMap.Load("map_" + levelNum);
            player.Heal(player.MaxHealth);
            UnPause();
        }

        private bool Init()
        {
            // Initializing menu list
            MenuList[0] = new MainMenu();
            MenuList[1] = new AboutMenu();
            MenuList[2] = new LevelCompleteMenu();
            MenuList[3] = new MerchantMenu();
            MenuList[4] = new DeathMenu();

            // Swapping to main menu for start of game
            SwapMenu(MenuType.MAIN);

            // Map succesfully loaded
            if (currentMap.Load("map_" + levelNum.ToString()) == true)
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
                started = true;

                Update();

                Render();

                System.Threading.Thread.Sleep(m_TickTime);

                tickCount++;

                Console.SetCursorPosition(0, 0);

            }

        }

        private void Update()
        {

            // Checks attack sto clean
            //AttackAutoClear.staticRef.CheckAttackOverlap();
            AttackAutoClear.staticRef.CheckAttacksToClean();

            if (context == AppContext.GAME) // Tick handling for game
            {
                // entityManager.EnemyUpdate();

                // Player invincibility frame tick
                player.TickIFrame();

                // Death check
                if(player.Health <= 0)
                {
                    Pause();
                    SwapMenu(MenuType.DEATH);

                }

                // LEVEL CLEAR CHECK
                if(entityManager.entityList.Count == 0)
                {
                    // ALL ENEMIES DEAD
                    Pause();
                    SwapMenu(MenuType.LVLCOMPLETE);
                    Thread.Sleep(100);
                }


                // PLAYER TAKE DMG CHECK
                if(player.IsNextToEnemy() == true)
                {
                    player.TakeDamage();
                }


                // PLAYER INPUT
                if (inputMap == InputMap.PAUSE)
                {
                    Pause();
                }
                else if (inputMap >= InputMap.UP && inputMap <= InputMap.LEFT) // Check if input is a move input
                {
                    player.Move(inputMap);
                }
                else if(inputMap == InputMap.DEVBTN)
                {

                    Enemy e = (Enemy)entityManager.entityList[0];
                    e.Die();

                }
                else if(inputMap == InputMap.ATTACK)
                {

                    player.Attack();

                }

                inputMap = InputMap.NONE;

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
                    case InputMap.ATTACK:
                        m_currentMenu.PressButton();
                        break;
                }

                inputMap = InputMap.NONE;

            }

            if (m_Paused)
                context = AppContext.MENU;
            else
                context = AppContext.GAME;

        }


        private void Render()
        {

            if (context == AppContext.GAME) // Game rendering
            {
                m_Renderer.OldDraw(CurrentMap);
            }
            else if (context == AppContext.MENU) // Menu rendering
            {
                m_Renderer.Draw(m_currentMenu);
            }

        }

        // Input function handled on independant thread
        private void HandleInput()
        {
            //if (started) { return; }

            //while (true)
            //{
<<<<<<< HEAD
                //InputSystem.instance.GetKeyboardInput();
=======
            //    InputSystem.instance.GetKeyboardInput();
>>>>>>> 34771bfc706ef9b9044d4360fea0beb0a8278d55
            //}


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
                            inputMap = InputMap.ATTACK;
                            break;
                        case ConsoleKey.Escape:
                            inputMap = InputMap.PAUSE;
                            break;
                        case ConsoleKey.E:
                            inputMap = InputMap.DEVBTN;
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
