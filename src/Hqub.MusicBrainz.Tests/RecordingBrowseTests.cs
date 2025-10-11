namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities;
    using NUnit.Framework;
    using System.Threading.Tasks;

    // Resource: recording-browse.json
    // URL: https://musicbrainz.org/ws/2/recording?release=12195c41-6136-4dfd-acf1-9923dadc73e2&inc=ratings&fmt=json

    public class RecordingBrowseTests
    {
        private QueryResult<Recording> data;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            data = await client.Recordings.BrowseAsync("release", "12195c41-6136-4dfd-acf1-9923dadc73e2", inc: ["ratings"]);
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
            Assert.That(data.Items.Count, Is.EqualTo(19));
        }

        [Test]
        public void TestRecordingListElements()
        {
            var release = data.Items[0];

            Assert.That(release.Id, Is.EqualTo("0f2e5fd4-7397-468e-ae0e-a6bc373eebce"));
        }
    }
}
