using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IArtistRepository : IGenericRepository<Artist, Guid>
    {
        Task<Artist?> GetArtistByName(string artistName);
        Task<List<Artist>> GetAllWithUsersAsync();
        Task<Artist?> GetArtistWithDetails(Guid id);
    }
}
