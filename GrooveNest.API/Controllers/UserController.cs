using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;


        // ------------------------------------------------------------------------- //
        // ------------------------ GetAllUsersAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var response = await _userService.GetAllUsersAsync();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
