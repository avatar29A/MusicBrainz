using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hqub.MusicBrainz.API.Entities.Metadata;
using Hqub.MusicBrainz.API.Entities;

namespace Hqub.MusicBrainz.API.Test
{
    // Resource: release-group-get.xml
    // ReleaseGroup.Get("fc325dd3-73ed-36aa-9c77-6b65a958e3cf", "artists", "releases");
    //
    // http://musicbrainz.org/ws/2/release-group/fc325dd3-73ed-36aa-9c77-6b65a958e3cf?inc=artists+releases
    
    [TestClass]
    public class ReleaseGroupTests
    {
        ReleaseGroup group;

        public ReleaseGroupTests()
        {
            this.group = TestHelper.Get<ReleaseGroup>("releasegroup-get.xml");
        }

        [TestMethod]
        public void TestReleaseGroupAttributes()
        {
            Assert.IsNotNull(group);
            Assert.AreEqual("fc325dd3-73ed-36aa-9c77-6b65a958e3cf", group.Id);
            Assert.AreEqual("Album", group.Type);
        }

        [TestMethod]
        public void TestReleaseGroupElements()
        {
            Assert.AreEqual("Desire", group.Title);
            Assert.AreEqual("Album", group.PrimaryType);
            Assert.AreEqual("1976-01-05", group.FirstReleaseDate);
        }

        [TestMethod]
        public void TestReleaseGroupCredits()
        {
            var credits = group.Credits;

            Assert.IsNotNull(credits);

            var artist = credits[0].Artist;

            Assert.IsNotNull(artist);
            Assert.AreEqual("72c536dc-7137-4477-a521-567eeb840fa8", artist.Id);
            Assert.AreEqual("Bob Dylan", artist.Name);
        }

        [TestMethod]
        public void TestReleaseGroupReleases()
        {
            var releases = group.Releases;

            Assert.IsNotNull(releases);
            Assert.AreEqual(10, releases.Count);
        }
    }
}
