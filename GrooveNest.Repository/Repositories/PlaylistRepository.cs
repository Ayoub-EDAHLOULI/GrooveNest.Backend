using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class PlaylistRepository(AppDbContext context) : GenericRepository<Playlist, Guid>(context), IPlaylistRepository
    {
        public async Task<Playlist?> GetPlaylistByName(string name)
        {
            return await _context.Playlists
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
