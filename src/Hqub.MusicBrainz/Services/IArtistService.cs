namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;

    /// <summary>
    /// Interface defining the artist service.
    /// </summary>
    public interface IArtistService : ILookupService<Artist>, ISearchService<Artist>, IBrowseService<Artist>
    {
    }
}
