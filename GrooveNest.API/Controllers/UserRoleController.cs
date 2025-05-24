using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController(UserRoleService userRoleService) : ControllerBase
    {
        private readonly UserRoleService _userRoleService = userRoleService;


        // ---------------------------------------------------------------------------- //
        // ------------------------ GetAllUserRoleAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------- // 

        [HttpGet]
        public async Task<IActionResult> GetAllUserRolesAsync()
        {
            var response = await _userRoleService.GetAllUserRoleAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
