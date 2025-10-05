
namespace Hqub.MusicBrainz.Client
{
    using Hqub.MusicBrainz;
    using Hqub.MusicBrainz.Entities;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Setup an advanced search query to find a recording using <c>client.Recordings.SearchAsync()</c>
    /// and get details including related work using <c>client.Recordings.GetAsync()</c>. Then get info
    /// for the related work (like lyrics) using <c>client.Work.GetAsync()</c>.
    /// </summary>
    class Example4
    {
        public static async Task Run(MusicBrainzClient client)
        {
            await Search(client, "Massive Attack", "Mezzanine", "Teardrop");
        }

        public static async Task Search(MusicBrainzClient client, string artist, string album, string song)
        {
            // Build an advanced query to search for the recording.
            var query = new QueryParameters<Recording>()
            {
                { "artist", artist },
                { "release", album },
                { "recording", song }
            };

            // Search for a recording by title.
            var recordings = await client.Recordings.SearchAsync(query);

            Console.WriteLine("Total matches for '{0} ({1}) {2}': {3}", artist, album, song, recordings.Count);

            // Get exact matches.
            var matches = recordings.Items.Where(r => r.Title == song && r.Releases.Any(s => s.Title == album));

            // Get the best match (in this case, we use the recording that has the most releases associated).
            var recording = matches.OrderByDescending(r => r.Releases.Count).First();

            // Get the first official release.
            var release = recording.Releases.Where(r => r.Title == album && IsOfficial(r)).OrderBy(r => r.Date).First();

            // Get detailed information of the recording, including related works.
            recording = await client.Recordings.GetAsync(recording.Id, "work-rels");

            if (recording.Relations.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("No related work available for the selected recording.");
                Console.WriteLine();

                return;
            }
            
            // Expect only a single work related to recording.
            var work = recording.Relations.First().Work;

            // Get detailed information of the work, including related urls.
            work = await client.Work.GetAsync(work.Id, "url-rels");

            // Check if there are lyrics available for the recording.
            var lyrics = work.Relations.Where(r => r.Type == "lyrics");

            if (lyrics.Any())
            {
                Console.WriteLine();
                Console.WriteLine("You can find lyrics for '{0} - {1} ({2})' at", artist, recording.Title, release.Date.ToShortDate());
                Console.WriteLine();
                Console.WriteLine("     {0}", lyrics.First().Url.Resource);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No lyrics available :-(");
                Console.WriteLine();

                return;
            }
        }

        static bool IsOfficial(Release r)
        {
            return !string.IsNullOrEmpty(r.Date) && !string.IsNullOrEmpty(r.Status)
                 && r.Status.Equals("Official", StringComparison.OrdinalIgnoreCase);
        }
    }
}
