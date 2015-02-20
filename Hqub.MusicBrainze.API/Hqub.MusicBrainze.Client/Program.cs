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
            ShowTracksByAlbum("Король и Шут", "Акустический альбом");
//            ShowAlbumsTracksByArtist("Король и Шут");

            Console.ReadKey();
        }

        private static Artist GetArtist(string name)
        {
            var artists = Artist.Search(name);

            var artist = artists.First();
            Console.WriteLine(artist.Name);

            return artist;
        }

        private static async void ShowTracksByAlbum(string artistName, string albumName)
        {
            var artist = GetArtist(artistName);

            var albums = await Release.BrowseAsync("artist", artist.Id);


            var album = albums.FirstOrDefault(r => r.Title.ToLower() == albumName.ToLower());

            if (album == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Album not found.\n");
                Console.ResetColor();

                Console.WriteLine("Results:\n");
                foreach (var al in albums)
                {
                    Console.WriteLine("\t{0}", al.Title);
                }

                return;
            }

            Console.WriteLine("\t{0}", albumName);

            var tracks = await Recording.BrowseAsync("release", album.Id, 100);
            foreach (var track in tracks)
            {
                Console.WriteLine("\t\t{0}", track.Title);
            }
        }

        private static async void ShowAlbumsTracksByArtist(string name)
        {
            var artist = GetArtist(name);

            var releases = await Release.BrowseAsync("artist", artist.Id, 100, 0, "media");

            foreach (var release in releases)
            {
                Console.WriteLine("\t{0}", release.Title);
                var tracks = await Recording.BrowseAsync("release", release.Id, 100);

                foreach (var track in tracks)
                {
                    Console.WriteLine("\t\t{0}", track.Title);
                }
            }
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
