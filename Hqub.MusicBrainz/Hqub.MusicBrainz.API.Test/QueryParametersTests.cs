﻿
namespace Hqub.MusicBrainz.API.Test
{
    using Hqub.MusicBrainz.API.Entities;
    using NUnit.Framework;

    public class QueryParametersTests
    {
        [Test]
        public void TestSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("arid", "1");

            Assert.AreEqual("arid:1", query.ToString());
        }

        [Test]
        public void TestNegateSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("arid", "1", true);

            Assert.AreEqual("NOT arid:1", query.ToString());
        }

        [Test]
        public void TestQuoteSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "bob dylan");

            Assert.AreEqual("artist:\"bob dylan\"", query.ToString());
        }

        [Test]
        public void TestAlreadyQuotedSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "\"rolling stones\"");

            Assert.AreEqual("artist:\"rolling stones\"", query.ToString());
        }

        [Test]
        public void TestInlineSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "\"rolling stones\" OR jagger");

            Assert.AreEqual("artist:(\"rolling stones\" OR jagger)", query.ToString());
        }

        [Test]
        public void TestInlineBracketsSimpleQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "(\"rolling stones\" OR jagger)");

            Assert.AreEqual("artist:(\"rolling stones\" OR jagger)", query.ToString());
        }

        [Test]
        public void TestMultiQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "stones");
            query.Add("tag", "rock");

            Assert.AreEqual("artist:stones AND tag:rock", query.ToString());
        }

        [Test]
        public void TestNegateMultiQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "stones");
            query.Add("tag", "rock", true);

            Assert.AreEqual("artist:stones AND NOT tag:rock", query.ToString());
        }

        [Test]
        public void TestQuoteMultiQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "rolling stones");
            query.Add("tag", "rock");

            Assert.AreEqual("artist:\"rolling stones\" AND tag:rock", query.ToString());
        }

        [Test]
        public void TestInlineMultiQuery()
        {
            var query = new QueryParameters<Artist>();

            query.Add("artist", "\"rolling stones\" OR jagger");
            query.Add("tag", "rock", true);

            Assert.AreEqual("artist:(\"rolling stones\" OR jagger) AND NOT tag:rock", query.ToString());
        }
    }
}
