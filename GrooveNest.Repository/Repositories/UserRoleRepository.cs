using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class UserRoleRepository(AppDbContext context) : IUserRoleRepository
    {

        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<UserRole>> GetAllAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }
        public async Task AddAsync(UserRole entity)
        {
            await _context.UserRoles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserRole entity)
        {
            _context.UserRoles.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserRole?> GetByIdsAsync(Guid userId, int roleId)
        {
            return await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }

        public async Task<List<Role>> GetRolesByUserIdAsync(Guid userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<List<User>> GetUsersByRoleIdAsync(int roleId)
        {
            return await _context.UserRoles
                .Where(ur => ur.RoleId == roleId)
                .Select(ur => ur.User)
                .ToListAsync();
        }
    }
}
