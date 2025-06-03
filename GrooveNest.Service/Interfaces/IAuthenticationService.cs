using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IAuthenticationService<TLoginDto, TResponseDto>
    {
        Task<ApiResponse<TResponseDto>> LoginAsync(TLoginDto loginDto);
    }
}
