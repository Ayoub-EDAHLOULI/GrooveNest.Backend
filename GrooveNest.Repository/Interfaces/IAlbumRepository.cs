using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IAlbumRepository : IGenericRepository<Album, Guid>
    {
        Task<Album?> GetAlbumByTitle(string title);
    }
}
