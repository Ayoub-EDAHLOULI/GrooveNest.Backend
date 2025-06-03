using GrooveNest.Domain.DTOs.LoginDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class AuthenticationService(IUserRepository userRepository) : IAuthenticationService<LoginDto, string>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public Task<ApiResponse<string>> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
