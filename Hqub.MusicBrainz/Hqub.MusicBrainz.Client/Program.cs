using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Hqub.MusicBrainz.API;
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

        private static async Task<Artist> GetArtist(string name)
        {
            var artists = await Artist.SearchAsync(name);

            var artist = artists.Items.First();
            Console.WriteLine(artist.Name);

            return artist;
        }

        private static async void ShowTracksByAlbum(string artistName, string albumName)
        {
            Configuration.UserAgent = "Chrome/41.0.2228.0";

            var artist = await GetArtist(artistName);

            var albums = await Release.BrowseAsync("artist", artist.Id);


            var album = albums.Items.FirstOrDefault(r => r.Title.ToLower() == albumName.ToLower());

            if (album == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Album not found.\n");
                Console.ResetColor();

                Console.WriteLine("Results:\n");
                foreach (var al in albums.Items)
                {
                    Console.WriteLine("\t{0}", al.Title);
                }

                return;
            }

            Console.WriteLine("\t{0}", albumName);

            var tracks = await Recording.BrowseAsync("release", album.Id, 100);
            foreach (var track in tracks.Items)
            {
                Console.WriteLine("\t\t{0}", track.Title);
            }
        }

        private static async void ShowAlbumsTracksByArtist(string name)
        {
            var artist = await GetArtist(name);

            var releases = await Release.BrowseAsync("artist", artist.Id, 100, 0, "media");

            foreach (var release in releases.Items)
            {
                Console.WriteLine("\t{0}", release.Title);
                var tracks = await Recording.BrowseAsync("release", release.Id, 100);

                foreach (var track in tracks.Items)
                {
                    Console.WriteLine("\t\t{0}", track.Title);
                }
            }
        }


        private static async void Test()
        {
            var artists = await Artist.SearchAsync("The Scorpions");

            var artist = artists.Items.First();

            Console.WriteLine(artist.Name);

            var recordings = await Recording.BrowseAsync("artist", artist.Id, 40);

            foreach (var recording in recordings.Items)
            {
                Console.WriteLine("{0}", recording.Title);
            }
        }
    }
}
