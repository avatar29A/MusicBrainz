
namespace Hqub.MusicBrainz.API.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    partial class Work
    {
        #region Static Methods

        /// <summary>
        /// Lookup a work in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The work MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        [Obsolete("Use MusicBrainzClient instead of static API.")]
        public static async Task<Work> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            var client = new MusicBrainzClient(Configuration.Proxy)
            {
                Cache = Configuration.Cache
            };

            return await client.Work.GetAsync(id, inc);
        }

        #endregion
    }
}