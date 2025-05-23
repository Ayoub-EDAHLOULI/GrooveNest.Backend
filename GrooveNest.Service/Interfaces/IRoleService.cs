using GrooveNest.Domain.DTOs.RoleDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IRoleService
    {
        Task<ApiResponse<List<RoleResponseDto>>> GetAllRolesAsync();
        Task<ApiResponse<object>> GetAllPaginatedRolesAsync(int page = 1, int pageSize = 10, string searchQuery = "");
        Task<ApiResponse<RoleResponseDto>> GetRoleByIdAsync(int id);
        Task<ApiResponse<RoleResponseDto>> CreateRoleAsync(RoleCreateDto roleCreateDto);
        Task<ApiResponse<RoleResponseDto>> UpdateRoleAsync(int id, RoleUpdateDto roleUpdateDto);
        Task<ApiResponse<string>> DeleteRoleAsync(int id);

    }
}
