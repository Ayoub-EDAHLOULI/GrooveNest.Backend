using GrooveNest.Domain.DTOs.PlaylistTrackDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IPlaylistTrackService
    {
        Task<ApiResponse<List<PlaylistTrackResponseDto>>> GetTracksByPlaylistIdAsync(Guid playlistId);
        Task<ApiResponse<PlaylistTrackResponseDto>> AddTrackToPlaylistAsync(Guid playlistId, Guid trackId);
        Task<ApiResponse<string>> RemoveTrackFromPlaylistAsync(Guid playlistId, Guid trackId);
    }
}
