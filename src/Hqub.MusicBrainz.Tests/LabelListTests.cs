
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities.Collections;
    using NUnit.Framework;

    // Resource: label-search.json
    // Label.Search("City Slang");
    //
    // http://musicbrainz.org/ws/2/label/?query=%22City%20Slang%22&fmt=json

    public class LabelListTests
    {
        private readonly LabelList data;

        public LabelListTests()
        {
            data = TestHelper.GetJson<LabelList>("label-search.json");
        }

        [Test]
        public void TestLabelListQueryCount()
        {
            Assert.That(data.Offset, Is.EqualTo(0));
            Assert.That(data.Count, Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void TestLabelListCount()
        {
            var labels = data.Items;

            Assert.That(labels.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestLabelListElements()
        {
            var label = data.Items[0];

            Assert.That(label.Id, Is.EqualTo("82935ddb-a9d6-45a7-85e3-0b0add51fa1c"));
            Assert.That(label.Type, Is.EqualTo("Original Production"));
            Assert.That(label.Score, Is.EqualTo(100));

            Assert.That(label.Name, Is.EqualTo("City Slang"));
            Assert.That(label.SortName, Is.EqualTo("City Slang"));
            Assert.That(label.Country, Is.EqualTo("DE"));

            Assert.That(label.Area, Is.Not.Null);
            Assert.That(label.LifeSpan, Is.Not.Null);
            Assert.That(label.Tags, Is.Not.Null);
        }
    }
}
