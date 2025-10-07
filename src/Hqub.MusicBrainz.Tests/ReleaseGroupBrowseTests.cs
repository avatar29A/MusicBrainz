namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities.Collections;
    using NUnit.Framework;
    using System.Threading.Tasks;

    // Resource: releasegroup-browse.json
    // URL: https://musicbrainz.org/ws/2/release-group?artist=45a663b5-b1cb-4a91-bff6-2bef7bbfdd76&limit=10&inc=ratings&fmt=json

    public class ReleaseGroupBrowseTests
    {
        private ReleaseGroupList data;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            data = await client.ReleaseGroups.BrowseAsync("artist", "45a663b5-b1cb-4a91-bff6-2bef7bbfdd76", 10, 0, "ratings");
        }

        [Test]
        public void TestReleaseGroupListQueryCount()
        {
            Assert.That(data.Offset, Is.EqualTo(0));
            Assert.That(data.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TestReleaseGroupListCount()
        {
            Assert.That(data.Items.Count, Is.EqualTo(10));
        }

        [Test]
        public void TestReleaseGroupListElements()
        {
            var release = data.Items[0];

            Assert.That(release.Id, Is.EqualTo("00351ca9-c8e0-4ac3-9f59-b2c485da9be0"));
        }
    }
}
