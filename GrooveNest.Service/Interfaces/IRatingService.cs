using GrooveNest.Domain.DTOs.RatingDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IRatingService
    {
        Task<ApiResponse<double>> GetAverageRatingByTrackIdAsync(Guid trackId);
        Task<ApiResponse<string>> CreateRatingAsync(RatingCreateDto ratingCreateDto);
        Task<ApiResponse<RatingResponseDto>> UpdateRatingAsync(int ratingId, RatingUpdateDto ratingUpdateDto);
        Task<ApiResponse<bool>> DeleteRatingAsync(int ratingId);
    }
}
