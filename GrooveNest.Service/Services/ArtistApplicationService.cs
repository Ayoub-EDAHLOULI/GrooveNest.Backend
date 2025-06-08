using GrooveNest.Domain.DTOs.ArtistApplicationDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class ArtistApplicationService(IArtistApplicationRepository artistApplicationRepository, IUserService userService) : IArtistApplicationService
    {
        private readonly IArtistApplicationRepository _artistApplicationRepository = artistApplicationRepository;
        private readonly IUserService _userService = userService;



        // ------------------------------------------------------------------------------------------- //
        // ------------------------ GetPaginatedArtistApplicationAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<object>> GetAllPaginatedArtistApplicationsAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var artistApplications = await _artistApplicationRepository.GetAllAsync();
            if (artistApplications == null || !artistApplications.Any())
            {
                return ApiResponse<object>.ErrorResponse("No artist applications found.");
            }

            var usersResponse = await _userService.GetAllUsersAsync();
            var users = usersResponse.Data ?? [];

            // Filter by search query if provided
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                artistApplications = artistApplications
                    .Where(app =>
                        (!string.IsNullOrEmpty(app.StageName) && app.StageName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) ||
                        users.Any(u =>
                            u.Id == app.UserId && (
                                (!string.IsNullOrEmpty(u.UserName) && u.UserName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)) ||
                                u.Id.ToString().Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                            )
                        )
                    )
                    .ToList();
            }

            // Pagination
            var totalItems = artistApplications.Count();
            var pagedApplications = artistApplications
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var artistApplicationDtos = new List<ArtistApplicationResponseDto>();
            foreach (var app in pagedApplications)
            {
                var user = users.FirstOrDefault(u => u.Id == app.UserId);
                var userName = user?.UserName ?? "Unknown User";
                var email = user?.Email ?? "Unknown Email";

                var socialLinks = new List<string>();
                if (!string.IsNullOrWhiteSpace(app.InstagramUrl)) socialLinks.Add(app.InstagramUrl);
                if (!string.IsNullOrWhiteSpace(app.TwitterUrl)) socialLinks.Add(app.TwitterUrl);
                if (!string.IsNullOrWhiteSpace(app.YouTubeUrl)) socialLinks.Add(app.YouTubeUrl);

                artistApplicationDtos.Add(new ArtistApplicationResponseDto
                {
                    Id = app.Id,
                    UserId = app.UserId,
                    UserName = userName,
                    Email = email,
                    StageName = app.StageName,
                    ArtistBio = app.ArtistBio,
                    MusicGenres = app.MusicGenres ?? [],
                    SampleTrackLinks = app.SampleTrackLinks ?? [],
                    SubmittedAt = app.SubmittedAt,
                    IsApproved = app.IsApproved,

                    // You will need to update the DTO to accept SocialLinks
                    SocialLinks = socialLinks
                });
            }

            var result = new
            {
                PaginatedArtistApplication = artistApplicationDtos,
                TotalArtistApplication = totalItems
            };

            return ApiResponse<object>.SuccessResponse(result, "Paginated artist applications retrieved successfully.");
        }




        // -------------------------------------------------------------------------------------- //
        // ------------------------ CreateArtistApplicationAsync METHODS ------------------------ //
        // -------------------------------------------------------------------------------------- //
        public async Task<ApiResponse<ArtistApplicationResponseDto>> CreateArtistApplicationAsync(ArtistApplicationCreateDto artistApplicationCreateDto)
        {
            // Validate the input DTO
            if (artistApplicationCreateDto == null || artistApplicationCreateDto.UserId == Guid.Empty)
            {
                return ApiResponse<ArtistApplicationResponseDto>.ErrorResponse("Invalid artist application data.");
            }

            // Check if the user exists
            var userResponse = await _userService.GetUserByIdAsync(artistApplicationCreateDto.UserId);
            if (userResponse.Data == null)
            {
                return ApiResponse<ArtistApplicationResponseDto>.ErrorResponse("User not found.");
            }

            // Create the artist application
            var artistApplication = new ArtistApplication
            {
                UserId = artistApplicationCreateDto.UserId,
                StageName = StringValidator.TrimOrEmpty(artistApplicationCreateDto.StageName),
                ArtistBio = StringValidator.TrimOrEmpty(artistApplicationCreateDto.ArtistBio),
                MusicGenres = artistApplicationCreateDto.MusicGenres ?? [],
                SampleTrackLinks = artistApplicationCreateDto.SampleTrackLinks ?? [],
                InstagramUrl = artistApplicationCreateDto.InstagramUrl,
                TwitterUrl = artistApplicationCreateDto.TwitterUrl,
                YouTubeUrl = artistApplicationCreateDto.YouTubeUrl,
                SubmittedAt = DateTime.UtcNow,
                IsApproved = false // Default to false until approved
            };

            await _artistApplicationRepository.AddAsync(artistApplication);

            var socialLinks = new List<string>();
            if (!string.IsNullOrWhiteSpace(artistApplication.InstagramUrl)) socialLinks.Add(artistApplication.InstagramUrl);
            if (!string.IsNullOrWhiteSpace(artistApplication.TwitterUrl)) socialLinks.Add(artistApplication.TwitterUrl);
            if (!string.IsNullOrWhiteSpace(artistApplication.YouTubeUrl)) socialLinks.Add(artistApplication.YouTubeUrl);

            // Create the response DTO
            var artistApplicationResponseDto = new ArtistApplicationResponseDto
            {
                Id = artistApplication.Id,
                UserId = artistApplication.UserId,
                UserName = userResponse.Data.UserName,
                StageName = artistApplication.StageName,
                ArtistBio = artistApplication.ArtistBio,
                MusicGenres = artistApplication.MusicGenres,
                SampleTrackLinks = artistApplication.SampleTrackLinks,
                SocialLinks = socialLinks,
                SubmittedAt = artistApplication.SubmittedAt,
                IsApproved = artistApplication.IsApproved
            };

            return ApiResponse<ArtistApplicationResponseDto>.SuccessResponse(
                artistApplicationResponseDto,
                "Artist application created successfully."
            );
        }



        // -------------------------------------------------------------------------------------- //
        // ------------------------ UpdateArtistApplicationAsync METHODS ------------------------ //
        // -------------------------------------------------------------------------------------- //
        public async Task<ApiResponse<ArtistApplicationResponseDto>> UpdateArtistApplicationAsync(Guid id, ArtistApplicationApprovalDto artistApplicationApprovalDto)
        {
            var artistApplication = await _artistApplicationRepository.GetByIdAsync(id);
            if (artistApplication == null)
            {
                return ApiResponse<ArtistApplicationResponseDto>.ErrorResponse("Artist application not found.");
            }

            // Update status and dateReviewed
            artistApplication.IsApproved = artistApplicationApprovalDto.Approve;
            artistApplication.DateReviewed = DateTime.UtcNow;
            artistApplication.Status = artistApplicationApprovalDto.Approve ? "Approved" : "Rejected";
            artistApplication.RejectionReason = artistApplicationApprovalDto.Approve ? null : artistApplicationApprovalDto.RejectionReason;

            await _artistApplicationRepository.UpdateAsync(artistApplication);

            // Get user info
            var user = await _userService.GetUserByIdAsync(artistApplication.UserId);
            var userName = user.Data?.UserName ?? "Unknown User";
            var email = user.Data?.Email ?? "Unknown Email";

            // Prepare social links
            var socialLinks = new List<string>();
            if (!string.IsNullOrWhiteSpace(artistApplication.InstagramUrl)) socialLinks.Add(artistApplication.InstagramUrl);
            if (!string.IsNullOrWhiteSpace(artistApplication.TwitterUrl)) socialLinks.Add(artistApplication.TwitterUrl);
            if (!string.IsNullOrWhiteSpace(artistApplication.YouTubeUrl)) socialLinks.Add(artistApplication.YouTubeUrl);

            var artistApplicationResponseDto = new ArtistApplicationResponseDto
            {
                Id = artistApplication.Id,
                UserId = artistApplication.UserId,
                UserName = userName,
                Email = email,
                StageName = artistApplication.StageName,
                ArtistBio = artistApplication.ArtistBio,
                MusicGenres = artistApplication.MusicGenres ?? [],
                SampleTrackLinks = artistApplication.SampleTrackLinks ?? [],
                SubmittedAt = artistApplication.SubmittedAt,
                IsApproved = artistApplication.IsApproved,
                Status = artistApplication.Status,
                RejectionReason = artistApplication.RejectionReason,
                DateReviewed = artistApplication.DateReviewed,
                SocialLinks = socialLinks
            };

            return ApiResponse<ArtistApplicationResponseDto>.SuccessResponse(
                artistApplicationResponseDto,
                "Artist application updated successfully."
            );
        }




        // -------------------------------------------------------------------------------------- //
        // ------------------------ DeleteArtistApplicationAsync METHODS ------------------------ //
        // -------------------------------------------------------------------------------------- //
        public async Task<ApiResponse<string>> DeleteArtistApplicationAsync(Guid id)
        {
            // Get the existing artist application
            var artistApplication = await _artistApplicationRepository.GetByIdAsync(id);
            if (artistApplication == null)
            {
                return ApiResponse<string>.ErrorResponse("Artist application not found.");
            }
            // Delete the artist application
            await _artistApplicationRepository.DeleteAsync(artistApplication);
            return ApiResponse<string>.SuccessResponse(string.Empty, "Artist application deleted successfully.");
        }
    }
}
