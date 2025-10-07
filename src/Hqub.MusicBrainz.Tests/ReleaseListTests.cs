
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities.Collections;
    using NUnit.Framework;
    using System.Threading.Tasks;

    // Resource: release-search.json
    // Release.Search("artist:(giant sand) release:(tucson)", 10);
    //
    // https://musicbrainz.org/ws/2/release?query=artist:(giant%20sand)%20release:(tucson)&limit=10&fmt=json

    public class ReleaseListTests
    {
        private ReleaseList data;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            data = await client.Releases.SearchAsync("artist:(giant sand) release:(tucson)", 10);
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
            var release = data.Items[3];

            Assert.That(release.Id, Is.EqualTo("12195c41-6136-4dfd-acf1-9923dadc73e2"));
            Assert.That(release.Score, Is.GreaterThanOrEqualTo(1));

            Assert.That(release.Title, Is.EqualTo("Tucson: A Country Rock Opera"));
            Assert.That(release.Status, Is.EqualTo("Official"));
            Assert.That(release.Date, Is.EqualTo("2012-06-11"));
            Assert.That(release.Country, Is.EqualTo("US"));
            Assert.That(release.Barcode, Is.EqualTo("809236126221"));

            Assert.That(release.Credits, Is.Not.Null);
            Assert.That(release.ReleaseGroup, Is.Not.Null);
            Assert.That(release.Labels, Is.Not.Null);
            Assert.That(release.Media, Is.Not.Null);
        }
    }
}
