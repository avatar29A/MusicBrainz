using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hqub.MusicBrainz.API.Entities.Metadata;

namespace Hqub.MusicBrainz.API.Test
{
    // Resource: release-search.xml
    // Release.Search("artist:(giant sand) release:(tucson)", 10);
    //
    // http://musicbrainz.org/ws/2/release?query=artist:(giant%20sand)%20release:(tucson)&limit=10

    [TestClass]
    public class ReleaseListTests
    {
        ReleaseMetadata data;

        public ReleaseListTests()
        {
            this.data = TestHelper.Get<ReleaseMetadata>("release-search.xml", false);
        }

        [TestMethod]
        public void TestReleaseListQueryCount()
        {
            var releases = data.Collection;

            Assert.AreEqual(692, releases.QueryCount);
        }

        [TestMethod]
        public void TestReleaseListCount()
        {
            var releases = data.Collection.Items;

            Assert.AreEqual(10, releases.Count);
        }

        [TestMethod]
        public void TestReleaseListElements()
        {
            var release = data.Collection.Items[0];

            Assert.AreEqual("12195c41-6136-4dfd-acf1-9923dadc73e2", release.Id);
            //Assert.AreEqual(100, release.Score);

            Assert.AreEqual("Tucson: A Country Rock Opera", release.Title);
            Assert.AreEqual("Official", release.Status);
            Assert.AreEqual("2012-06-11", release.Date);
            Assert.AreEqual("US", release.Country);
            Assert.AreEqual("809236126221", release.Barcode);

            Assert.IsNotNull(release.Credits);
            Assert.IsNotNull(release.ReleaseGroup);
            Assert.IsNotNull(release.Labels);
            Assert.IsNotNull(release.MediumList);
        }
    }
}
