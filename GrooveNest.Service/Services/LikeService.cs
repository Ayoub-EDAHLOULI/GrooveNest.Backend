using GrooveNest.Domain.DTOs.LikeDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class LikeService(ILikeRepository likeRepository, IUserRepository userRepository, ITrackRepository trackRepository) : ILikeService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITrackRepository _trackRepository = trackRepository;
        private readonly ILikeRepository _likeRepository = likeRepository;


        // ------------------------------------------------------------------------ //
        // ------------------------ CreateLikeAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 
        public async Task<ApiResponse<string>> CreateLikeAsync(Guid trackId, Guid userId)
        {
            // Check if the user exists
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return ApiResponse<string>.ErrorResponse("User not found.");
            }

            // Check if the track exists
            var track = await _trackRepository.GetByIdAsync(trackId);
            if (track == null)
            {
                return ApiResponse<string>.ErrorResponse("Track not found.");
            }

            // Check if the like already exists
            var existingLike = await _likeRepository.GetLikeByTrackAndUserAsync(trackId, userId);
            if (existingLike != null)
            {
                return ApiResponse<string>.ErrorResponse("You have already liked this track.");
            }

            // Create a new like
            var like = new Like
            {
                TrackId = trackId,
                UserId = userId,
            };

            await _likeRepository.AddAsync(like);
            return ApiResponse<string>.SuccessResponse(string.Empty, "Like created successfully.");
        }


        // ------------------------------------------------------------------------ //
        // ------------------------ DeleteLikeAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 
        public async Task<ApiResponse<string>> DeleteLikeAsync(Guid trackId, Guid userId)
        {
            // Check if the user exists
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return ApiResponse<string>.ErrorResponse("User not found.");
            }
            // Check if the track exists
            var track = await _trackRepository.GetByIdAsync(trackId);
            if (track == null)
            {
                return ApiResponse<string>.ErrorResponse("Track not found.");
            }
            // Check if the like exists
            var existingLike = await _likeRepository.GetLikeByTrackAndUserAsync(trackId, userId);
            if (existingLike == null)
            {
                return ApiResponse<string>.ErrorResponse("You have not liked this track.");
            }
            // Delete the like
            await _likeRepository.DeleteAsync(existingLike);
            return ApiResponse<string>.SuccessResponse(string.Empty, "Like deleted successfully.");
        }



        // ---------------------------------------------------------------------------------- //
        // ------------------------ GetLikedTracksByUserAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<List<LikeResponseDto>>> GetLikedTracksByUserAsync(Guid userId)
        {
            // Check if the user exists  
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return ApiResponse<List<LikeResponseDto>>.ErrorResponse("User not found.");
            }

            // Get the likes by user ID  
            var likes = await _likeRepository.GetLikesByUserIdAsync(userId);
            if (likes == null || likes.Count == 0)
            {
                return ApiResponse<List<LikeResponseDto>>.SuccessResponse([], "No liked tracks found.");
            }

            // Map likes to LikeResponseDto  
            var likeDtos = likes.Select(like => new LikeResponseDto
            {
                TrackId = like.TrackId,
                TrackTitle = like.Track?.Title ?? "Unknown",
                UserId = like.UserId,
                UserName = like.User?.UserName ?? "Unknown",
                CreatedAt = like.CreatedAt
            }).ToList();

            return ApiResponse<List<LikeResponseDto>>.SuccessResponse(likeDtos, "Liked tracks retrieved successfully.");
        }

        public Task<ApiResponse<object>> GetTrackLikesAsync(Guid trackId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> HasUserLikedTrackAsync(Guid trackId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
