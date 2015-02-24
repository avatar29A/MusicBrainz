using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hqub.MusicBrainz.API.Entities.Metadata;

namespace Hqub.MusicBrainz.API.Test
{
    // Resource: artist-search.xml
    // Artist.Search("artist:(bob dylan)", 10);
    //
    // http://musicbrainz.org/ws/2/artist?query=artist:(bob%20dylan)&limit=10

    [TestClass]
    public class ArtistListTests
    {
        ArtistMetadataWrapper data;

        public ArtistListTests()
        {
            this.data = TestHelper.Get<ArtistMetadataWrapper>("artist-search.xml", false);
        }

        [TestMethod]
        public void TestArtistListQueryCount()
        {
            var artists = data.Collection;

            Assert.AreEqual(1899, artists.QueryCount);
        }

        [TestMethod]
        public void TestArtistListCount()
        {
            var artists = data.Collection;

            Assert.AreEqual(10, artists.Count);
        }

        [TestMethod]
        public void TestArtistListElement()
        {
            var artist = data.Collection[0];

            Assert.AreEqual("72c536dc-7137-4477-a521-567eeb840fa8", artist.Id);
            Assert.AreEqual("Person", artist.Type);
            //Assert.AreEqual(artist, artist.Score);

            Assert.AreEqual("Bob Dylan", artist.Name);
            Assert.AreEqual("Dylan, Bob", artist.SortName);
            //Assert.AreEqual("male", artist.Gender);
            Assert.AreEqual("US", artist.Country);

            //Assert.IsNotNull(artist.Area);
            Assert.IsNotNull(artist.LifeSpan);
            Assert.IsNotNull(artist.Tags);
        }
    }
}
