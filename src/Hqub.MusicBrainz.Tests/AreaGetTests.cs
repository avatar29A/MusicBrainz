namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    // Resource: area-get.json
    // URL: https://musicbrainz.org/ws/2/area/c9ac1239-e832-41bc-9930-e252a1fd1105?inc=area-rels&fmt=json

    public class AreaGetTests
    {
        private Area area;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            string[] inc = ["area-rels"];

            area = await client.Area.GetAsync("c9ac1239-e832-41bc-9930-e252a1fd1105", inc);
        }

        [Test]
        public void TestAreaElements()
        {
            Assert.That(area.Id, Is.EqualTo("c9ac1239-e832-41bc-9930-e252a1fd1105"));
            Assert.That(area.Type, Is.EqualTo("City"));
            Assert.That(area.Name, Is.EqualTo("Berlin"));
            Assert.That(area.Relations, Is.Not.Null);
            Assert.That(area.Iso2Codes, Is.Not.Null);
        }

        [Test]
        public void TestAreaForwardRelations()
        {
            var list = area.Relations.Where(r => r.TargetType == "area" && r.Direction == "forward");

            Assert.That(list.Count(), Is.GreaterThanOrEqualTo(6));
        }

        [Test]
        public void TestAreaBackwardRelations()
        {
            var list = area.Relations.Where(r => r.TargetType == "area" && r.Direction == "backward");

            Assert.That(list.Count(), Is.EqualTo(1));

            var relation = list.First();

            Assert.That(relation.Area, Is.Not.Null);
            Assert.That(relation.Area.Type, Is.EqualTo("Country"));
            Assert.That(relation.Area.Name, Is.EqualTo("Germany"));

        }
    }
}
