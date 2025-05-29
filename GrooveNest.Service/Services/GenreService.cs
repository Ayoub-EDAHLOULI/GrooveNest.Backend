using GrooveNest.Domain.DTOs.GenreDTOs;
using GrooveNest.Domain.Entities;
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


        // ----------------------------------------------------------------------------------- //
        // ------------------------ GetAllPaginatedGenresAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------------- // 
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


        // -------------------------------------------------------------------------- //
        // ------------------------ GetGenreByIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<GenreResponseDto>> GetGenreByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
            {
                return ApiResponse<GenreResponseDto>.ErrorResponse("Genre not found.");
            }
            var genreDto = new GenreResponseDto
            {
                Id = genre.Id,
                Name = genre.Name,
            };
            return ApiResponse<GenreResponseDto>.SuccessResponse(genreDto, "Genre retrieved successfully.");
        }


        // ------------------------------------------------------------------------- //
        // ------------------------ CreateGenreAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<GenreResponseDto>> CreateGenreAsync(GenreCreateDto genreCreateDto)
        {
            if (string.IsNullOrWhiteSpace(genreCreateDto.Name))
            {
                return ApiResponse<GenreResponseDto>.ErrorResponse("Genre name cannot be empty.");
            }

            var trimmedName = StringValidator.TrimOrEmpty(genreCreateDto.Name);

            var existingGenre = await _genreRepository.GetGenreByName(trimmedName);
            if (existingGenre != null)
            {
                return ApiResponse<GenreResponseDto>.ErrorResponse("Genre already exists.");
            }
            var newGenre = new Genre
            {
                Name = trimmedName
            };
            await _genreRepository.AddAsync(newGenre);
            var genreDto = new GenreResponseDto
            {
                Id = newGenre.Id,
                Name = newGenre.Name,
            };
            return ApiResponse<GenreResponseDto>.SuccessResponse(genreDto, "Genre created successfully.");
        }

        public Task<ApiResponse<string>> DeleteGenreAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<GenreResponseDto>> UpdateGenreAsync(int id, GenreUpdateDto genreUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
