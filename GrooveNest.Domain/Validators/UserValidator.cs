using GrooveNest.Domain.DTOs.UserDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Domain.Validators
{
    public static class UserValidator
    {
        public static ApiResponse<UserResponseDto>? ValidateCreate(UserCreateDto userCreateDto)
        {
            var requiredFields = new Dictionary<string, string?>
            {
                { nameof(userCreateDto.UserName), userCreateDto.UserName },
                { nameof(userCreateDto.Email), userCreateDto.Email },
                { nameof(userCreateDto.Password), userCreateDto.Password}
            };

            foreach (var field in requiredFields)
            {
                if (StringValidator.IsNullOrWhiteSpace(field.Value))
                {
                    return ApiResponse<UserResponseDto>.ErrorResponse($"{field.Key} is required.");
                }
            }

            if (userCreateDto.UserName.Length < 3)
            {
                return ApiResponse<UserResponseDto>.ErrorResponse("UserName must be at least 3 characters long.");
            }

            if (userCreateDto.Password.Length < 6)
            {
                return ApiResponse<UserResponseDto>.ErrorResponse("Password must be at least 6 characters long.");
            }

            if (!IsValidEmail.IsValid(userCreateDto.Email))
            {
                return ApiResponse<UserResponseDto>.ErrorResponse("Email is not valid.");
            }

            return null;
        }
    }
}
