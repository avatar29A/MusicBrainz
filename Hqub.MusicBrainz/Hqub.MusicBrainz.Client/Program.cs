namespace Hqub.MusicBrainz.Client
{
    using Hqub.MusicBrainz.API;
    using System;
    using System.IO;
    using System.Net;
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
                    if (item.InnerException == null)
                    {
                        Console.WriteLine(item.Message);
                    }
                    else
                    {
                        // Display inner exception.
                        Console.WriteLine(item.InnerException.Message);
                    }
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
            // Make sure that TLS 1.2 is available.
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;

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
