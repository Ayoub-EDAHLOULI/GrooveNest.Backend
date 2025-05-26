using GrooveNest.Domain.DTOs.PlaylistDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class PlaylistService(IPlaylistRepository playlistRepository, IUserRepository userRepository) : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository = playlistRepository;
        private readonly IUserRepository _userRepository = userRepository;


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


        // -------------------------------------------------------------------------------------- //
        // ------------------------ GetAllPaginatedPlaylistsAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------------------- //
        public async Task<ApiResponse<object>> GetAllPaginatedPlaylistsAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var playlists = await _playlistRepository.GetAllAsync();
            if (playlists == null || !playlists.Any())
            {
                return ApiResponse<object>.ErrorResponse("No playlists found.");
            }

            // Filter playlists by search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                playlists = [.. playlists.Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))];
            }

            // Paginate playlists
            var paginatedPlaylists = playlists
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(playlist => new PlaylistResponseDto
                {
                    Id = playlist.Id,
                    Name = playlist.Name,
                    IsPublic = playlist.IsPublic,
                    CreatedAt = playlist.CreatedAt,
                    OwnerId = playlist.OwnerId,
                    OwnerUserName = playlist.Owner?.UserName ?? "Unknown User"
                })
                .ToList();

            var totalPlaylists = playlists.Count();
            var response = new
            {
                PaginatedPlaylists = paginatedPlaylists,
                TotalPlaylists = totalPlaylists
            };

            return ApiResponse<object>.SuccessResponse(response, "Paginated playlists retrieved successfully.");
        }


        // ----------------------------------------------------------------------------- //
        // ------------------------ GetPlaylistByIdAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------- //
        public async Task<ApiResponse<PlaylistResponseDto>> GetPlaylistByIdAsync(Guid id)
        {
            var playlist = await _playlistRepository.GetByIdAsync(id);
            if (playlist == null)
            {
                return ApiResponse<PlaylistResponseDto>.ErrorResponse("Playlist not found.");
            }
            var playlistDto = new PlaylistResponseDto
            {
                Id = playlist.Id,
                Name = playlist.Name,
                IsPublic = playlist.IsPublic,
                CreatedAt = playlist.CreatedAt,
                OwnerId = playlist.OwnerId,
                OwnerUserName = playlist.Owner?.UserName ?? "Unknown User"
            };
            return ApiResponse<PlaylistResponseDto>.SuccessResponse(playlistDto, "Playlist retrieved successfully.");
        }


        // ---------------------------------------------------------------------------- //
        // ------------------------ CreatePlaylistAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------- //
        public async Task<ApiResponse<PlaylistResponseDto>> CreatePlaylistAsync(PlaylistCreateDto playlistCreateDto)
        {
            // Validate the input
            if (string.IsNullOrWhiteSpace(playlistCreateDto.Name) || playlistCreateDto.Name.Trim().Length < 3)
            {
                return ApiResponse<PlaylistResponseDto>.ErrorResponse("Playlist name must be at least 3 characters long.");
            }

            // Check if a playlist with the same name already exists
            var existingPlaylist = await _playlistRepository.GetPlaylistByName(playlistCreateDto.Name);
            if (existingPlaylist != null)
            {
                return ApiResponse<PlaylistResponseDto>.ErrorResponse("A playlist with this name already exists.");
            }

            // Check if owner exists
            var owner = await _userRepository.GetByIdAsync(playlistCreateDto.OwnerId);
            if (owner == null)
            {
                return ApiResponse<PlaylistResponseDto>.ErrorResponse("Playlist owner not found.");
            }

            // Create a new playlist entity
            var newPlaylist = new Domain.Entities.Playlist
            {
                Id = Guid.NewGuid(),
                Name = StringValidator.TrimOrEmpty(playlistCreateDto.Name),
                IsPublic = playlistCreateDto.IsPublic,
                CreatedAt = DateTime.UtcNow,
                OwnerId = playlistCreateDto.OwnerId
            };

            // Save the new playlist to the repository
            await _playlistRepository.AddAsync(newPlaylist);

            // Prepare response DTO
            var playlistDto = new PlaylistResponseDto
            {
                Id = newPlaylist.Id,
                Name = newPlaylist.Name,
                IsPublic = newPlaylist.IsPublic,
                CreatedAt = newPlaylist.CreatedAt,
                OwnerId = newPlaylist.OwnerId,
                OwnerUserName = owner.UserName
            };

            return ApiResponse<PlaylistResponseDto>.SuccessResponse(playlistDto, "Playlist created successfully.");
        }


        // ---------------------------------------------------------------------------- //
        // ------------------------ UpdatePlaylistAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------- //
        public async Task<ApiResponse<PlaylistResponseDto>> UpdatePlaylistAsync(Guid id, PlaylistUpdateDto playlistUpdateDto)
        {
            // Check if the playlist exists
            var existingPlaylist = await _playlistRepository.GetByIdAsync(id);
            if (existingPlaylist == null)
            {
                return ApiResponse<PlaylistResponseDto>.ErrorResponse("Playlist not found.");
            }

            // Update the name if provided
            if (!string.IsNullOrWhiteSpace(playlistUpdateDto.Name))
            {
                // Trimmed name
                var trimmedName = StringValidator.TrimOrEmpty(playlistUpdateDto.Name);

                // Validate the name length
                if (trimmedName.Length < 3)
                {
                    return ApiResponse<PlaylistResponseDto>.ErrorResponse("Playlist name must be at least 3 characters long.");
                }

                // Check if a playlist with the same name already exists
                var existingNamePlaylist = await _playlistRepository.GetPlaylistByName(trimmedName);
                if (existingNamePlaylist != null && existingNamePlaylist.Id != id)
                {
                    return ApiResponse<PlaylistResponseDto>.ErrorResponse("A playlist with this name already exists.");
                }

                // Update the name
                existingPlaylist.Name = trimmedName;
            }

            // Update the IsPublic property if provided
            if (playlistUpdateDto.IsPublic.HasValue)
            {
                existingPlaylist.IsPublic = playlistUpdateDto.IsPublic.Value;
            }

            // Save the updated playlist to the repository
            await _playlistRepository.UpdateAsync(existingPlaylist);

            // Prepare response DTO
            var playlistDto = new PlaylistResponseDto
            {
                Id = existingPlaylist.Id,
                Name = existingPlaylist.Name,
                IsPublic = existingPlaylist.IsPublic,
                CreatedAt = existingPlaylist.CreatedAt,
                OwnerId = existingPlaylist.OwnerId,
                OwnerUserName = existingPlaylist.Owner?.UserName ?? "Unknown User"
            };

            return ApiResponse<PlaylistResponseDto>.SuccessResponse(playlistDto, "Playlist updated successfully.");
        }


        // ---------------------------------------------------------------------------- //
        // ------------------------ DeletePlaylistAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------- //
        public async Task<ApiResponse<string>> DeletePlaylistAsync(Guid id)
        {
            // Check if the playlist exists
            var existingPlaylist = await _playlistRepository.GetByIdAsync(id);
            if (existingPlaylist == null)
            {
                return ApiResponse<string>.ErrorResponse("Playlist not found.");
            }
            // Delete the playlist from the repository
            await _playlistRepository.DeleteAsync(existingPlaylist);
            return ApiResponse<string>.SuccessResponse(string.Empty, "Playlist deleted successfully.");
        }
    }
}
