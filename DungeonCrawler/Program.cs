namespace DungeonCrawler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please fullscreen command line.");
            Console.WriteLine("Press enter when ready to start...");
            Console.ReadLine();

            Console.Clear();

            Application app = new Application();
            app.Run();

            Console.Write("Thanks for playing!\n\n");
        }
    }
}
