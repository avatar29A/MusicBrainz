namespace Hqub.MusicBrainz.Client
{
    using Hqub.MusicBrainz.API;
    using System;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var task = RunExamples();

                task.Wait();
            }
            catch (AggregateException e)
            {
                foreach (var item in e.Flatten().InnerExceptions)
                {
                    Console.WriteLine(item.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.ReadKey();
        }

        private static async Task RunExamples()
        {
            var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Use local file cache (subdirectory of executing program).
            Configuration.Cache = new FileRequestCache(Path.Combine(location, "cache"));

            Header("Example 1");
            await Example1.Run();

            Header("Example 2");
            await Example2.Run();

            Header("Example 3");
            await Example3.Run();

            Header("Example 4");
            await Example4.Run();

            Header("Example 5");
            await Example5.Run();
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
