using GrooveNest.Domain.DTOs.ArtistApplicationDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IArtistApplicationService
    {
        Task<ApiResponse<List<ArtistApplicationResponseDto>>> GetAllArtistApplicationsAsync();
        Task<ApiResponse<object>> GetAllPaginatedArtistApplicationsAsync(int page = 1, int pageSize = 10, string searchQuery = "");
        Task<ApiResponse<ArtistApplicationResponseDto>> GetArtistApplicationByIdAsync(Guid id);
        Task<ApiResponse<ArtistApplicationResponseDto>> CreateArtistApplicationAsync(ArtistApplicationCreateDto artistApplicationCreateDto);
        Task<ApiResponse<ArtistApplicationResponseDto>> UpdateArtistApplicationAsync(Guid id, ArtistApplicationApprovalDto artistApplicationApprovalDto);
        Task<ApiResponse<string>> DeleteArtistApplicationAsync(Guid id);
    }
}
