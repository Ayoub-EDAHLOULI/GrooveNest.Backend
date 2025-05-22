using GrooveNest.Domain.DTOs.UserDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;


        // ------------------------------------------------------------------------- //
        // ------------------------ GetAllUsersAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<List<UserResponseDto>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            // Validate if users is null or empty
            if (users == null || !users.Any())
            {
                return ApiResponse<List<UserResponseDto>>.ErrorResponse("No users found");
            }

            // Map users to UserResponseDto
            var userResponseDtos = users.Select(user => new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            }).ToList();

            // Return success response
            return ApiResponse<List<UserResponseDto>>.SuccessResponse(userResponseDtos, "Users retrieved successfully");
        }


        // ---------------------------------------------------------------------------------- //
        // ------------------------ GetAllPaginatedUsersAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<object>> GetAllPaginatedUsersAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            var users = await _userRepository.GetAllAsync();

            // Validate if users is null or empty
            if (users == null || !users.Any())
            {
                return ApiResponse<object>.ErrorResponse("No users found");
            }

            // Filter users based on search query
            if (!string.IsNullOrEmpty(searchQuery))
            {
                users = [.. users.Where(user => user.UserName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                            user.Email.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))];
            }

            // Paginate users logic
            var paginatedUsers = users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(user => new UserResponseDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                })
                .ToList();

            // Calculate total pages
            var totalUsers = users.Count();

            var response = new
            {
                PaginatedUsers = paginatedUsers,
                TotalUsers = totalUsers,
            };

            // Return success response
            return ApiResponse<object>.SuccessResponse(response, "Users retrieved successfully");
        }


        // ------------------------------------------------------------------------- //
        // ------------------------ GetUserByIdAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<UserResponseDto>> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            // Validate if user is null
            if (user == null)
            {
                return ApiResponse<UserResponseDto>.ErrorResponse("User not found");
            }
            // Map user to UserResponseDto
            var userResponseDto = new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
            // Return success response
            return ApiResponse<UserResponseDto>.SuccessResponse(userResponseDto, "User retrieved successfully");
        }


        // ------------------------------------------------------------------------- //
        // ------------------------ GetUserByIdAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        public async Task<ApiResponse<UserResponseDto>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            // Validate if any required field is null or empty, if so, return error response
            throw new NotImplementedException();
        }








        public Task<ApiResponse<string>> DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserResponseDto>> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
