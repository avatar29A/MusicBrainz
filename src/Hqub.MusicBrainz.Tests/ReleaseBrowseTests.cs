
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities.Collections;
    using NUnit.Framework;
    using System.Threading.Tasks;

    // Resource: release-browse.json
    //
    // https://musicbrainz.org/ws/2/release?label=82935ddb-a9d6-45a7-85e3-0b0add51fa1c&limit=10&fmt=json

    public class ReleaseBrowseTests
    {
        private ReleaseList data;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            data = await client.Releases.BrowseAsync("label", "82935ddb-a9d6-45a7-85e3-0b0add51fa1c", 10);
        }

        [Test]
        public void TestReleaseListQueryCount()
        {
            Assert.That(data.Offset, Is.EqualTo(0));
            Assert.That(data.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TestReleaseListCount()
        {
            var releases = data.Items;

            Assert.That(releases.Count, Is.EqualTo(10));
        }

        [Test]
        public void TestReleaseListElements()
        {
            var release = data.Items[0];

            Assert.That(release.Id, Is.EqualTo("187623a9-98a0-4d51-a76e-6e6fc81b2e59"));
        }
    }
}
