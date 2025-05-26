using GrooveNest.Domain.DTOs.PlaylistDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IPlaylistService
    {
        Task<ApiResponse<List<PlaylistResponseDto>>> GetAllPlaylistsAsync();
        Task<ApiResponse<object>> GetAllPaginatedPlaylistsAsync(int page = 1, int pageSize = 10, string searchQuery = "");
        Task<ApiResponse<PlaylistResponseDto>> GetPlaylistByIdAsync(Guid id);
        Task<ApiResponse<PlaylistResponseDto>> CreatePlaylistAsync(PlaylistCreateDto playlistCreateDto);
        Task<ApiResponse<PlaylistResponseDto>> UpdatePlaylistAsync(Guid id, PlaylistUpdateDto playlistUpdateDto);
        Task<ApiResponse<string>> DeletePlaylistAsync(Guid id);
    }
}
