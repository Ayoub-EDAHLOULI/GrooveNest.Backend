using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment, int>
    {
        Task<Comment?> GetByTrackIdAndUserIdAsync(Guid trackId, Guid userId);
        Task<List<Comment>> GetCommentsByTrackIdAsync(Guid trackId);
        Task<int> GetCommentCountByTrackIdAsync(Guid trackId);
    }
}
