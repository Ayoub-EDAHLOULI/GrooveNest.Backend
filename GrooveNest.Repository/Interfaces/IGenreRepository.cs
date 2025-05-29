using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre, int>
    {
        Task<Genre?> GetGenreByName(string name);
    }
}
