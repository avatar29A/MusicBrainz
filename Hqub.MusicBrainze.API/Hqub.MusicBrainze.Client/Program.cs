using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hqub.MusicBrainz.API.Entities;

namespace Hqub.MusicBrainz.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Test();

            Console.WriteLine("OK");
            Console.ReadKey();
        }

        private static async void Test()
        {
            var artists = Artist.Search("The Scorpions");

            var artist = artists.First();

            Console.WriteLine(artist.Name);

            var releases = await Recording.BrowseAsync("artist", artist.Id, 40);
            

            foreach (var release in releases)
            {
                Console.WriteLine("{0}", release.Title);
            }
//
        }
    }
}
