using GrooveNest.Domain.DTOs.GenreDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IGenreService
    {
        Task<ApiResponse<List<GenreResponseDto>>> GetAllGenresAsync();
        Task<ApiResponse<object>> GetAllPaginatedGenresAsync(int page = 1, int pageSize = 10, string searchQuery = "");
        Task<ApiResponse<GenreResponseDto>> GetGenreByIdAsync(Guid id);
        Task<ApiResponse<GenreResponseDto>> CreateGenreAsync(GenreCreateDto genreCreateDto);
        Task<ApiResponse<GenreResponseDto>> UpdateGenreAsync(Guid id, GenreUpdateDto genreUpdateDto);
        Task<ApiResponse<string>> DeleteGenreAsync(Guid id);
    }
}
