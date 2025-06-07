using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre, Guid>
    {
        Task<Genre?> GetGenresWithTracksCounts();
        Task<Genre?> GetGenreByName(string name);
    }
}
