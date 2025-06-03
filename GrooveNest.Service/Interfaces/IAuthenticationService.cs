using GrooveNest.Domain.DTOs.UserDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IAuthenticationService<TLoginDto, TRespoUserAuthResponseDtonseDto>
    {
        Task<ApiResponse<UserAuthResponseDto>> LoginAsync(TLoginDto loginDto);
        Task<ApiResponse<UserAuthResponseDto>> RegisterAsync(UserCreateDto registerDto);
    }
}
