using GrooveNest.Domain.DTOs.RoleDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;


        // ------------------------------------------------------------------------- //
        // ------------------------ GetAllRolesAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<List<RoleResponseDto>>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();

            //  Check if roles are null
            if (roles == null)
            {
                return ApiResponse<List<RoleResponseDto>>.ErrorResponse("No roles found.");
            }

            var roleResponseDtos = roles.Select(role => new RoleResponseDto
            {
                Id = role.Id,
                Name = role.Name
            }).ToList();

            // Return the response
            return ApiResponse<List<RoleResponseDto>>.SuccessResponse(roleResponseDtos, "Roles retrieved successfully.");
        }



        public Task<ApiResponse<RoleResponseDto>> CreateRoleAsync(RoleCreateDto roleCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> DeleteRoleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<object>> GetAllPaginatedRolesAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<RoleResponseDto>> GetRoleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<RoleResponseDto>> UpdateRoleAsync(int id, RoleUpdateDto roleUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
