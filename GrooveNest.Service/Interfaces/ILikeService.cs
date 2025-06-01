using GrooveNest.Domain.DTOs.LikeDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface ILikeService
    {
        Task<ApiResponse<List<LikeResponseDto>>> GetLikedTracksByUserAsync(Guid userId);
        Task<ApiResponse<string>> CreateLikeAsync(Guid trackId, Guid userId);
        Task<ApiResponse<string>> DeleteLikeAsync(Guid trackId, Guid userId);
        Task<ApiResponse<bool>> HasUserLikedTrackAsync(Guid trackId, Guid userId);
    }
}
