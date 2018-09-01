﻿
namespace Hqub.MusicBrainz.Client
{
    using Hqub.MusicBrainz.API.Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Browse release-groups of a given artist using 'ReleaseGroup.BrowseAsync'.
    /// </summary>
    public class Example5
    {
        public static async Task Run()
        {
            await Browse("Britney Spears");
        }
        
        private static async Task Browse(string name)
        {
            // Search for an artist by name (limit to 20 matches).
            var artists = await Artist.SearchAsync(name.Quote(), 20);

            Console.WriteLine("Browsing release-groups of '{0}'", name);

            var artist = artists.Items.OrderByDescending(a => Levenshtein.Similarity(a.Name, name)).First();
            
            int limit = 50;

            // Browse the first 50 release-groups of given artist, include ratings.
            var groups = await ReleaseGroup.BrowseAsync("artist", artist.Id, limit, 0, "ratings");

            Console.WriteLine();
            Console.WriteLine("Album");
            Console.WriteLine();

            // Show offical albums.
            foreach (var item in groups.Items.Where(g => IsOffical(g)).OrderBy(g => g.FirstReleaseDate))
            {
                Console.WriteLine("     {0} - {1}  {2}  {3}", item.FirstReleaseDate.ToShortDate(),
                    item.Id, GetRating(item.Rating, 10), item.Title);
            }

            Console.WriteLine();
            Console.WriteLine("Album + Compilation");
            Console.WriteLine();

            // Show compilations.
            foreach (var item in groups.Items.Where(g => IsCompilation(g)).OrderBy(g => g.FirstReleaseDate))
            {
                Console.WriteLine("     {0} - {1}  {2}  {3}", item.FirstReleaseDate.ToShortDate(),
                    item.Id, GetRating(item.Rating, 10), item.Title);
            }

            Console.WriteLine();
            
            if (groups.Items.Count == limit)
            {
                Console.WriteLine("There are probably more items to browse ...");
            }

            Console.WriteLine();
        }

        static bool IsOffical(ReleaseGroup g)
        {
            return g.PrimaryType.Equals("album", StringComparison.OrdinalIgnoreCase)
                && g.SecondaryTypes.Count == 0
                && !string.IsNullOrEmpty(g.FirstReleaseDate);
        }

        static bool IsCompilation(ReleaseGroup g)
        {
            return g.PrimaryType.Equals("album", StringComparison.OrdinalIgnoreCase)
                && g.SecondaryTypes.Contains("Compilation")
                && !string.IsNullOrEmpty(g.FirstReleaseDate);
        }

        static string GetRating(Rating rating, int length)
        {
            string s = "";

            if (!rating.Value.HasValue)
            {
                return s.PadRight(length);
            }

            int stars = (int)(length * (double)rating.Value / 5.0);

            s = s.PadRight(stars, '+');

            return s.PadRight(length, '-');
        }
    }
}
