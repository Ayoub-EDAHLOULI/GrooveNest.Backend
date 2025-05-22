using GrooveNest.Domain.DTOs.UserDTOs;
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


        // ---------------------------------------------------------------------------------- //
        // ------------------------ GetAllPaginatedUsersAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------------- // 

        [HttpGet]
        public async Task<IActionResult> GetAllPaginatedUsersAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var response = await _userService.GetAllPaginatedUsersAsync(page, pageSize, search);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------- //
        // ------------------------ GetUserByIdAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------ //
        // ------------------------ CreateUserAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateDto userCreateDto)
        {
            var response = await _userService.CreateUserAsync(userCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------ //
        // ------------------------ UpdateUserAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] UserUpdateDto userUpdateDto)
        {
            var response = await _userService.UpdateUserAsync(id, userUpdateDto);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
