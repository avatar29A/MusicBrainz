
namespace Hqub.MusicBrainz
{
    using System;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Helper class to build MusicBrainz web service urls.
    /// </summary>
    internal class UrlBuilder
    {
        private const string LookupTemplate = "{0}/{1}?fmt=json";
        private const string BrowseTemplate = "{0}?{1}={2}&fmt=json";
        private const string SearchTemplate = "{0}?query={1}&fmt=json";

        private const int DEFAULT_LIMIT = 25;
        private const int DEFAULT_OFFSET = 0;

        private readonly bool validate;

        public UrlBuilder(bool clientSideValidation)
        {
            validate = clientSideValidation;
        }

        /// <summary>
        /// Creates a web service lookup template.
        /// </summary>
        public string CreateLookupUrl(string entity, string mbid, params string[] inc)
        {
            var url = string.Format(LookupTemplate, entity, mbid);

            if (inc != null && inc.Length > 0)
            {
                url += "&inc=" + string.Join("+", inc);
            }

            return url;
        }

        /// <summary>
        /// Creates a web service browse template.
        /// </summary>
        public string CreateBrowseUrl(string entity, string relatedEntity, string mbid, int limit, int offset, params string[] inc)
        {
            return CreateBrowseUrl(entity, relatedEntity, mbid, null, null, limit, offset, inc);
        }

        /// <summary>
        /// Creates a web service browse template.
        /// </summary>
        public string CreateBrowseUrl(string entity, string relatedEntity, string mbid, string type, string status,
            int limit, int offset, params string[] inc)
        {
            var url = string.Format(BrowseTemplate, entity, relatedEntity, mbid);

            url = AddOptionalParams(url, limit, offset, inc);

            if (!string.IsNullOrEmpty(type))
            {
                if (validate && !ValidateBrowseParam(Resources.Constants.BrowseType, type))
                {
                    throw new ArgumentException(string.Format(Resources.Messages.InvalidQueryValue, type, "type"));
                }

                url += "&type=" + type;
            }

            if (!string.IsNullOrEmpty(status))
            {
                if (validate && !ValidateBrowseParam(Resources.Constants.BrowseStatus, status))
                {
                    throw new ArgumentException(string.Format(Resources.Messages.InvalidQueryValue, status, "status"));
                }

                url += "&status=" + status;
            }

            return url;
        }

        /// <summary>
        /// Creates a web service search template.
        /// </summary>
        public string CreateSearchUrl(string entity, string query, int limit, int offset)
        {
            query = WebUtility.UrlEncode(query);

            var url = string.Format(SearchTemplate, entity, query);

            url = AddOptionalParams(url, limit, offset, null);

            return url;
        }

        private string AddOptionalParams(string url, int limit, int offset, params string[] inc)
        {
            if (limit != DEFAULT_LIMIT)
            {
                url += "&limit=" + limit;
            }

            if (offset != DEFAULT_OFFSET)
            {
                url += "&offset=" + offset;
            }

            if (inc != null && inc.Length > 0)
            {
                url += "&inc=" + string.Join("+", inc);
            }

            return url;
        }

        private bool ValidateBrowseParam(string availableParams, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true; // Ignore, if no value specified.
            }

            if (value.IndexOf('|') > 0)
            {
                return value.Split('|').All(s => availableParams.Contains("+" + s + "+"));
            }

            return availableParams.Contains("+" + value + "+");
        }
    }
}
