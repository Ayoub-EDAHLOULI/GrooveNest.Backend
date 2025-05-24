using GrooveNest.Domain.DTOs.UserRoleDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class UserRoleService(IUserRoleRepository userRoleRepository, IUserRepository userRepository, IRoleRepository roleRepository) : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;


        // ---------------------------------------------------------------------------- //
        // ------------------------ GetAllUserRoleAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------- // 
        public async Task<ApiResponse<List<UserRoleResponseDto>>> GetAllUserRoleAsync()
        {
            var userRoles = await _userRoleRepository.GetAllAsync();
            if (userRoles == null || !userRoles.Any())
            {
                return ApiResponse<List<UserRoleResponseDto>>.ErrorResponse("No user roles found.");
            }
            var userRoleDtos = userRoles.Select(ur => new UserRoleResponseDto
            {
                UserId = ur.UserId,
                RoleId = ur.RoleId,
                UserName = ur.User.UserName,
                RoleName = ur.Role.Name
            }).ToList();

            return ApiResponse<List<UserRoleResponseDto>>.SuccessResponse(userRoleDtos, "User roles retrieved successfully.");
        }


        // ---------------------------------------------------------------------------- //
        // ------------------------ CreateUserRoleAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------- // 
        public async Task<ApiResponse<UserRoleResponseDto>> AddUserRoleAsync(UserRoleCreateDto userRoleCreateDto)
        {

            // Check if user exists
            var user = await _userRepository.GetByIdAsync(userRoleCreateDto.UserId);
            if (user == null)
            {
                return ApiResponse<UserRoleResponseDto>.ErrorResponse("User not found.");
            }

            // Check if role exists
            var role = await _roleRepository.GetByIdAsync(userRoleCreateDto.RoleId);
            if (role == null)
            {
                return ApiResponse<UserRoleResponseDto>.ErrorResponse("Role not found.");
            }

            // Check if user already has the role
            var existingUserRole = await _userRoleRepository.GetByIdsAsync(userRoleCreateDto.UserId, userRoleCreateDto.RoleId);
            if (existingUserRole != null)
            {
                return ApiResponse<UserRoleResponseDto>.ErrorResponse("User already has this role.");
            }

            // Create new user role
            var userRole = new UserRole
            {
                UserId = userRoleCreateDto.UserId,
                RoleId = userRoleCreateDto.RoleId
            };
            await _userRoleRepository.AddAsync(userRole);
            var userRoleResponseDto = new UserRoleResponseDto
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
                UserName = user.UserName,
                RoleName = role.Name
            };

            // Return success response
            return ApiResponse<UserRoleResponseDto>.SuccessResponse(userRoleResponseDto, "User role created successfully.");
        }


        // ----------------------------------------------------------------------------- //
        // ------------------------ GetUserRoleByIdAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------- //
        public async Task<ApiResponse<UserRoleResponseDto>> GetUserRoleByIdAsync(Guid userId)
        {
            // Check if user role exists
            var userRole = await _userRoleRepository.GetByIdAsync(userId);
            if (userRole == null)
            {
                return ApiResponse<UserRoleResponseDto>.ErrorResponse("User role not found.");
            }
            var userRoleResponseDto = new UserRoleResponseDto
            {
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
                UserName = userRole.User.UserName,
                RoleName = userRole.Role.Name
            };
            // Return success response
            return ApiResponse<UserRoleResponseDto>.SuccessResponse(userRoleResponseDto, "User role retrieved successfully.");
        }


        // ---------------------------------------------------------------------------- //
        // ------------------------ DeleteUserRoleAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------- //
        public async Task<ApiResponse<string>> DeleteUserRoleAsync(Guid userId, int roleId)
        {
            // Check if user role exists
            var userRole = await _userRoleRepository.GetByIdsAsync(userId, roleId);
            if (userRole == null)
            {
                return ApiResponse<string>.ErrorResponse("User role not found.");
            }
            // Delete user role
            await _userRoleRepository.DeleteAsync(userRole);
            return ApiResponse<string>.SuccessResponse(string.Empty, "User role deleted successfully.");
        }
    }
}
