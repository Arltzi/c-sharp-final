﻿namespace DungeonCrawler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Levels.BuildLevels();
            Application app = new Application();
            app.Run();

            Console.Write("Thanks for playing!\n\n");
        }
    }
}
