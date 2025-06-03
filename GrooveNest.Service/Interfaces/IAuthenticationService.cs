using GrooveNest.Domain.DTOs.UserDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IAuthenticationService<TLoginDto, TResponseDto>
    {
        Task<ApiResponse<TResponseDto>> LoginAsync(TLoginDto loginDto);
        Task<ApiResponse<TResponseDto>> RegisterAsync(UserCreateDto registerDto);
    }
}
