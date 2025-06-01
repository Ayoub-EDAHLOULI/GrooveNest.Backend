using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Data;
using GrooveNest.Repository.Interfaces;

namespace GrooveNest.Repository.Repositories
{
    public class LikeRepository(AppDbContext context) : GenericRepository<Like, Guid>(context), ILikeRepository
    {
    }
}
