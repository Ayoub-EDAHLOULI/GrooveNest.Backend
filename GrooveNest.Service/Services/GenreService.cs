using GrooveNest.Domain.DTOs.GenreDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class GenreService(IGenreRepository genreRepository) : IGenreService
    {
        private readonly IGenreRepository _genreRepository = genreRepository;

        public Task<ApiResponse<GenreResponseDto>> CreateGenreAsync(GenreCreateDto genreCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> DeleteGenreAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<GenreResponseDto>>> GetAllGenresAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<object>> GetAllPaginatedGenresAsync(int page = 1, int pageSize = 10, string searchQuery = "")
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
