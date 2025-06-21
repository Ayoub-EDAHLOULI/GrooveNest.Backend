using GrooveNest.Domain.Entities;

namespace GrooveNest.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User, Guid>
    {
        Task<List<User>> GetAllWithRolesAsync();
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetUserNameByIdAsync(Guid id);
        Task<User?> GetUserDetails(Guid userId);
    }
}
