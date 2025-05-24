using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRole>> GetAllAsync();
        Task<UserRole?> GetByIdsAsync(Guid userId, int roleId);
        Task<UserRole?> GetByIdAsync(Guid userId);
        Task AddAsync(UserRole entity);
        Task DeleteAsync(UserRole entity);

        Task<List<Role>> GetRolesByUserIdAsync(Guid userId);
        Task<List<User>> GetUsersByRoleIdAsync(int roleId);
    }
}
