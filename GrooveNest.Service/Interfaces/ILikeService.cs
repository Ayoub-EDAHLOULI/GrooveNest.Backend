using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface ILikeService
    {
        Task<ApiResponse<object>> GetTrackLikesAsync(Guid trackId);
        Task<ApiResponse<string>> CreateLikeAsync(Guid trackId, Guid UserId);
        Task<ApiResponse<string>> DeleteLikeAsync(Guid trackId, Guid UserId);
    }
}
