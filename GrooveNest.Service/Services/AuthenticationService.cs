using GrooveNest.Domain.DTOs.LoginDTOs;
using GrooveNest.Domain.DTOs.UserDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Repository.Repositories;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class AuthenticationService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IUserService userService, JwtTokenService jwtTokenService) : IAuthenticationService<LoginDto, UserAuthResponseDto>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;
        private readonly IUserService _userService = userService;
        private readonly JwtTokenService _jwtTokenService = jwtTokenService;

        public async Task<ApiResponse<UserAuthResponseDto>> LoginAsync(LoginDto loginDto)
        {
            string email = StringValidator.TrimOrEmpty(loginDto.Email);
            string password = StringValidator.TrimOrEmpty(loginDto.Password);

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return ApiResponse<UserAuthResponseDto>.ErrorResponse("Email and password are required");
            }

            // Check if the user exists
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                return ApiResponse<UserAuthResponseDto>.ErrorResponse("Invalid email or password");
            }

            // Get the user role
            var userRole = await _userRoleRepository.GetByIdAsync(user.Id);
            if (userRole == null || userRole.Role == null)
            {
                return ApiResponse<UserAuthResponseDto>.ErrorResponse("User role not found");
            }

            // Verify the password
            if (!string.IsNullOrEmpty(StringValidator.TrimOrEmpty(user.Password)))
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return CreateUserAuthResponse(user, userRole.Role.Name);
                }
            }

            return ApiResponse<UserAuthResponseDto>.ErrorResponse("Invalid email or password");
        }


        public async Task<ApiResponse<UserAuthResponseDto>> RegisterAsync(UserCreateDto registerDto)
        {
            var createdUser = await _userService.CreateUserAsync(registerDto);
            if (!createdUser.Success || createdUser.Data == null)
            {
                return ApiResponse<UserAuthResponseDto>.ErrorResponse(createdUser.Message ?? "User creation failed");
            }

            // Get the created user
            var user = await _userRepository.GetByEmailAsync(createdUser.Data.Email);
            if (user == null)
            {
                return ApiResponse<UserAuthResponseDto>.ErrorResponse("User not found after creation");
            }

            // Get the user role
            var userRole = await _userRoleRepository.GetByIdAsync(user.Id);
            if (userRole == null || userRole.Role == null)
            {
                return ApiResponse<UserAuthResponseDto>.ErrorResponse("User role not found after creation");
            }

            // Generate JWT token
            return CreateUserAuthResponse(user, userRole.Role.Name);
        }


        private ApiResponse<UserAuthResponseDto> CreateUserAuthResponse(User user, string roleName)
        {
            // Generate JWT token
            string token = _jwtTokenService.GenerateJwtToken(user.Email, roleName);

            var response = new UserAuthResponseDto
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                UserName = user.UserName,
                Role = roleName,
                Token = token
            };

            return ApiResponse<UserAuthResponseDto>.SuccessResponse(response, "Login successful");
        }
    }
}
