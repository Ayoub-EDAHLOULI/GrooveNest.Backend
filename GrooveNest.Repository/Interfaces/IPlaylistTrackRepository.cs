using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IPlaylistTrackRepository : IGenericRepository<PlaylistTrack, Guid>
    {
        Task<List<PlaylistTrack>> GetTracksByPlaylistIdWithTrackAsync(Guid playlistId);
        Task<PlaylistTrack?> GetByPlaylistAndTrackAsync(Guid playlistId, Guid trackId);
        Task<int> GetNextTrackNumberAsync(Guid playlistId);
    }
}
