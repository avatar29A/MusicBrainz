
namespace Hqub.MusicBrainz.Tests
{
    using Hqub.MusicBrainz.Entities;
    using NUnit.Framework;
    using System;

    public class QueryParametersTests
    {
        [Test]
        public void TestSimpleQuery()
        {
            var query = new QueryParameters<Artist>
            {
                { "arid", "1" }
            };

            Assert.That(query.ToString(), Is.EqualTo("arid:1"));
        }

        [Test]
        public void TestNegateSimpleQuery()
        {
            var query = new QueryParameters<Artist>
            {
                { "arid", "1", true }
            };

            Assert.That(query.ToString(), Is.EqualTo("NOT arid:1"));
        }

        [Test]
        public void TestQuoteSimpleQuery()
        {
            var query = new QueryParameters<Artist>
            {
                { "artist", "bob dylan" }
            };

            Assert.That(query.ToString(), Is.EqualTo("artist:\"bob dylan\""));
        }

        [Test]
        public void TestAlreadyQuotedSimpleQuery()
        {
            var query = new QueryParameters<Artist>
            {
                { "artist", "\"rolling stones\"" }
            };

            Assert.That(query.ToString(), Is.EqualTo("artist:\"rolling stones\""));
        }

        [Test]
        public void TestInlineSimpleQuery()
        {
            var query = new QueryParameters<Artist>
            {
                { "artist", "\"rolling stones\" OR jagger" }
            };

            Assert.That(query.ToString(), Is.EqualTo("artist:(\"rolling stones\" OR jagger)"));
        }

        [Test]
        public void TestInlineBracketsSimpleQuery()
        {
            var query = new QueryParameters<Artist>
            {
                { "artist", "(\"rolling stones\" OR jagger)" }
            };

            Assert.That(query.ToString(), Is.EqualTo("artist:(\"rolling stones\" OR jagger)"));
        }

        [Test]
        public void TestMultiQuery()
        {
            var query = new QueryParameters<Artist>
            {
                { "artist", "stones" },
                { "tag", "rock" }
            };

            Assert.That(query.ToString(), Is.EqualTo("artist:stones AND tag:rock"));
        }

        [Test]
        public void TestNegateMultiQuery()
        {
            var query = new QueryParameters<Artist>
            {
                { "artist", "stones" },
                { "tag", "rock", true }
            };

            Assert.That(query.ToString(), Is.EqualTo("artist:stones AND NOT tag:rock"));
        }

        [Test]
        public void TestQuoteMultiQuery()
        {
            var query = new QueryParameters<Artist>
            {
                { "artist", "rolling stones" },
                { "tag", "rock" }
            };

            Assert.That(query.ToString(), Is.EqualTo("artist:\"rolling stones\" AND tag:rock"));
        }

        [Test]
        public void TestInlineMultiQuery()
        {
            var query = new QueryParameters<Artist>
            {
                { "artist", "\"rolling stones\" OR jagger" },
                { "tag", "rock", true }
            };

            Assert.That(query.ToString(), Is.EqualTo("artist:(\"rolling stones\" OR jagger) AND NOT tag:rock"));
        }

        [Test]
        public void TestParameterValidation()
        {
            var query = new QueryParameters<Artist>();

            // See https://musicbrainz.org/doc/MusicBrainz_API/Search#Search_Fields_3
            string[] validQueryParameters =
            [
                "alias",
                "primary_alias",
                "area",
                "arid",
                "artist",
                "artistaccent",
                "begin",
                "beginarea",
                "comment",
                "country",
                "end",
                "endarea",
                "ended",
                "gender",
                "ipi",
                "isni",
                "sortname",
                "tag",
                "type"
            ];

            Assert.DoesNotThrow(() =>
            {
                foreach (var key in validQueryParameters)
                {
                    query.Add(key, "dummmy");
                }
            });

            Assert.Throws<ArgumentException>(() => query.Add("invalidParam", "dummmy"), "Key not supported (invalidParam).");
        }
    }
}
