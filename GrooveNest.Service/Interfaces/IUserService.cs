using GrooveNest.Domain.DTOs.UserDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<List<UserResponseDto>>> GetAllUsersAsync();
        Task<ApiResponse<object>> GetAllPaginatedUsersAsync(int page = 1, int pageSize = 10, string searchQuery = "");
        Task<ApiResponse<UserResponseDto>> GetUserByIdAsync(Guid id);
        Task<ApiResponse<UserResponseDto>> CreateUserAsync(UserCreateDto userCreateDto);
        Task<ApiResponse<UserResponseDto>> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto);
        Task<ApiResponse<string>> DeleteUserAsync(Guid id);
    }
}
