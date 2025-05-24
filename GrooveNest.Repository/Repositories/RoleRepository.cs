using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class RoleRepository(AppDbContext context) : GenericRepository<Role, int>(context), IRoleRepository
    {
        public Task<Role?> GetByNameAsync(string name)
        {
            return _context.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());
        }
    }
}
