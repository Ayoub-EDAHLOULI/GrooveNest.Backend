using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class RatingRepository(AppDbContext context)
        : GenericRepository<Rating, int>(context), IRatingRepository
    {
        public async Task<Rating?> GetByTrackIdAndUserIdAsync(Guid trackId, Guid userId)
        {
            return await _context.Ratings
                .FirstOrDefaultAsync(r => r.TrackId == trackId && r.UserId == userId);
        }

        public async Task<List<Rating>> GetRatingsByTrackIdAsync(Guid trackId)
        {
            return await _context.Ratings
                .Where(r => r.TrackId == trackId)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingByTrackIdAsync(Guid trackId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.TrackId == trackId)
                .ToListAsync();

            if (!ratings.Any())
                return 0;

            return ratings.Average(r => r.Stars);
        }
    }
}
