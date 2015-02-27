using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hqub.MusicBrainz.API.Entities.Metadata;

namespace Hqub.MusicBrainz.API.Test
{
    // Resource: release-group-search.xml
    // ReleaseGroup.Search("artist:(bob dylan)", 10);
    //
    // http://musicbrainz.org/ws/2/release-group?query=artist:%28bob%20dylan%29&limit=10

    [TestClass]
    public class ReleaseGroupListTests
    {
        ReleaseGroupMetadata data;

        public ReleaseGroupListTests()
        {
            this.data = TestHelper.Get<ReleaseGroupMetadata>("releasegroup-search.xml", false);
        }

        [TestMethod]
        public void TestReleaseGroupListQueryCount()
        {
            var group = data.Collection;

            Assert.AreEqual(2787, group.QueryCount);
        }

        [TestMethod]
        public void TestReleaseGroupListCount()
        {
            var group = data.Collection.Items;

            Assert.AreEqual(10, group.Count);
        }

        [TestMethod]
        public void TestReleaseGroupListElements()
        {
            var group = data.Collection.Items[0];

            Assert.AreEqual("fc325dd3-73ed-36aa-9c77-6b65a958e3cf", group.Id);
            Assert.AreEqual("Album", group.Type);
            Assert.AreEqual(100, group.Score);

            Assert.AreEqual("Desire", group.Title);
            Assert.AreEqual("Album", group.PrimaryType);

            Assert.IsNotNull(group.Releases);
            Assert.IsNotNull(group.Credits);
            Assert.IsNotNull(group.Tags);
        }
    }
}
