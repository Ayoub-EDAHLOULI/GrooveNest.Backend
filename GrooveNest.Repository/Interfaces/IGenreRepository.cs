using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre, Guid>
    {
        Task<Genre?> GetGenreByName(string name);
    }
}
