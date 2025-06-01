using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class PlaylistTrackRepository(AppDbContext context) : GenericRepository<PlaylistTrack, Guid>(context), IPlaylistTrackRepository
    {
        public async Task<PlaylistTrack?> GetByPlaylistAndTrackAsync(Guid playlistId, Guid trackId)
        {
            return await _context.PlaylistTracks
                .FirstOrDefaultAsync(pt => pt.PlaylistId == playlistId && pt.TrackId == trackId);
        }

        public async Task<List<PlaylistTrack>> GetTracksByPlaylistIdWithTrackAsync(Guid playlistId)
        {
            return await _context.PlaylistTracks
                .Include(pt => pt.Track) // Include related Track entity
                .Include(pt => pt.Playlist) // Include related Playlist entity
                .Where(pt => pt.PlaylistId == playlistId)
                .ToListAsync();
        }

        public async Task<int> GetNextTrackNumberAsync(Guid playlistId)
        {
            var count = await _context.PlaylistTracks
                .Where(pt => pt.PlaylistId == playlistId)
                .CountAsync();

            return count + 1;
        }
    }
}
