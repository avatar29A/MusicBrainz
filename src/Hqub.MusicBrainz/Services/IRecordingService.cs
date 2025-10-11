namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;

    /// <summary>
    /// Interface defining the recording service.
    /// </summary>
    public interface IRecordingService : ILookupService<Recording>, ISearchService<Recording>, IBrowseService<Recording>
    {
    }
}
