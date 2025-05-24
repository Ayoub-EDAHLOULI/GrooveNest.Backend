using GrooveNest.Domain.DTOs.ArtistApplicationDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class ArtistApplicationService(IArtistApplicationRepository artistApplicationRepository) : IArtistApplicationService
    {
        private readonly IArtistApplicationRepository _artistApplicationRepository = artistApplicationRepository;

        public Task<ApiResponse<ArtistApplicationResponseDto>> CreateArtistApplicationAsync(ArtistApplicationCreateDto artistApplicationCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> DeleteArtistApplicationAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<ArtistApplicationResponseDto>>> GetAllArtistApplicationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<object>> GetAllPaginatedArtistApplicationsAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ArtistApplicationResponseDto>> GetArtistApplicationByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<ArtistApplicationResponseDto>> UpdateArtistApplicationAsync(Guid id, ArtistApplicationApprovalDto artistApplicationApprovalDto)
        {
            throw new NotImplementedException();
        }
    }
}
