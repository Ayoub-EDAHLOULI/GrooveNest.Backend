using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class ArtistRepository(AppDbContext context) : GenericRepository<Artist, Guid>(context), IArtistRepository
    {
        public async Task<Artist?> GetArtistByName(string artistName)
        {
            return await _context.Artists
                .FirstOrDefaultAsync(a => a.Name.ToLower() == artistName.ToLower());
        }
    }
}
