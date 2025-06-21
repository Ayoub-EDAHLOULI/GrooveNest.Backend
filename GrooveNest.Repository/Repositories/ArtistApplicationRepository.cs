using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class ArtistApplicationRepository(AppDbContext context) : GenericRepository<ArtistApplication, Guid>(context), IArtistApplicationRepository
    {
        public async Task<ArtistApplication?> GetUserArtistApplicationAsync(Guid userId)
        {
            return await _context.ArtistApplications
                .FirstOrDefaultAsync(app => app.UserId == userId);
        }
    }
}
