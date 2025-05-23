using GrooveNest.Repository.Interfaces;

namespace GrooveNest.Service.Services
{
    public class RoleService(IRoleRepository roleRepository)
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
    }
}
