using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class TrackGenreRepository(AppDbContext context) : ITrackGenreRepository
    {
        public async Task AddAsync(TrackGenre entity)
        {
            await context.TrackGenres.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TrackGenre entity)
        {
            context.TrackGenres.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrackGenre>> GetAllAsync()
        {
            return await context.TrackGenres
                .Include(tg => tg.Track)
                .Include(tg => tg.Genre)
                .ToListAsync();
        }

        public async Task<TrackGenre?> GetByIdAsync(Guid trackId)
        {
            return await context.TrackGenres
                .Include(tg => tg.Track)
                .Include(tg => tg.Genre)
                .FirstOrDefaultAsync(tg => tg.TrackId == trackId);
        }

        public async Task<List<Genre>> GetGenresByTrackIdAsync(Guid trackId)
        {
            return await context.TrackGenres
                .Where(tg => tg.TrackId == trackId)
                .Select(tg => tg.Genre)
                .ToListAsync();
        }

        public async Task<List<Track>> GetTracksByGenreIdAsync(Guid genreId)
        {
            return await context.TrackGenres
                .Where(tg => tg.GenreId == genreId)
                .Select(tg => tg.Track)
                .ToListAsync();
        }
    }
}
