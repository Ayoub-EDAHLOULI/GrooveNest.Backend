using GrooveNest.Domain.DTOs.UserRoleDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class UserRoleService(IUserRoleRepository userRoleRepository) : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;


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
                RoleId = ur.RoleId
            }).ToList();

            return ApiResponse<List<UserRoleResponseDto>>.SuccessResponse(userRoleDtos, "User roles retrieved successfully.");
        }

        public Task<ApiResponse<UserRoleResponseDto>> AddUserRoleAsync(UserRoleCreateDto userRoleCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserRoleResponseDto>> DeleteUserRoleAsync(Guid userId, int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserRoleResponseDto>> GetUserRoleByIdAsync(Guid userId, int roleId)
        {
            throw new NotImplementedException();
        }
    }
}
