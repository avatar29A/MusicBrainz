using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Hqub.MusicBrainz.API.Entities.Metadata;

namespace Hqub.MusicBrainz.API.Test
{
    // Resource: recording-search.xml
    // Recording.Search("artist:(calexico) AND recording:(alone again or) AND NOT secondarytype:(live)", 10);
    //
    // http://musicbrainz.org/ws/2/recording?query=artist:(calexico)%20AND%20recording:(alone%20again%20or)%20AND%20NOT%20secondarytype:(live)&limit=10

    [TestClass]
    public class RecordingListTests
    {
        RecordingMetadata data;

        public RecordingListTests()
        {
            this.data = TestHelper.Get<RecordingMetadata>("recording-search.xml", false);
        }

        [TestMethod]
        public void TestRecordingListQueryCount()
        {
            var recordings = data.Collection;

            Assert.AreEqual(8, recordings.QueryCount);
        }

        [TestMethod]
        public void TestRecordingListCount()
        {
            var recordings = data.Collection.Items;

            Assert.AreEqual(8, recordings.Count);
        }

        [TestMethod]
        public void TestRecordingListElements()
        {
            var recording = data.Collection.Items[0];

            Assert.AreEqual("89d8f933-7c31-47c6-8f80-4927e93e7896", recording.Id);
            Assert.AreEqual(100, recording.Score);

            Assert.AreEqual("Alone Again Or", recording.Title);
            Assert.AreEqual(195173, recording.Length);

            Assert.IsNotNull(recording.Credits);
            Assert.IsNotNull(recording.Releases);
        }
    }
}
