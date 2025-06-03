using GrooveNest.Domain.DTOs.CommentDTOs;
using GrooveNest.Domain.DTOs.RatingDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class CommentService(ICommentRepository commentRepository, IUserRepository userRepository, ITrackRepository trackRepository) : ICommentService
    {
        private readonly ICommentRepository _commentRepository = commentRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITrackRepository _trackRepository = trackRepository;



        // --------------------------------------------------------------------------- //
        // ------------------------ CreateCommentAsync METHODS ----------------------- //
        // --------------------------------------------------------------------------- // 
        public async Task<ApiResponse<string>> CreateCommentAsync(CommentCreateDto commentCreateDto)
        {
            // Check if the user exists
            var user = await _userRepository.GetByIdAsync(commentCreateDto.UserId);
            if (user == null)
            {
                return ApiResponse<string>.ErrorResponse("User not found.");
            }

            // Check if the track exists
            var track = await _trackRepository.GetByIdAsync(commentCreateDto.TrackId);
            if (track == null)
            {
                return ApiResponse<string>.ErrorResponse("Track not found.");
            }

            // Check if the content not empty
            if (!StringValidator.IsNullOrWhiteSpace(commentCreateDto.Content))
            {
                return ApiResponse<string>.ErrorResponse("Comment is required");
            }

            // Create a new comment entity
            var comment = new Comment
            {
                Content = commentCreateDto.Content,
                UserId = user.Id,
                TrackId = track.Id,
                CreatedAt = DateTime.UtcNow,
            };

            // Save the comment
            await _commentRepository.AddAsync(comment);

            // Return a response 
            return ApiResponse<string>.SuccessResponse(string.Empty, "Comment created successfully.");

        }

        public Task<ApiResponse<bool>> DeleteCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<CommentResponseDto>>> GetCommentsByTrackId(Guid trackId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> UpdateCommentAsync(CommentUpdateDto commentUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
