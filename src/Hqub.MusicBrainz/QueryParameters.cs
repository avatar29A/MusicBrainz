﻿
namespace Hqub.MusicBrainz
{
    using Hqub.MusicBrainz.Entities;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Helper for building MusicBrainz query strings.
    /// </summary>
    /// <typeparam name="T">Any supported MusicBrainz entity implementing the <see cref="IEntity"/> interface.</typeparam>
    /// <remarks>
    /// See https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2/Search
    /// </remarks>
    public class QueryParameters<T> : IEnumerable<QueryParameters<T>.Node>
         where T : IEntity
    {
        private readonly List<Node> nodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParameters{T}"/> class.
        /// </summary>
        public QueryParameters()
        {
            nodes = new List<Node>();
        }

        /// <summary>
        /// Add a field to the query parameters.
        /// </summary>
        /// <param name="key">The field key.</param>
        /// <param name="value">The field value.</param>
        /// <param name="negate">Negate the field (will result in 'AND NOT key:value')</param>
        public void Add(string key, string value, bool negate = false)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(Resources.Messages.EmptySearchKey, nameof(key));
            }

            if (!Validate(key))
            {
                throw new ArgumentException(string.Format(Resources.Messages.UnsupportedSearchField, key), nameof(key));
            }

            nodes.Add(new Node(key, value, negate));
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        public IEnumerator<Node> GetEnumerator()
        {
            return nodes.GetEnumerator();
        }

        /// <summary>
        /// Returns the query string that represents the current object.
        /// </summary>
        public override string ToString()
        {
            return BuildQueryString();
        }

        private string BuildQueryString()
        {
            var sb = new StringBuilder();
            
            foreach (var item in nodes)
            {
                // Append operator.
                if (sb.Length > 0)
                {
                    sb.Append(" AND ");
                }

                // Negate operator.
                if (item.Negate)
                {
                    sb.Append("NOT ");
                }

                // Append key.
                sb.Append(item.Key);
                sb.Append(':');

                // Append value.
                string value = item.Value;

                if (value.Contains("AND") || value.Contains("OR"))
                {
                    if (!value.StartsWith("("))
                    {
                        // The search value appears to be an expression, so enclose it in brackets.
                        sb.Append("(" + value + ")");
                    }
                    else
                    {
                        sb.Append(value);
                    }
                }
                else if (value.Contains(" ") && !value.StartsWith("\""))
                {
                    // The search value contains whitespace but isn't quoted.
                    sb.Append("\"" + value + "\"");
                }
                else
                {
                    // The search value is already quoted or doesn't need quoting, so just append it.
                    sb.AppendFormat(value);
                }
            }

            return sb.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return nodes.GetEnumerator();
        }

        private static bool Validate(string key)
        {
            key = "-" + key + "-";

            if (typeof(T) == typeof(Artist))
            {
                return Resources.Constants.ArtistQueryParams.Contains(key);
            }

            if (typeof(T) == typeof(Recording))
            {
                return Resources.Constants.RecordingQueryParams.Contains(key);
            }

            if (typeof(T) == typeof(Release))
            {
                return Resources.Constants.ReleaseQueryParams.Contains(key);
            }

            if (typeof(T) == typeof(ReleaseGroup))
            {
                return Resources.Constants.ReleaseGroupQueryParams.Contains(key);
            }

            if (typeof(T) == typeof(Label))
            {
                return Resources.Constants.LabelQueryParams.Contains(key);
            }

            return false;
        }

        /// <summary>
        /// Represents a node of the search query.
        /// </summary>
        public class Node
        {
            /// <summary>
            /// Gets the key of the node.
            /// </summary>
            public string Key { get; private set; }

            /// <summary>
            /// Gets the value of the node.
            /// </summary>
            public string Value { get; private set; }

            /// <summary>
            /// Gets a value indicating whether the node should be negated.
            /// </summary>
            public bool Negate { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Node"/> class.
            /// </summary>
            public Node(string key, string value, bool negate)
            {
                Key = key;
                Value = value;
                Negate = negate;
            }
        }
    }
}
