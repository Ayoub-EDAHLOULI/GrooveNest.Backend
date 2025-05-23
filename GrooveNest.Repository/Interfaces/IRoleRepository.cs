using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role, int>
    {
        /// <summary>
        /// Get a role by its name.
        /// </summary>
        /// <param name="name">The name of the role.</param>
        /// <returns>The role with the specified name, or null if not found.</returns>
        Task<Role?> GetByNameAsync(string name);
    }
}
