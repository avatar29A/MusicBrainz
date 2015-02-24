using Hqub.MusicBrainz.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hqub.MusicBrainz.API
{
    /// <summary>
    /// Helper for building MusicBrainz query strings.
    /// </summary>
    /// <typeparam name="T">The entity type to search for.</typeparam>
    /// <remarks>
    /// See https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search
    /// </remarks>
    public class QueryParameters<T> : IEnumerable<KeyValuePair<string, string>>
        where T : Entity
    {
        Dictionary<string, string> values;

        public QueryParameters()
        {
            values = new Dictionary<string, string>();
        }

        /// <summary>
        /// Add a field to the query paramaters.
        /// </summary>
        /// <param name="key">The field key.</param>
        /// <param name="value">The field value.</param>
        public void Add(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "key"));
            }

            if (!Validate(key))
            {
                throw new Exception(string.Format(Resources.Messages.InvalidQueryParameter, key));
            }

            values.Add(key, value);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return values.GetEnumerator();
        }

        public string ToString(bool urlEncode)
        {
            var sb = new StringBuilder();

            foreach (var item in values)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }

                sb.AppendFormat("{0}:({1})", item.Key, item.Value);
            }

            var query = sb.ToString();

            return urlEncode ? Uri.EscapeUriString(query) : query;
        }

        public override string ToString()
        {
            return this.ToString(true);
        }

        private bool Validate(string key)
        {
            key = "-" + key + "-";

            if (typeof(T) == typeof(Artist))
            {
                return Resources.Constants.ArtistQueryParams.IndexOf(key) >= 0;
            }

            if (typeof(T) == typeof(Recording))
            {
                return Resources.Constants.RecordingQueryParams.IndexOf(key) >= 0;
            }

            if (typeof(T) == typeof(Release))
            {
                return Resources.Constants.ReleaseQueryParams.IndexOf(key) >= 0;
            }

            if (typeof(T) == typeof(ReleaseGroup))
            {
                return Resources.Constants.ReleaseGroupQueryParams.IndexOf(key) >= 0;
            }

            return false;
        }
    }
}
