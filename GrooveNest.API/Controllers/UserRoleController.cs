using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController(UserRoleService userRoleService) : ControllerBase
    {
        private readonly UserRoleService _userRoleService = userRoleService;
    }
}
