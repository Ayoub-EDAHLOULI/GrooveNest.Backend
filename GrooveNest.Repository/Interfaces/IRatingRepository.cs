using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IRatingRepository : IGenericRepository<Rating, int>
    {
        Task<Rating?> GetByTrackIdAndUserIdAsync(Guid trackId, Guid userId);
        Task<List<Rating>> GetRatingsByTrackIdAsync(Guid trackId);
        Task<double> GetAverageRatingByTrackIdAsync(Guid trackId);
    }
}
