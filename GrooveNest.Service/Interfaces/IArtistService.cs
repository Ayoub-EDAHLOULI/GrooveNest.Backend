using GrooveNest.Domain.DTOs.ArtistDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IArtistService
    {
        Task<ApiResponse<List<ArtistResponseDto>>> GetAllArtistsAsync();
        Task<ApiResponse<object>> GetAllPaginatedArtistsAsync(int page = 1, int pageSize = 10, string searchQuery = "");
        Task<ApiResponse<ArtistResponseDto>> GetArtistByIdAsync(Guid id);
        Task<ApiResponse<ArtistResponseDto>> CreateArtistAsync(ArtistCreateDto artistCreateDto);
        Task<ApiResponse<ArtistResponseDto>> UpdateArtistAsync(Guid id, ArtistUpdateDto artistUpdateDto);
        Task<ApiResponse<string>> DeleteArtistAsync(Guid id);
    }
}
