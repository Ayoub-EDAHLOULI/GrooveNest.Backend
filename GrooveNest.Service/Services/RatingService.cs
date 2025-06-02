using GrooveNest.Domain.DTOs.RatingDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class RatingService(IRatingRepository ratingRepository, IUserRepository userRepository, ITrackRepository trackRepository) : IRatingService
    {
        private readonly IRatingRepository _ratingRepository = ratingRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITrackRepository _trackRepository = trackRepository;


        // -------------------------------------------------------------------------- //
        // ------------------------ CreateRatingAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<string>> CreateRatingAsync(RatingCreateDto ratingCreateDto)
        {
            // Check if the user exists
            var user = await _userRepository.GetByIdAsync(ratingCreateDto.UserId);
            if (user == null)
            {
                return ApiResponse<string>.ErrorResponse("User not found.");
            }

            // Check if the track exists
            var track = await _trackRepository.GetByIdAsync(ratingCreateDto.TrackId);
            if (track == null)
            {
                return ApiResponse<string>.ErrorResponse("Track not found.");
            }

            // Check if the user has already rated the track
            var existingRating = await _ratingRepository.GetByTrackIdAndUserIdAsync(ratingCreateDto.TrackId, ratingCreateDto.UserId);
            if (existingRating != null)
            {
                return ApiResponse<string>.ErrorResponse("You have already rated this track.");
            }

            // Create a new rating entity
            var rating = new Rating
            {
                Stars = ratingCreateDto.Stars,
                CreatedAt = DateTime.UtcNow,
                TrackId = ratingCreateDto.TrackId,
                UserId = ratingCreateDto.UserId
            };

            // Save the rating to the repository
            await _ratingRepository.AddAsync(rating);

            // Return a success response
            return ApiResponse<string>.SuccessResponse(string.Empty, "Rating created successfully.");
        }

        public Task<ApiResponse<bool>> DeleteRatingAsync(int ratingId)
        {
            throw new NotImplementedException();
        }

        public Task<double> GetAverageRatingByTrackIdAsync(Guid trackId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<RatingResponseDto>> UpdateRatingAsync(int ratingId, RatingUpdateDto ratingUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
