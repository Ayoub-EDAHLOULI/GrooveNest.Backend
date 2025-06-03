using GrooveNest.Domain.DTOs.LoginDTOs;
using GrooveNest.Domain.DTOs.UserDTOs;
using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(AuthenticationService authenticationService) : ControllerBase
    {
        private readonly AuthenticationService _authenticationService = authenticationService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authenticationService.LoginAsync(loginDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto registerDto)
        {
            var response = await _authenticationService.RegisterAsync(registerDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
