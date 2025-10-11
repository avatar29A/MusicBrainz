namespace Hqub.MusicBrainz.Services
{
    using Hqub.MusicBrainz.Entities;

    /// <summary>
    /// Interface defining the label service.
    /// </summary>
    public interface ILabelService : ILookupService<Label>, ISearchService<Label>, IBrowseService<Label>
    {
    }
}
