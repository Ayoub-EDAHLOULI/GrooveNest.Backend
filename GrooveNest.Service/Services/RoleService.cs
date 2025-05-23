using GrooveNest.Domain.DTOs.RoleDTOs;
using GrooveNest.Domain.Entities;
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



        // ---------------------------------------------------------------------------------- //
        // ------------------------ GetAllPaginatedRolesAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<object>> GetAllPaginatedRolesAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            // Get all roles
            var roles = await _roleRepository.GetAllAsync();

            // Check if roles are null
            if (roles == null || !roles.Any())
            {
                return ApiResponse<object>.ErrorResponse("No roles found.");
            }

            // Filter roles based on search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                roles = roles.Where(role => role.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Paginate the results
            var paginatedRoles = roles
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(role => new RoleResponseDto
                {
                    Id = role.Id,
                    Name = role.Name
                })
                .ToList();

            // Total number of roles
            var totalRoles = roles.Count();

            // Create the response object
            var response = new
            {
                PaginatedRoles = paginatedRoles,
                TotalRoles = totalRoles
            };

            // Return the response
            return ApiResponse<object>.SuccessResponse(response, "Roles retrieved successfully.");
        }



        // -------------------------------------------------------------------------- //
        // ------------------------ GetRolesByIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<RoleResponseDto>> GetRoleByIdAsync(int id)
        {
            // Get the role by ID
            var role = await _roleRepository.GetByIdAsync(id);
            // Check if the role is null
            if (role == null)
            {
                return ApiResponse<RoleResponseDto>.ErrorResponse("Role not found.");
            }
            // Map the role to RoleResponseDto
            var roleResponseDto = new RoleResponseDto
            {
                Id = role.Id,
                Name = role.Name
            };
            // Return the response
            return ApiResponse<RoleResponseDto>.SuccessResponse(roleResponseDto, "Role retrieved successfully.");
        }



        // ------------------------------------------------------------------------ //
        // ------------------------ CreateRoleAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 
        public async Task<ApiResponse<RoleResponseDto>> CreateRoleAsync(RoleCreateDto roleCreateDto)
        {
            // Check if the role name is empty
            if (string.IsNullOrEmpty(roleCreateDto.Name))
            {
                return ApiResponse<RoleResponseDto>.ErrorResponse("Role name cannot be empty.");
            }
            // Check if the role already exists
            var existingRole = await _roleRepository.GetByNameAsync(StringValidator.TrimOrEmpty(roleCreateDto.Name));
            if (existingRole != null)
            {
                return ApiResponse<RoleResponseDto>.ErrorResponse("Role already exists.");
            }
            // Create a new role entity
            var newRole = new Role
            {
                Name = StringValidator.TrimOrEmpty(roleCreateDto.Name)
            };
            // Save the new role to the database
            await _roleRepository.AddAsync(newRole);
            // Map the new role to RoleResponseDto
            var roleResponseDto = new RoleResponseDto
            {
                Id = newRole.Id,
                Name = newRole.Name
            };
            // Return the response
            return ApiResponse<RoleResponseDto>.SuccessResponse(roleResponseDto, "Role created successfully.");
        }



        // ------------------------------------------------------------------------ //
        // ------------------------ UpdateRoleAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ //
        public async Task<ApiResponse<RoleResponseDto>> UpdateRoleAsync(int id, RoleUpdateDto roleUpdateDto)
        {
            // Get the existing role by ID
            var existingRole = await _roleRepository.GetByIdAsync(id);
            // Check if the role exists
            if (existingRole == null)
            {
                return ApiResponse<RoleResponseDto>.ErrorResponse("Role not found.");
            }

            // Update name if provided
            if (!string.IsNullOrEmpty(roleUpdateDto.Name))
            {
                // Check if the role name already exists
                var existingRoleWithName = await _roleRepository.GetByNameAsync(StringValidator.TrimOrEmpty(roleUpdateDto.Name));
                if (existingRoleWithName != null && existingRoleWithName.Id != id)
                {
                    return ApiResponse<RoleResponseDto>.ErrorResponse("Role name already exists.");
                }

                // Update the role name
                existingRole.Name = StringValidator.TrimOrEmpty(roleUpdateDto.Name);
            }

            // Save the changes to the database
            await _roleRepository.UpdateAsync(existingRole);
            // Map the updated role to RoleResponseDto
            var roleResponseDto = new RoleResponseDto
            {
                Id = existingRole.Id,
                Name = existingRole.Name
            };
            // Return the response
            return ApiResponse<RoleResponseDto>.SuccessResponse(roleResponseDto, "Role updated successfully.");
        }




        public Task<ApiResponse<string>> DeleteRoleAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
