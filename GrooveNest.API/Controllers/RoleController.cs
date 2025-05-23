using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(RoleService roleService) : ControllerBase
    {
        private readonly RoleService _roleService = roleService;
    }
}
