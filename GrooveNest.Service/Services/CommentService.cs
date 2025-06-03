using GrooveNest.Domain.DTOs.CommentDTOs;
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



        // --------------------------------------------------------------------------- //
        // ------------------------ UpdateCommentAsync METHODS ----------------------- //
        // --------------------------------------------------------------------------- // 
        public async Task<ApiResponse<string>> UpdateCommentAsync(int id, CommentUpdateDto commentUpdateDto)
        {
            // Find the comment by id
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return ApiResponse<string>.ErrorResponse("Comment not found.");
            }

            // Validate new content if provided
            if (!StringValidator.IsNullOrWhiteSpace(commentUpdateDto.Content))
            {
                comment.Content = StringValidator.TrimOrEmpty(commentUpdateDto.Content);
            }

            await _commentRepository.UpdateAsync(comment);

            return ApiResponse<string>.SuccessResponse(string.Empty, "Comment updated successfully.");
        }



        // --------------------------------------------------------------------------- //
        // ------------------------ DeleteCommentAsync METHODS ----------------------- //
        // --------------------------------------------------------------------------- // 
        public async Task<ApiResponse<bool>> DeleteCommentAsync(int commentId)
        {
            // Find the comment by id
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                return ApiResponse<bool>.ErrorResponse("Comment not found.");
            }

            await _commentRepository.DeleteAsync(comment);
            return ApiResponse<bool>.SuccessResponse(true, "Comment deleted successfully.");
        }



        // ----------------------------------------------------------------------------- //
        // ------------------------ GetCommentsByTrackId METHODS ----------------------- //
        // ----------------------------------------------------------------------------- // 
        public async Task<ApiResponse<List<CommentResponseDto>>> GetCommentsByTrackIdAsync(Guid trackId)
        {
            // Check if the track exists
            var track = await _trackRepository.GetByIdAsync(trackId);
            if (track == null)
            {
                return ApiResponse<List<CommentResponseDto>>.ErrorResponse("Track not found.");
            }

            // Get all comments for the track
            var comments = await _commentRepository.GetCommentsByTrackIdAsync(trackId);

            // Map comments to DTOs
            var commentDtos = new List<CommentResponseDto>();
            foreach (var comment in comments)
            {
                // Fetch user for each comment (could be optimized with includes or batch fetching)
                var user = await _userRepository.GetByIdAsync(comment.UserId);

                commentDtos.Add(new CommentResponseDto
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    TrackId = comment.TrackId,
                    UserId = comment.UserId,
                    UserName = user?.UserName,
                    TrackTitle = track.Title
                });
            }

            return ApiResponse<List<CommentResponseDto>>.SuccessResponse(commentDtos, "Comments retrieved successfully.");
        }

    }
}
