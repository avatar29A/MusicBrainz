
namespace Hqub.MusicBrainz.API.Test
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using NUnit.Framework;

    // Resource: release-search.json
    // Release.Search("artist:(giant sand) release:(tucson)", 10);
    //
    // http://musicbrainz.org/ws/2/release?query=artist:(giant%20sand)%20release:(tucson)&limit=10&fmt=json

    public class ReleaseListTests
    {
        ReleaseList data;

        public ReleaseListTests()
        {
            this.data = TestHelper.GetJson<ReleaseList>("release-search.json");
        }

        [Test]
        public void TestReleaseListQueryCount()
        {
            Assert.AreEqual(0, data.Offset);
            Assert.AreEqual(839, data.Count);
        }

        [Test]
        public void TestReleaseListCount()
        {
            var releases = data.Items;

            Assert.AreEqual(10, releases.Count);
        }

        [Test]
        public void TestReleaseListElements()
        {
            var release = data.Items[1];

            Assert.AreEqual("12195c41-6136-4dfd-acf1-9923dadc73e2", release.Id);
            Assert.AreEqual(68, release.Score);

            Assert.AreEqual("Tucson: A Country Rock Opera", release.Title);
            Assert.AreEqual("Official", release.Status);
            Assert.AreEqual("2012-06-11", release.Date);
            Assert.AreEqual("US", release.Country);
            Assert.AreEqual("809236126221", release.Barcode);

            Assert.IsNotNull(release.Credits);
            Assert.IsNotNull(release.ReleaseGroup);
            Assert.IsNotNull(release.Labels);
            Assert.IsNotNull(release.Media);
        }
    }
}
