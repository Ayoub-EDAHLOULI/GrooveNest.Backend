using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class TrackRepository(AppDbContext context) : GenericRepository<Track, Guid>(context), ITrackRepository
    {
        public async Task<Track?> GetTrackByTitleAsync(string title)
        {
            return await _context.Tracks
                .Include(t => t.Artist)
                .Include(t => t.Album)
                .FirstOrDefaultAsync(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<Track>> GetTracksByAlbumIdAsync(Guid albumId)
        {
            return await _context.Tracks
                .Include(t => t.Artist)
                .Include(t => t.Album)
                .Where(t => t.AlbumId == albumId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Track>> GetTracksByAlbumTitleAsync(string albumTitle)
        {
            return await _context.Tracks
                .Include(t => t.Artist)
                .Include(t => t.Album)
                .Where(t => t.Album != null && t.Album.Title.Equals(albumTitle, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<IEnumerable<Track>> GetTracksByArtistIdAsync(Guid artistId)
        {
            return await _context.Tracks
                .Include(t => t.Artist)
                .Include(t => t.Album)
                .Where(t => t.ArtistId == artistId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Track>> GetTracksByArtistNameAsync(string artistName)
        {
            return await _context.Tracks
                .Include(t => t.Artist)
                .Include(t => t.Album)
                .Where(t => t.Artist.Name.Equals(artistName, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }
    }
}
