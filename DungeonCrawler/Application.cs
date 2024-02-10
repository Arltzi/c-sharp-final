using DungeonCrawler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        //private string roomTop =    "####################################################################\n" +
        //                            "##                                                                ##\n" +
        //                            "# #                                                              # #\n" +
        //                            "#  #                                                            #  #\n" +
        //                            "#   #                                                          #   #\n" +
        //                            "#    ##########################################################    #\n";

        //private string roomSide =   "#    #";

        //private string roomBottom = "#    #####################                #####################    #\n" +
        //                            "#   #                    #                #                    #   #\n" +
        //                            "#  #                    ##                ##                    #  #\n" +
        //                            "# #                    # #                # #                    # #\n" +
        //                            "##                    #  #                #  #                    ##\n" +
        //                            "####################################################################";

        //private string roomTop =    "████████████████████████████████████████████████████████████████████\n" +
        //                            "██                                                                ██\n" +
        //                            "█ █                                                              █ █\n" +
        //                            "█  █                                                            █  █\n" +
        //                            "█   █                                                          █   █\n" +
        //                            "█    ██████████████████████████████████████████████████████████    █\n";

        //private string roomSide =   "█    █";

        //private string roomBottom = "█    █████████████████████                █████████████████████    █\n" +
        //                            "█   █                    █                █                    █   █\n" +
        //                            "█  █                    ██                ██                    █  █\n" +
        //                            "█ █                    █ █                █ █                    █ █\n" +
        //                            "██                    █  █                █  █                    ██\n" +
        //                            "████████████████████████████████████████████████████████████████████";

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


        static private bool m_isRunning;

        static private bool m_Paused;

        static public int windowX = 56;
        static public int windowY = 20;

        private InputMap inputMap;

        private Thread inputThread;

        static private int m_TickTime;

        Player p;

        MainMenu mainMenu;

        private int tickCount = 0;


        static public void CloseApp()
        {
            m_isRunning = false;
        }

        static public void UnPause()
        {
            m_Paused = false;
            m_TickTime = 300;
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

            p = new Player(windowX / 2, windowY / 2);

            mainMenu = new MainMenu();

            inputMap = new InputMap();
            inputMap = InputMap.UP;

        }

        public void Run()
        {
            inputThread.Start();
            GameLoop();
        }

        private void GameLoop()
        {

            while (m_isRunning)
            {

                if (m_Paused)
                {
                    mainMenu.Update(inputMap);
                    mainMenu.Render();
                }
                else
                {
                    Update();
                    TestRender();
                }

                inputMap = InputMap.NONE;

                System.Threading.Thread.Sleep(m_TickTime);

                Console.Clear();

                tickCount++;

            }

        }


        private void Update()
        {

            if (inputMap == InputMap.PAUSE)
            {
                Pause();
            }

            p.Move(inputMap);

        }

        private void TestRender()
        {

            Console.Write(roomTop);

            for (int i = 0; i < windowY; i++)
            {

                Console.Write(roomSide);

                for (int j = 0; j < windowX; j++)
                {


                    if (i == p.y && j == p.x)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(p.sprite);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(' ');
                    }

                }

                Console.WriteLine(roomSide);


            }

            Console.WriteLine(roomBottom);

            Console.WriteLine($"\nPlayer X: {p.x}, Y: {p.y}");
            Console.WriteLine($"Tick time: {m_TickTime}");
            Console.WriteLine($"Tick count:{tickCount}");
            Console.WriteLine($"Input: {inputMap}");


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
