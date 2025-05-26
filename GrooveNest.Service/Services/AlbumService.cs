using GrooveNest.Domain.DTOs.AlbumDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Domain.Validators;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;
using Microsoft.AspNetCore.Http;

namespace GrooveNest.Service.Services
{
    public class AlbumService(IAlbumRepository albumRepository, IArtistRepository artistRepository) : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository = albumRepository;
        private readonly IArtistRepository _artistRepository = artistRepository;

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



        // ------------------------------------------------------------------------- //
        // ------------------------ CreateAlbumAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<AlbumResponseDto>> CreateAlbumAsync(AlbumCreateDto albumCreateDto)
        {
            // Validate the inputs
            var validationResponse = AlbumValidator.ValidateCreate(albumCreateDto);
            if (validationResponse != null)
            {
                return ApiResponse<AlbumResponseDto>.ErrorResponse(validationResponse.Message);
            }

            // Check if the artist exists
            var artist = await _artistRepository.GetByIdAsync(albumCreateDto.ArtistId);
            if (artist == null)
            {
                return ApiResponse<AlbumResponseDto>.ErrorResponse("Artist not found.");
            }

            // Validate and save cover file if provided
            string? coverUrl = null;
            if (albumCreateDto.Cover != null)
            {
                if (albumCreateDto.Cover.Length > 5 * 1024 * 1024)
                {
                    return ApiResponse<AlbumResponseDto>.ErrorResponse("Cover file size exceeds the limit of 5 MB.");
                }

                if (!albumCreateDto.Cover.ContentType.StartsWith("image/"))
                {
                    return ApiResponse<AlbumResponseDto>.ErrorResponse("Cover must be an image file.");
                }

                coverUrl = await SaveCoverAsync(albumCreateDto.Cover); // You must implement this method
            }

            // Create the Album entity
            var album = new Album
            {
                Id = Guid.NewGuid(),
                Title = albumCreateDto.Title,
                ReleaseDate = albumCreateDto.ReleaseDate,
                CoverUrl = coverUrl,
                ArtistId = albumCreateDto.ArtistId
            };

            await _albumRepository.AddAsync(album);

            // Prepare the response DTO
            var albumDto = new AlbumResponseDto
            {
                Id = album.Id,
                Title = album.Title,
                ReleaseDate = album.ReleaseDate,
                CoverUrl = album.CoverUrl,
                ArtistId = album.ArtistId,
                ArtistName = artist.Name
            };

            return ApiResponse<AlbumResponseDto>.SuccessResponse(albumDto, "Album created successfully.");
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


        private static async Task<string?> SaveCoverAsync(IFormFile coverFile)
        {
            if (coverFile == null || coverFile.Length == 0)
                return null; // No cover file provided

            // Example saving logic
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(coverFile.FileName)}";
            var filePath = Path.Combine("wwwroot", "uploads", "covers", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!); // Ensure folder exists

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await coverFile.CopyToAsync(stream);
            }

            return $"/uploads/covers/{fileName}"; // return relative URL for access
        }

    }
}
