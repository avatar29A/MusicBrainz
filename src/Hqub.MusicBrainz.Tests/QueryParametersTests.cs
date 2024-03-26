
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities;
    using NUnit.Framework;

    public class QueryParametersTests
    {
        [Test]
        public void TestSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("arid", "1");

            Assert.That(query.ToString(), Is.EqualTo("arid:1"));
        }

        [Test]
        public void TestNegateSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("arid", "1", true);

            Assert.That(query.ToString(), Is.EqualTo("NOT arid:1"));
        }

        [Test]
        public void TestQuoteSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "bob dylan");

            Assert.That(query.ToString(), Is.EqualTo("artist:\"bob dylan\""));
        }

        [Test]
        public void TestAlreadyQuotedSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "\"rolling stones\"");

            Assert.That(query.ToString(), Is.EqualTo("artist:\"rolling stones\""));
        }

        [Test]
        public void TestInlineSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "\"rolling stones\" OR jagger");

            Assert.That(query.ToString(), Is.EqualTo("artist:(\"rolling stones\" OR jagger)"));
        }

        [Test]
        public void TestInlineBracketsSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "(\"rolling stones\" OR jagger)");

            Assert.That(query.ToString(), Is.EqualTo("artist:(\"rolling stones\" OR jagger)"));
        }

        [Test]
        public void TestMultiQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "stones");
            query.Add("tag", "rock");

            Assert.That(query.ToString(), Is.EqualTo("artist:stones AND tag:rock"));
        }

        [Test]
        public void TestNegateMultiQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "stones");
            query.Add("tag", "rock", true);

            Assert.That(query.ToString(), Is.EqualTo("artist:stones AND NOT tag:rock"));
        }

        [Test]
        public void TestQuoteMultiQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "rolling stones");
            query.Add("tag", "rock");

            Assert.That(query.ToString(), Is.EqualTo("artist:\"rolling stones\" AND tag:rock"));
        }

        [Test]
        public void TestInlineMultiQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "\"rolling stones\" OR jagger");
            query.Add("tag", "rock", true);

            Assert.That(query.ToString(), Is.EqualTo("artist:(\"rolling stones\" OR jagger) AND NOT tag:rock"));
        }
    }
}
