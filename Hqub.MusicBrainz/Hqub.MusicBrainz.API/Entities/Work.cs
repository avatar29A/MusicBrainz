
namespace Hqub.MusicBrainz.API.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    /// <summary>
    /// In MusicBrainz terminology, a work is a distinct intellectual or artistic creation,
    /// which can be expressed in the form of one or more audio recordings.
    /// </summary>
    /// <see href="https://musicbrainz.org/doc/Work"/>
    [DataContract(Name = "work")]
    public class Work
    {
        public const string EntityName = "work";

        #region Properties

        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        [DataMember(Name = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the ISW code.
        /// </summary>
        [DataMember(Name = "iswc")]
        public string ISWC { get; set; }

        /// <summary>
        /// Gets or sets the disambiguation.
        /// </summary>
        [DataMember(Name = "disambiguation")]
        public string Disambiguation { get; set; }

        #endregion

        #region Subqueries

        /// <summary>
        /// Gets or sets a list of relations associated to this work.
        /// </summary>
        /// <example>
        /// var e = await Work.GetAsync(mbid, "url-rels");
        /// </example>
        [DataMember(Name = "relations")]
        public List<Relation> Relations { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Lookup a work in the MusicBrainz database.
        /// </summary>
        /// <param name="id">The work MusicBrainz id.</param>
        /// <param name="inc">A list of entities to include (subqueries).</param>
        /// <returns></returns>
        public static async Task<Work> GetAsync(string id, params string[] inc)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException(string.Format(Resources.Messages.MissingParameter, "id"));
            }

            string url = WebServiceHelper.CreateLookupUrl(EntityName, id, inc);

            return await WebServiceHelper.GetAsync<Work>(url);
        }

        #endregion
    }
}
