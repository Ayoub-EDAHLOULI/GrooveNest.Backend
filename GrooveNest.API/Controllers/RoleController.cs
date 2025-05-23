using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(RoleService roleService) : ControllerBase
    {
        private readonly RoleService _roleService = roleService;


        // ------------------------------------------------------------------------- //
        // ------------------------ GetAllRolesAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var response = await _roleService.GetAllRolesAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
