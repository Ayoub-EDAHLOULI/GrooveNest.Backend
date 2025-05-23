using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IUserRoleRepository : IGenericRepository<UserRole, Guid>
    {
        Task<UserRole?> GetByIdsAsync(Guid userId, int roleId);
        Task<IEnumerable<Role>> GetRolesByUserIdAsync(Guid userId);
        Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId);
    }
}
