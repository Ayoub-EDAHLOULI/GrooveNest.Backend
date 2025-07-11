﻿using GrooveNest.Domain.DTOs.LikeDTOs;
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



        // ------------------------------------------------------------------------------- //
        // ------------------------ GetLikesByTrackIdAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<object>> GetLikesByTrackIdAsync(Guid trackId)
        {
            // Check if the track exists
            var track = await _trackRepository.GetByIdAsync(trackId);
            if (track == null)
            {
                return ApiResponse<object>.ErrorResponse("Track not found.");
            }

            // Get the likes for the track
            var likes = await _likeRepository.GetLikesByTrackIdAsync(trackId);
            if (likes == null || likes.Count == 0)
            {
                return ApiResponse<object>.SuccessResponse(
                    new { TrackId = trackId, LikesCount = 0, LikedBy = new List<object>() },
                    "No likes found for this track."
                );
            }

            // Map likes to a response object
            var likeDtos = likes.Select(like => new
            {
                UserName = like.User?.UserName ?? "Unknown",
            }).ToList();

            return ApiResponse<object>.SuccessResponse(
                new
                {
                    TrackId = trackId,
                    LikesCount = likes.Count,
                    LikedBy = likeDtos
                },
                "Likes retrieved successfully."
            );
        }



        // ------------------------------------------------------------------------------- //
        // ------------------------ HasUserLikedTrackAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<bool>> HasUserLikedTrackAsync(Guid trackId, Guid userId)
        {
            // Check if the user exists
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return ApiResponse<bool>.ErrorResponse("User not found.");
            }
            // Check if the track exists
            var track = await _trackRepository.GetByIdAsync(trackId);
            if (track == null)
            {
                return ApiResponse<bool>.ErrorResponse("Track not found.");
            }
            // Check if the like exists
            var existingLike = await _likeRepository.GetLikeByTrackAndUserAsync(trackId, userId);
            return existingLike != null
                ? ApiResponse<bool>.SuccessResponse(true, "User has liked the track.")
                : ApiResponse<bool>.SuccessResponse(false, "User has not liked the track.");
        }
    }
}
