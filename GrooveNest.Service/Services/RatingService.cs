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



        // -------------------------------------------------------------------------- //
        // ------------------------ UpdateRatingAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<RatingResponseDto>> UpdateRatingAsync(int ratingId, RatingUpdateDto ratingUpdateDto)
        {
            // Check if the rating exists
            var existingRating = await _ratingRepository.GetByIdAsync(ratingId);
            if (existingRating == null)
            {
                return ApiResponse<RatingResponseDto>.ErrorResponse("Rating not found.");
            }

            // Update the rating properties
            existingRating.Stars = ratingUpdateDto.Stars;

            // Save the updated rating to the repository
            await _ratingRepository.UpdateAsync(existingRating);

            // Map the updated rating to a response DTO
            var responseDto = new RatingResponseDto
            {
                Id = existingRating.Id,
                Stars = existingRating.Stars,
                CreatedAt = existingRating.CreatedAt,
                TrackId = existingRating.TrackId,
                UserId = existingRating.UserId
            };

            // Return a success response with the updated rating
            return ApiResponse<RatingResponseDto>.SuccessResponse(responseDto, "Rating updated successfully.");
        }



        // --------------------------------------------------------------------------------------- //
        // ------------------------ GetAverageRatingByTrackIdAsync METHODS ----------------------- //
        // --------------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<double>> GetAverageRatingByTrackIdAsync(Guid trackId)
        {
            // Check if the track exists
            var track = await _trackRepository.GetByIdAsync(trackId);
            if (track == null)
            {
                return ApiResponse<double>.ErrorResponse("Track not found.");
            }
            // Get the average rating for the track
            var averageRating = await _ratingRepository.GetAverageRatingByTrackIdAsync(trackId);
            if (averageRating == 0)
            {
                return ApiResponse<double>.SuccessResponse(averageRating, "No ratings found for this track.");
            }

            // Return a success response with the average rating
            return ApiResponse<double>.SuccessResponse(averageRating, "Average rating retrieved successfully.");
        }



        // -------------------------------------------------------------------------- //
        // ------------------------ DeleteRatingAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<bool>> DeleteRatingAsync(int ratingId)
        {
            // Check if the rating exists
            var existingRating = await _ratingRepository.GetByIdAsync(ratingId);
            if (existingRating == null)
            {
                return ApiResponse<bool>.ErrorResponse("Rating not found.");
            }
            // Delete the rating from the repository
            await _ratingRepository.DeleteAsync(existingRating);
            // Return a success response indicating the rating was deleted
            return ApiResponse<bool>.SuccessResponse(true, "Rating deleted successfully.");
        }
    }
}
