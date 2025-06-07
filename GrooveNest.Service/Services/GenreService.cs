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
                TrackCount = g.TrackGenres?.Count ?? 0
            }).ToList();

            return ApiResponse<List<GenreResponseDto>>.SuccessResponse(genreDtos, "Genres retrieved successfully.");
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ GetGenreByIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<GenreResponseDto>> GetGenreByIdAsync(Guid id)
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


        // ------------------------------------------------------------------------- //
        // ------------------------ UpdateGenreAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<GenreResponseDto>> UpdateGenreAsync(Guid id, GenreUpdateDto genreUpdateDto)
        {
            // Check if the genre exists
            var existingGenre = await _genreRepository.GetByIdAsync(id);
            if (existingGenre == null)
            {
                return ApiResponse<GenreResponseDto>.ErrorResponse("Genre not found.");
            }

            // Update the name if provided
            if (!string.IsNullOrWhiteSpace(genreUpdateDto.Name))
            {
                var trimmedName = StringValidator.TrimOrEmpty(genreUpdateDto.Name);
                if (trimmedName != existingGenre.Name)
                {
                    var genreWithSameName = await _genreRepository.GetGenreByName(trimmedName);
                    if (genreWithSameName != null)
                    {
                        return ApiResponse<GenreResponseDto>.ErrorResponse("Genre with the same name already exists.");
                    }
                    existingGenre.Name = trimmedName;
                }
            }

            // Save the updated genre
            await _genreRepository.UpdateAsync(existingGenre);

            var genreDto = new GenreResponseDto
            {
                Id = existingGenre.Id,
                Name = existingGenre.Name,
            };

            return ApiResponse<GenreResponseDto>.SuccessResponse(genreDto, "Genre updated successfully.");
        }


        // ------------------------------------------------------------------------- //
        // ------------------------ DeleteGenreAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<string>> DeleteGenreAsync(Guid id)
        {
            // Check if the genre exists
            var existingGenre = await _genreRepository.GetByIdAsync(id);
            if (existingGenre == null)
            {
                return ApiResponse<string>.ErrorResponse("Genre not found.");
            }
            // Delete the genre
            await _genreRepository.DeleteAsync(existingGenre);
            return ApiResponse<string>.SuccessResponse(string.Empty, "Genre deleted successfully.");
        }
    }
}
