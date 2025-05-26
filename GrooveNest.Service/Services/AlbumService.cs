using GrooveNest.Domain.DTOs.AlbumDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class AlbumService(IAlbumRepository albumRepository) : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository = albumRepository;

        // ------------------------------------------------------------------------- //
        // ------------------------ GetAllAlbumAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<List<AlbumResponseDto>>> GetAllAlbumAsync()
        {
            var albums = await _albumRepository.GetAllAsync();
            if (albums == null || !albums.Any())
            {
                return ApiResponse<List<AlbumResponseDto>>.ErrorResponse("No albums found.");
            }

            var albumDtos = albums.Select(album => new AlbumResponseDto
            {
                Id = album.Id,
                Title = album.Title,
                ReleaseDate = album.ReleaseDate,
                CoverUrl = album.CoverUrl,
                ArtistId = album.ArtistId,
                ArtistName = album.Artist?.Name ?? "Unknown Artist"
            }).ToList();

            // Return response
            return ApiResponse<List<AlbumResponseDto>>.SuccessResponse(albumDtos, "Albums retrieved successfully.");
        }



        // ---------------------------------------------------------------------------------- //
        // ------------------------ GetAllPaginatedAlbumAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<object>> GetAllPaginatedAlbumAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var albums = await _albumRepository.GetAllAsync();
            if (albums == null || !albums.Any())
            {
                return ApiResponse<object>.ErrorResponse("No albums found.");
            }

            // Filter albums by search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                albums = [.. albums.Where(a => a.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))];
            }

            // Paginate albums
            var paginatedAlbums = albums
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(album => new AlbumResponseDto
                {
                    Id = album.Id,
                    Title = album.Title,
                    ReleaseDate = album.ReleaseDate,
                    CoverUrl = album.CoverUrl,
                    ArtistId = album.ArtistId,
                    ArtistName = album.Artist?.Name ?? "Unknown Artist"
                })
                .ToList();

            var totalAlbums = albums.Count();
            var response = new
            {
                PaginatedAlbums = paginatedAlbums,
                TotalAlbums = totalAlbums
            };

            // Return response
            return ApiResponse<object>.SuccessResponse(response, "Paginated albums retrieved successfully.");
        }


        public Task<ApiResponse<AlbumResponseDto>> CreateAlbumAsync(AlbumCreateDto albumCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> DeleteAlbumAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<AlbumResponseDto>> GetAlbumByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<AlbumResponseDto>> UpdateAlbumAsync(Guid id, AlbumUpdateDto albumUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
