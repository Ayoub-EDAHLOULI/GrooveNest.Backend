using GrooveNest.Domain.DTOs.UserRoleDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IUserRoleService
    {
        Task<ApiResponse<IList<UserRoleResponseDto>>> GetAllUserRoleAsync();
        Task<ApiResponse<UserRoleResponseDto>> GetUserRoleByIdAsync(Guid userId, int roleId);
        Task<ApiResponse<UserRoleResponseDto>> AddUserRoleAsync(UserRoleCreateDto userRoleCreateDto);
        Task<ApiResponse<UserRoleResponseDto>> DeleteUserRoleAsync(Guid userId, int roleId);
    }
}
