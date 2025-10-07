
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities.Collections;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    // Resource: recording-search.json
    // Recording.Search("artist:(calexico) AND recording:(alone again or) AND NOT secondarytype:(live)", 10);
    //
    // https://musicbrainz.org/ws/2/recording?query=artist:(calexico)%20AND%20recording:(alone%20again%20or)%20AND%20NOT%20secondarytype:(live)&limit=10&fmt=json

    public class RecordingSearchTests
    {
        private RecordingList data;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            data = await client.Recordings.SearchAsync("artist:(calexico) AND recording:(alone again or) AND NOT secondarytype:(live)", 10);
        }

        [Test]
        public void TestRecordingListQueryCount()
        {
            Assert.That(data.Offset, Is.EqualTo(0));
            Assert.That(data.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TestRecordingListCount()
        {
            var recordings = data.Items;

            Assert.That(recordings.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TestRecordingListElements()
        {
            var recording = data.Items.Where(g => g.Id == "89d8f933-7c31-47c6-8f80-4927e93e7896").FirstOrDefault();

            Assert.That(recording, Is.Not.Null);

            Assert.That(recording.Score, Is.EqualTo(100));

            Assert.That(recording.Title, Is.EqualTo("Alone Again Or"));
            Assert.That(recording.Length, Is.GreaterThanOrEqualTo(1));

            Assert.That(recording.Credits, Is.Not.Null);
            Assert.That(recording.Releases, Is.Not.Null);
        }
    }
}
