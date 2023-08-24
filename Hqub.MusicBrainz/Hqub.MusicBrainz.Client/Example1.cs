
namespace Hqub.MusicBrainz.Client
{
    using Hqub.MusicBrainz.API;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// Search for an artist using 'Artist.SearchAsync' and lookup artist details
    /// like band-members and related urls using 'Artist.GetAsync'.
    /// </summary>
    public class Example1
    {
        public static async Task Run(MusicBrainzClient client)
        {
            await Search(client, "The Rolling Stones");
        }

        public static async Task Search(MusicBrainzClient client, string name)
        {
            // Search for an artist by name (limit to 20 matches).
            var artists = await client.Artists.SearchAsync(name.Quote(), 20);

            Console.WriteLine("Total matches for '{0}': {1}", name, artists.Count);

            // Count matches with score 100.
            int count = artists.Items.Count(a => a.Score == 100);

            Console.WriteLine("Exact matches for '{0}': {1}", name, count);

            // By default, search results will be ordered by score, so to get the
            // best match you could do artists.Items.First(). Sometimes this method
            // won't work (example: search for 'U2').
            // 
            // If the search string is the exact name, it might be better to compare
            // to that string or to order by similarity, like done here:

            var artist = artists.Items.OrderByDescending(a => Levenshtein.Similarity(a.Name, name)).First();

            // Get detailed information of the artist, including band-members and related urls.
            artist = await client.Artists.GetAsync(artist.Id, "artist-rels", "url-rels");

            Console.WriteLine();
            Console.WriteLine("Current band members of '{0}':", artist.Name);
            Console.WriteLine();

            // Band members are represented as artist-artist relationships. To filter relations,
            // inspect "TargetType" and "Type" properties.
            var members = artist.Relations.Where(r => r.TargetType == "artist" && r.Type.Contains("member"));

            foreach (var relation in members.Where(r => !(bool)r.Ended))
            {
                Console.WriteLine("     {0}", relation.Artist.Name);
            }

            // Lyric are represented as artist-url relationships.
            var lyrics = artist.Relations.Where(r => r.TargetType == "url" && r.Type == "lyrics");

            if (lyrics.Count() > 0)
            {
                Console.WriteLine();
                Console.WriteLine("You can find lyrics for '{0}' at", artist.Name);
                Console.WriteLine();

                foreach (var relation in lyrics)
                {
                    Console.WriteLine("     {0}", relation.Url.Resource);
                }
            }
        }
    }
}
