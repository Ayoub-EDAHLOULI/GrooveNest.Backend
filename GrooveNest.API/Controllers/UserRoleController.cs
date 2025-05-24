using GrooveNest.Domain.DTOs.UserRoleDTOs;
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


        // ---------------------------------------------------------------------------- //
        // ------------------------ CreateUserRoleAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------- //

        [HttpPost]
        public async Task<IActionResult> CreateUserRoleAsync([FromBody] UserRoleCreateDto userRoleCreateDto)
        {
            var response = await _userRoleService.AddUserRoleAsync(userRoleCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ----------------------------------------------------------------------------- //
        // ------------------------ GetUserRoleByIdAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------- //

        [HttpGet("{userId}/{roleId}")]
        public async Task<IActionResult> GetUserRoleByIdAsync(Guid userId, int roleId)
        {
            var response = await _userRoleService.GetUserRoleByIdAsync(userId, roleId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ---------------------------------------------------------------------------- //
        // ------------------------ DeleteUserRoleAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------- //

        [HttpDelete("{userId}/{roleId}")]
        public async Task<IActionResult> DeleteUserRoleAsync(Guid userId, int roleId)
        {
            var response = await _userRoleService.DeleteUserRoleAsync(userId, roleId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
