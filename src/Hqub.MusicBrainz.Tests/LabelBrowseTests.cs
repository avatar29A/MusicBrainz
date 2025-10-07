
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities.Collections;
    using NUnit.Framework;
    using System.Threading.Tasks;

    // Resource: label-browse.json
    //
    // https://musicbrainz.org/ws/2/label?area=85752fda-13c4-31a3-bee5-0e5cb1f51dad&limit=10&fmt=json

    public class LabelBrowseTests
    {
        private LabelList data;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            data = await client.Labels.BrowseAsync("area", "85752fda-13c4-31a3-bee5-0e5cb1f51dad", 10);
        }

        [Test]
        public void TestLabelListQueryCount()
        {
            Assert.That(data.Offset, Is.EqualTo(0));
            Assert.That(data.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TestLabelListCount()
        {
            Assert.That(data.Items.Count, Is.EqualTo(10));
        }

        [Test]
        public void TestLabelListElements()
        {
            var label = data.Items[0];

            Assert.That(label.Id, Is.EqualTo("0af8c6dd-83b4-4e76-84a5-1ac315c355c2"));
        }
    }
}
