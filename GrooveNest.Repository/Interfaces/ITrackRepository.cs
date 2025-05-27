using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface ITrackRepository : IGenericRepository<Track, Guid>
    {
        Task<Track> GetTrackByTitleAsync(string title);
        Task<IEnumerable<Track>> GetTracksByArtistIdAsync(Guid artistId);
        Task<IEnumerable<Track>> GetTracksByAlbumIdAsync(Guid albumId);
        Task<IEnumerable<Track>> GetTracksByArtistNameAsync(string artistName);
        Task<IEnumerable<Track>> GetTracksByAlbumTitleAsync(string albumTitle);
    }
}
