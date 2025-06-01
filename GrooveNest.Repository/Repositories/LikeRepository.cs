using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class LikeRepository(AppDbContext context) : GenericRepository<Like, Guid>(context), ILikeRepository
    {
        public async Task<Like?> GetLikeByTrackAndUserAsync(Guid trackId, Guid userId)
        {
            return await _context.Likes
                .FirstOrDefaultAsync(l => l.TrackId == trackId && l.UserId == userId);
        }
    }
}
