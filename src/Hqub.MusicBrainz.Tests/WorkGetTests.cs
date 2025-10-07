
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    // Resource: work-get.json
    // https://musicbrainz.org/ws/2/work/0e23ed77-ad7e-34e7-b57c-c3407b2ae5df?inc=url-rels+artist-rels&fmt=json

    public class WorkGetTests
    {
        private Work work;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            string[] inc = ["url-rels", "artist-rels"];

            work = await client.Work.GetAsync("0e23ed77-ad7e-34e7-b57c-c3407b2ae5df", inc);
        }

        [Test]
        public void TestWorkElements()
        {
            Assert.That(work.Id, Is.EqualTo("0e23ed77-ad7e-34e7-b57c-c3407b2ae5df"));
            Assert.That(work.Type, Is.EqualTo("Song"));
            Assert.That(work.Title, Is.EqualTo("Teardrop"));
            Assert.That(work.Language, Is.EqualTo("eng"));
            Assert.That(work.Relations, Is.Not.Null);
        }

        [Test]
        public void TestWorkArtistRelations()
        {
            var list = work.Relations.Where(r => r.TargetType == "artist");

            Assert.That(list, Is.Not.Empty);
            Assert.That(list.Count(), Is.GreaterThanOrEqualTo(4));
        }

        [Test]
        public void TestWorkUrlRelations()
        {
            var list = work.Relations.Where(r => r.TargetType == "url");

            Assert.That(list, Is.Not.Empty);
            Assert.That(list.Count(), Is.GreaterThanOrEqualTo(3));
        }
    }
}
