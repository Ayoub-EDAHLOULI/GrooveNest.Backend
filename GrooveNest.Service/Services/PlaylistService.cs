using GrooveNest.Domain.DTOs.PlaylistDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class PlaylistService(IPlaylistRepository playlistRepository) : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository = playlistRepository;


        // ------------------------------------------------------------------------- //
        // ------------------------ GetAllAlbumAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- //
        public async Task<ApiResponse<List<PlaylistResponseDto>>> GetAllPlaylistsAsync()
        {
            var playlists = await _playlistRepository.GetAllAsync();
            if (playlists == null || !playlists.Any())
            {
                return ApiResponse<List<PlaylistResponseDto>>.ErrorResponse("No playlists found.");
            }

            var playlistDtos = playlists.Select(playlist => new PlaylistResponseDto
            {
                Id = playlist.Id,
                Name = playlist.Name,
                IsPublic = playlist.IsPublic,
                CreatedAt = playlist.CreatedAt,
                OwnerId = playlist.OwnerId,
                OwnerUserName = playlist.Owner?.UserName ?? "Unknown User"
            }).ToList();

            return ApiResponse<List<PlaylistResponseDto>>.SuccessResponse(playlistDtos, "Playlists retrieved successfully.");
        }



        public Task<ApiResponse<PlaylistResponseDto>> CreatePlaylistAsync(PlaylistCreateDto playlistCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> DeletePlaylistAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<object>> GetAllPaginatedPlaylistsAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PlaylistResponseDto>> GetPlaylistByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<PlaylistResponseDto>> UpdatePlaylistAsync(Guid id, PlaylistUpdateDto playlistUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
