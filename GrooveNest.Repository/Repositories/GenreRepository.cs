using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class GenreRepository(AppDbContext context) : GenericRepository<Genre, Guid>(context), IGenreRepository
    {
        public async Task<Genre?> GetGenreByName(string name)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Name == name);
        }

        public Task<Genre?> GetGenresWithTracksCounts()
        {
            return _context.Genres
                .Include(g => g.TrackGenres)
                .FirstOrDefaultAsync();
        }
    }
}
