using GrooveNest.Domain.DTOs.UserDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public Task<ApiResponse<UserResponseDto>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<object>> GetAllPaginatedUsersAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<UserResponseDto>>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserResponseDto>> GetUserByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserResponseDto>> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
