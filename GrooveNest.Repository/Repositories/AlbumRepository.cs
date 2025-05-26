using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class AlbumRepository(AppDbContext context) : GenericRepository<Album, Guid>(context), IAlbumRepository
    {
        public async Task<Album?> GetAlbumByTitle(string title)
        {
            return await _context.Albums
                .Include(a => a.Artist)
                .FirstOrDefaultAsync(a => a.Title == title);
        }
    }
}
