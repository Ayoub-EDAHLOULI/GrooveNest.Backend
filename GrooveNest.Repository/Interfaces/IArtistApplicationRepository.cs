using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IArtistApplicationRepository : IGenericRepository<ArtistApplication, Guid>
    {
        Task<ArtistApplication?> GetUserArtistApplicationAsync(Guid userId);
    }
}
