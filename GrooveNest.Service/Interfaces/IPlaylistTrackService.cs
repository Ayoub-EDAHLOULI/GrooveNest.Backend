using GrooveNest.Domain.Entities;

namespace GrooveNest.Service.Interfaces
{
    public interface IPlaylistTrackService
    {
        Task<List<PlaylistTrack>> GetTracksByPlaylistIdAsync(Guid playlistId);
        Task<PlaylistTrack?> GetByPlaylistAndTrackAsync(Guid playlistId, Guid trackId);
        Task<PlaylistTrack> AddTrackToPlaylistAsync(Guid playlistId, Guid trackId);
        Task<PlaylistTrack> RemoveTrackFromPlaylistAsync(Guid playlistId, Guid trackId);
    }
}
