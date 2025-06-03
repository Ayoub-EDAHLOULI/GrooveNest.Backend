using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class CommentRepository(AppDbContext context) : GenericRepository<Comment, int>(context), ICommentRepository
    {
        public async Task<Comment?> GetByTrackIdAndUserIdAsync(Guid trackId, Guid userId)
        {
            return await _context.Comments
                .FirstOrDefaultAsync(c => c.TrackId == trackId && c.UserId == userId);
        }

        public async Task<int> GetCommentCountByTrackIdAsync(Guid trackId)
        {
            return await _context.Comments
                .CountAsync(c => c.TrackId == trackId);
        }

        public async Task<List<Comment>> GetCommentsByTrackIdAsync(Guid trackId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Track)
                .Where(c => c.TrackId == trackId)
                .ToListAsync();
        }
    }
}
