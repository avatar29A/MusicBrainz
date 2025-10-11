namespace Hqub.MusicBrainz.Entities
{
    /// <summary>
    /// Represents an entity in the MusicBrainz database with a unique id.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets the MusicBrainz id.
        /// </summary>
        public string Id { get; set; }
    }
}
