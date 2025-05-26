using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IPlaylistRepository : IGenericRepository<Playlist, Guid>
    {
        Task<Playlist?> GetPlaylistByName(string name);
    }
}
