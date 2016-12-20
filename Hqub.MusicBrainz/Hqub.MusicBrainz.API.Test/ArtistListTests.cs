using Hqub.MusicBrainz.API.Entities.Metadata;
using NUnit.Framework;

namespace Hqub.MusicBrainz.API.Test
{
    // Resource: artist-search.xml
    // Artist.Search("artist:(bob dylan)", 10);
    //
    // http://musicbrainz.org/ws/2/artist?query=artist:(bob%20dylan)&limit=10

    public class ArtistListTests
    {
        ArtistMetadata data;

        public ArtistListTests()
        {
            this.data = TestHelper.Get<ArtistMetadata>("artist-search.xml", false);
        }

        [Test]
        public void TestArtistListQueryCount()
        {
            var artists = data.Collection;

            Assert.AreEqual(1899, artists.QueryCount);
        }

        [Test]
        public void TestArtistListCount()
        {
            var artists = data.Collection.Items;

            Assert.AreEqual(10, artists.Count);
        }

        [Test]
        public void TestArtistListElements()
        {
            var artist = data.Collection.Items[0];

            Assert.AreEqual("72c536dc-7137-4477-a521-567eeb840fa8", artist.Id);
            Assert.AreEqual("Person", artist.Type);
            Assert.AreEqual(100, artist.Score);

            Assert.AreEqual("Bob Dylan", artist.Name);
            Assert.AreEqual("Dylan, Bob", artist.SortName);
            Assert.AreEqual("male", artist.Gender);
            Assert.AreEqual("US", artist.Country);

            //Assert.IsNotNull(artist.Area);
            Assert.IsNotNull(artist.LifeSpan);
            Assert.IsNotNull(artist.Tags);
        }
    }
}
