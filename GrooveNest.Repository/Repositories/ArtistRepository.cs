using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class ArtistRepository(AppDbContext context) : GenericRepository<Artist, Guid>(context), IArtistRepository
    {
        public async Task<List<Artist>> GetAllWithUsersAsync()
        {
            return await _context.Artists
                .Include(a => a.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Artist?> GetArtistByName(string artistName)
        {
            return await _context.Artists
                .FirstOrDefaultAsync(a => a.Name == artistName);
        }

        public async Task<Artist?> GetArtistWithDetails(Guid id)
        {
            return await _context.Artists
                .Include(a => a.User)
                .Include(a => a.Tracks)
                .Include(a => a.Albums)
                .ThenInclude(a => a.Tracks)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.User != null && a.User.Id == id);
        }
    }
}
