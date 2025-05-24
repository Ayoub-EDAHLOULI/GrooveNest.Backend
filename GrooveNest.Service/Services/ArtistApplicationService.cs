using GrooveNest.Domain.DTOs.ArtistApplicationDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class ArtistApplicationService(IArtistApplicationRepository artistApplicationRepository, IUserService userService) : IArtistApplicationService
    {
        private readonly IArtistApplicationRepository _artistApplicationRepository = artistApplicationRepository;
        private readonly IUserService _userService = userService;



        // ------------------------------------------------------------------------------------- //
        // ------------------------ GetAllArtistApplicationAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<List<ArtistApplicationResponseDto>>> GetAllArtistApplicationsAsync()
        {
            var artistApplications = await _artistApplicationRepository.GetAllAsync();

            if (artistApplications == null || !artistApplications.Any())
            {
                return ApiResponse<List<ArtistApplicationResponseDto>>.ErrorResponse("No artist applications found.");
            }

            var artistApplicationDtos = new List<ArtistApplicationResponseDto>();

            foreach (var app in artistApplications)
            {
                var user = await _userService.GetUserByIdAsync(app.UserId);
                var userName = user.Data?.UserName ?? "Unknown User";

                artistApplicationDtos.Add(new ArtistApplicationResponseDto
                {
                    Id = app.Id,
                    UserId = app.UserId,
                    UserName = userName,
                    Message = app.Message,
                    SubmittedAt = app.SubmittedAt,
                    IsApproved = app.IsApproved
                });
            }

            return ApiResponse<List<ArtistApplicationResponseDto>>.SuccessResponse(artistApplicationDtos, "Artist applications retrieved successfully.");
        }






        public Task<ApiResponse<ArtistApplicationResponseDto>> CreateArtistApplicationAsync(ArtistApplicationCreateDto artistApplicationCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> DeleteArtistApplicationAsync(Guid id)
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
