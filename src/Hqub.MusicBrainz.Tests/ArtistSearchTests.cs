namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities;
    using NUnit.Framework;
    using System.Threading.Tasks;

    // Resource: artist-search.json
    // URL: https://musicbrainz.org/ws/2/artist?query=artist:(bob%20dylan)&limit=10&fmt=json

    public class ArtistSearchTests
    {
        private QueryResult<Artist> data;

        [OneTimeSetUp]
        public async Task Init()
        {
            var client = new MusicBrainzClient()
            {
                Cache = EmbeddedResourceCache.Instance
            };

            data = await client.Artists.SearchAsync("artist:(bob dylan)", 10);
        }

        [Test]
        public void TestArtistListQueryCount()
        {
            Assert.That(data.Offset, Is.EqualTo(0));
            Assert.That(data.Count, Is.GreaterThanOrEqualTo(1000));
        }

        [Test]
        public void TestArtistListCount()
        {
            var artists = data.Items;

            Assert.That(artists.Count, Is.EqualTo(10));
        }

        [Test]
        public void TestArtistListElements()
        {
            var artist = data.Items[0];

            Assert.That(artist.Id, Is.EqualTo("72c536dc-7137-4477-a521-567eeb840fa8"));
            Assert.That(artist.Type, Is.EqualTo("Person"));
            Assert.That(artist.Score, Is.EqualTo(100));

            Assert.That(artist.Name, Is.EqualTo("Bob Dylan"));
            Assert.That(artist.SortName, Is.EqualTo("Dylan, Bob"));
            Assert.That(artist.Gender, Is.EqualTo("male"));
            Assert.That(artist.Country, Is.EqualTo("US"));

            Assert.That(artist.Area, Is.Not.Null);
            Assert.That(artist.LifeSpan, Is.Not.Null);
            Assert.That(artist.Tags, Is.Not.Null);
        }
    }
}
