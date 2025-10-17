namespace Hqub.MusicBrainz.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    static class FetchTestData
    {
        private static readonly Dictionary<string, string> Data = new Dictionary<string, string>()
        {
            { "artist-get", "artist/72c536dc-7137-4477-a521-567eeb840fa8?inc=release-groups+tags+genres+works+ratings+artist-rels+url-rels+aliases" },
            { "artist-search", "artist?query=artist:(bob dylan)&limit=10" },
            { "artist-browse", "artist?area=c9ac1239-e832-41bc-9930-e252a1fd1105&limit=10" },

            { "label-get", "label/82935ddb-a9d6-45a7-85e3-0b0add51fa1c?inc=releases+artist-credits+genres+url-rels" },
            { "label-search", "label?query=%22City%20Slang%22" },
            { "label-browse", "label?area=85752fda-13c4-31a3-bee5-0e5cb1f51dad&limit=10" },

            { "recording-get", "recording/9408b8ce-9b95-4fb0-ac70-595d054a15c6?inc=artists+releases+tags+genres+ratings+url-rels" },
            { "recording-search", "recording?query=artist:calexico AND recording:(alone again or) AND NOT secondarytype:live&limit=10" },
            { "recording-browse", "recording?release=12195c41-6136-4dfd-acf1-9923dadc73e2&inc=ratings" },

            { "releasegroup-get", "release-group/fc325dd3-73ed-36aa-9c77-6b65a958e3cf?inc=artists+releases+ratings+genres+url-rels" },
            { "releasegroup-search", "release-group?query=artist:(bob dylan)" },
            { "releasegroup-browse", "release-group?artist=45a663b5-b1cb-4a91-bff6-2bef7bbfdd76&limit=10&inc=ratings" },

            { "release-get", "release/12195c41-6136-4dfd-acf1-9923dadc73e2?inc=artists+labels+recordings+release-groups+genres+url-rels" },
            { "release-search", "release?query=artist:(giant sand) release:tucson&limit=10" },
            { "release-browse", "release?label=82935ddb-a9d6-45a7-85e3-0b0add51fa1c&limit=10" },

            { "area-get", "area/c9ac1239-e832-41bc-9930-e252a1fd1105?inc=area-rels" },
            { "work-get", "work/0e23ed77-ad7e-34e7-b57c-c3407b2ae5df?inc=url-rels+artist-rels" },
        };

        public static async Task Download(bool overwrite = false)
        {
            var client = new HttpClient() { BaseAddress = new Uri("https://musicbrainz.org/ws/2/") };

            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:143.0) Gecko/20100101 Firefox/143.0");

            foreach (var item in Data)
            {
                await Get(client, item.Key, item.Value, overwrite);
            }

            Console.Write("Done. Press any key to quit.");
        }

        private static async Task Get(HttpClient client, string target, string url, bool overwrite)
        {
            url += "&fmt=json";

            target = target + ".json";

            Console.Write("Saving {0} ... ", target);

            try
            {
                if (File.Exists(target) && !overwrite)
                {
                    Console.WriteLine("ALREADY EXISTS");
                    return;
                }

                var stream = await client.GetStreamAsync(url);

                using var file = File.Open(target, FileMode.Create);
                
                stream.CopyTo(file);

                Console.WriteLine("OK");

                // Avoid rate limit.
                await Task.Delay(500);
            }
            catch
            {
                Console.WriteLine("FAILED");
            }
        }
    }
}
