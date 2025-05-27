using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;

namespace GrooveNest.Repository.Repositories
{
    public class ArtistApplicationRepository(AppDbContext context) : GenericRepository<ArtistApplication, Guid>(context), IArtistApplicationRepository
    {
    }
}
