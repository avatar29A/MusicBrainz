namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities.Collections;
    using NUnit.Framework;
    using System.Threading.Tasks;

    // Resource: artist-browse.json
    // URL: https://musicbrainz.org/ws/2/artist?area=c9ac1239-e832-41bc-9930-e252a1fd1105&limit=10&fmt=json

    public class ArtistBrowseTests
    {
        private ArtistList data;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            data = await client.Artists.BrowseAsync("area", "c9ac1239-e832-41bc-9930-e252a1fd1105", 10);
        }

        [Test]
        public void TestArtistListQueryCount()
        {
            Assert.That(data.Offset, Is.EqualTo(0));
            Assert.That(data.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TestArtistListCount()
        {
            Assert.That(data.Items.Count, Is.EqualTo(10));
        }

        [Test]
        public void TestArtistListElements()
        {
            var artist = data.Items[0];

            Assert.That(artist.Id, Is.EqualTo("00ad9fb8-674f-4770-a77e-fbb5c9bdbfea"));
        }
    }
}
