﻿using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrooveNest.Repository.Repositories
{
    public class UserRepository(AppDbContext context) : GenericRepository<User, Guid>(context), IUserRepository
    {
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public Task<User?> GetByUserNameAsync(string userName)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
