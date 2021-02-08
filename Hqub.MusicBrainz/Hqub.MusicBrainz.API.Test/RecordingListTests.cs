
namespace Hqub.MusicBrainz.API.Test
{
    using Hqub.MusicBrainz.API.Entities.Collections;
    using NUnit.Framework;

    // Resource: recording-search.json
    // Recording.Search("artist:(calexico) AND recording:(alone again or) AND NOT secondarytype:(live)", 10);
    //
    // https://musicbrainz.org/ws/2/recording?query=artist:(calexico)%20AND%20recording:(alone%20again%20or)%20AND%20NOT%20secondarytype:(live)&limit=10&fmt=json

    public class RecordingListTests
    {
        RecordingList data;

        public RecordingListTests()
        {
            this.data = TestHelper.GetJson<RecordingList>("recording-search.json");
        }

        [Test]
        public void TestRecordingListQueryCount()
        {
            Assert.AreEqual(0, data.Offset);
            Assert.GreaterOrEqual(data.Count, 1);
        }

        [Test]
        public void TestRecordingListCount()
        {
            var recordings = data.Items;

            Assert.GreaterOrEqual(recordings.Count, 1);
        }

        [Test]
        public void TestRecordingListElements()
        {
            var recording = data.Items[0];

            Assert.AreEqual("89d8f933-7c31-47c6-8f80-4927e93e7896", recording.Id);
            Assert.AreEqual(100, recording.Score);

            Assert.AreEqual("Alone Again Or", recording.Title);
            Assert.GreaterOrEqual(recording.Length, 1);

            Assert.IsNotNull(recording.Credits);
            Assert.IsNotNull(recording.Releases);
        }
    }
}
