using GrooveNest.Domain.DTOs.GenreDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class GenreService(IGenreRepository genreRepository) : IGenreService
    {
        private readonly IGenreRepository _genreRepository = genreRepository;


        // -------------------------------------------------------------------------- //
        // ------------------------ GetAllGenresAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<List<GenreResponseDto>>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            if (genres == null || !genres.Any())
            {
                return ApiResponse<List<GenreResponseDto>>.ErrorResponse("No genres found.");
            }

            var genreDtos = genres.Select(g => new GenreResponseDto
            {
                Id = g.Id,
                Name = g.Name,
            }).ToList();

            return ApiResponse<List<GenreResponseDto>>.SuccessResponse(genreDtos, "Genres retrieved successfully.");
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ GetAllGenresAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<object>> GetAllPaginatedGenresAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var genres = await _genreRepository.GetAllAsync();
            if (genres == null || !genres.Any())
            {
                return ApiResponse<object>.ErrorResponse("No genres found.");
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                genres = genres.Where(g => g.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var totalCount = genres.Count();
            var paginatedGenres = genres
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(g => new GenreResponseDto
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .ToList();

            var response = new
            {
                PaginatedGenres = paginatedGenres,
                GenreCount = totalCount
            };

            return ApiResponse<object>.SuccessResponse(response, "Genres retrieved successfully.");
        }

        public Task<ApiResponse<GenreResponseDto>> CreateGenreAsync(GenreCreateDto genreCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> DeleteGenreAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<GenreResponseDto>> GetGenreByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<GenreResponseDto>> UpdateGenreAsync(Guid id, GenreUpdateDto genreUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
