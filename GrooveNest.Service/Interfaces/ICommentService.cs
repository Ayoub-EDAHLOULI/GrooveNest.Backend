using GrooveNest.Domain.DTOs.CommentDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface ICommentService
    {
        Task<ApiResponse<List<CommentResponseDto>>> GetCommentsByTrackId(Guid trackId);
        Task<ApiResponse<string>> CreateCommentAsync(CommentCreateDto commentCreateDto);
        Task<ApiResponse<string>> UpdateCommentAsync(int id, CommentUpdateDto commentUpdateDto);
        Task<ApiResponse<bool>> DeleteCommentAsync(int commentId);
    }
}
