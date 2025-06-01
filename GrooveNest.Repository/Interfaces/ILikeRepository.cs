using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface ILikeRepository : IGenericRepository<Like, Guid>
    {
        Task<Like?> GetLikeByTrackAndUserAsync(Guid trackId, Guid userId);
        Task<List<Like>> GetLikesByUserIdAsync(Guid userId);
    }
}
