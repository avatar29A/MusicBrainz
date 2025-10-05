namespace Hqub.MusicBrainz.Client
{
    using Hqub.MusicBrainz;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                await RunExamples();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.ReadKey();
        }

        private static async Task RunExamples()
        {
            // Get path for local file cache.
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var client = new MusicBrainzClient()
            {
                Cache = new FileRequestCache(Path.Combine(location, "cache"))
            };

            Header("Example 1");
            await Example1.Run(client);

            Header("Example 2");
            await Example2.Run(client);

            Header("Example 3");
            await Example3.Run(client);

            Header("Example 4");
            await Example4.Run(client);

            Header("Example 5");
            await Example5.Run(client);

            Header("Example 6");
            await Example6.Run(client);

            Header("Example 7");
            await Example7.Run(client);
        }

        private static void Header(string title)
        {
            var color = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine();
            Console.WriteLine(title);
            Console.WriteLine();

            Console.ForegroundColor = color;
        }
    }
}
