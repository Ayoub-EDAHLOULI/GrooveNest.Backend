using GrooveNest.Domain.DTOs.PlaylistDTOs;
using GrooveNest.Domain.DTOs.UserDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Domain.Validators;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;
using static BCrypt.Net.BCrypt;

namespace GrooveNest.Service.Services
{
    public class UserService(IUserRepository userRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IUserRoleRepository _userRoleRepository = userRoleRepository;


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
                Status = user.Status,
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
            var users = await _userRepository.GetAllWithRolesAsync();

            if (users == null || !users.Any())
            {
                return ApiResponse<object>.ErrorResponse("No users found");
            }

            // Filter
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                users = users
                    .Where(u => u.UserName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                                u.Email.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var totalUsers = users.Count();

            // Pagination
            var paginatedUsers = users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(user => new UserResponseDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    Status = user.Status,
                    Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
                })
                .ToList();

            var response = new
            {
                PaginatedUsers = paginatedUsers,
                TotalUsers = totalUsers
            };

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
                Status = user.Status,
                CreatedAt = user.CreatedAt
            };
            // Return success response
            return ApiResponse<UserResponseDto>.SuccessResponse(userResponseDto, "User retrieved successfully");
        }


        // ------------------------------------------------------------------------ //
        // ------------------------ CreateUserAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 

        public async Task<ApiResponse<UserResponseDto>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            // Validate if any required field is null or empty, if so, return error response
            var validationResponse = UserValidator.ValidateCreate(userCreateDto);
            if (validationResponse != null)
            {
                return validationResponse;
            }

            // Check if username already exists
            var existingUserName = await _userRepository.GetByUserNameAsync(StringValidator.TrimOrEmpty(userCreateDto.UserName));
            if (existingUserName != null)
            {
                return ApiResponse<UserResponseDto>.ErrorResponse("Username already exists");
            }

            // Check if email already exists
            var existingUser = await _userRepository.GetByEmailAsync(StringValidator.TrimOrEmpty(userCreateDto.Email));
            if (existingUser != null)
            {
                return ApiResponse<UserResponseDto>.ErrorResponse("Email already exists");
            }

            // Bcrypt password
            var hashedPassword = HashPassword(StringValidator.TrimOrEmpty(userCreateDto.Password));

            // Create new user
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                UserName = StringValidator.TrimOrEmpty(userCreateDto.UserName),
                Email = StringValidator.TrimOrEmpty(userCreateDto.Email),
                Password = hashedPassword,
                Status = userCreateDto.Status,
                CreatedAt = DateTime.UtcNow
            };

            // Save new user to the database
            await _userRepository.AddAsync(newUser);

            // Get the 'listener' role
            var role = await _roleRepository.GetByNameAsync("listener");

            // Check if the role exists
            if (role == null)
            {
                return ApiResponse<UserResponseDto>.ErrorResponse("Default role 'listener' not found");
            }

            // Assign the 'listener' role to the new user
            var userRole = new UserRole
            {
                UserId = newUser.Id,
                RoleId = role.Id
            };

            // Save the user role to the database
            await _userRoleRepository.AddAsync(userRole);

            // Map new user to UserResponseDto
            var userResponseDto = new UserResponseDto
            {
                Id = newUser.Id,
                UserName = newUser.UserName,
                Email = newUser.Email,
                Status = newUser.Status,
                CreatedAt = newUser.CreatedAt
            };

            // Validate if userResponseDto is null
            if (userResponseDto == null)
            {
                return ApiResponse<UserResponseDto>.ErrorResponse("Failed to create user");
            }

            // Return success response
            return ApiResponse<UserResponseDto>.SuccessResponse(userResponseDto, "User created successfully");
        }


        // ------------------------------------------------------------------------ //
        // ------------------------ UpdateUserAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 
        public async Task<ApiResponse<UserResponseDto>> UpdateUserAsync(Guid id, UserUpdateDto userUpdateDto)
        {
            // Check if user exists
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<UserResponseDto>.ErrorResponse("User not found");
            }

            // Update only provided fields
            if (!string.IsNullOrEmpty(userUpdateDto.UserName))
            {
                // Trim whitespace from username
                var trimmedUserName = StringValidator.TrimOrEmpty(userUpdateDto.UserName);

                // Check if username already exists
                var existingUserName = await _userRepository.GetByUserNameAsync(trimmedUserName);
                if (existingUserName != null && existingUserName.Id != user.Id)
                {
                    return ApiResponse<UserResponseDto>.ErrorResponse("Username already exists");
                }
                user.UserName = trimmedUserName;
            }

            if (!string.IsNullOrEmpty(userUpdateDto.Email))
            {
                // Trim whitespace from email
                var trimmedEmail = StringValidator.TrimOrEmpty(userUpdateDto.Email);

                // Check if email already exists
                var existingUserEmail = await _userRepository.GetByEmailAsync(trimmedEmail);
                if (existingUserEmail != null && existingUserEmail.Id != user.Id)
                {
                    return ApiResponse<UserResponseDto>.ErrorResponse("Email already exists");
                }

                // Validate if email is valid
                if (!IsValidEmail.IsValid(trimmedEmail))
                {
                    return ApiResponse<UserResponseDto>.ErrorResponse("Email is not valid");
                }

                user.Email = trimmedEmail;
            }

            if (!string.IsNullOrEmpty(userUpdateDto.Password))
            {
                // Trim whitespace from password
                var trimmedPassword = StringValidator.TrimOrEmpty(userUpdateDto.Password);
                // Validate if password is valid
                if (trimmedPassword.Length < 6)
                {
                    return ApiResponse<UserResponseDto>.ErrorResponse("Password must be at least 6 characters long");
                }
                user.Password = HashPassword(trimmedPassword);
            }

            // Update user status if provided
            if (userUpdateDto.Status.HasValue)
            {
                user.Status = userUpdateDto.Status.Value;
            }

            // Update user in the database
            await _userRepository.UpdateAsync(user);

            // Map updated user to UserResponseDto
            var userResponseDto = new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Status = user.Status,
                Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList(),
                CreatedAt = user.CreatedAt
            };

            // Return success response
            return ApiResponse<UserResponseDto>.SuccessResponse(userResponseDto, "User updated successfully");
        }


        // ------------------------------------------------------------------------ //
        // ------------------------ DeleteUserAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 
        public async Task<ApiResponse<string>> DeleteUserAsync(Guid id)
        {
            // Check if user exists
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<string>.ErrorResponse("User not found");
            }
            // Delete user from the database
            await _userRepository.DeleteAsync(user);
            // Validate if user is deleted
            var deletedUser = await _userRepository.GetByIdAsync(id);
            if (deletedUser != null)
            {
                return ApiResponse<string>.ErrorResponse("Failed to delete user");
            }
            // Return success response
            return ApiResponse<string>.SuccessResponse("User deleted successfully");
        }


        // ------------------------------------------------------------------------------------ //
        // ------------------------ GetUserByIdWithDetailsAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------------ //
        public async Task<ApiResponse<object>> GetUserByIdWithDetailsAsync(Guid id)
        {
            // Check if user exists
            var user = await _userRepository.GetUserDetails(id);
            if (user == null)
            {
                return ApiResponse<object>.ErrorResponse("User not found");
            }

            // Map to UserResponseDetails DTO
            var userResponseDetails = new UserResponseDetails
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Status = user.Status,
                CreatedAt = user.CreatedAt,
                Roles = [.. user.UserRoles.Select(ur => ur.Role.Name)],
                Playlists = [.. user.Playlists.Select(p => new PlaylistResponseDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CreatedAt = p.CreatedAt
                })]
            };

            // Calculate the total playlist count
            var totalPlaylists = userResponseDetails.Playlists.Count;


            // Validate if userResponseDetails is null
            if (userResponseDetails == null)
            {
                return ApiResponse<object>.ErrorResponse("Failed to retrieve user details");
            }

            var response = new
            {
                UserDetails = userResponseDetails,
                TotalPlaylists = totalPlaylists
            };

            // Return success response
            return ApiResponse<object>.SuccessResponse(response, "User details retrieved successfully");
        }

    }
}
