using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class LikeService(ILikeRepository likeRepository, IUserRepository userRepository, ITrackRepository trackRepository) : ILikeService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITrackRepository _trackRepository = trackRepository;
        private readonly ILikeRepository _likeRepository = likeRepository;

        public Task<ApiResponse<string>> CreateLikeAsync(Guid trackId, Guid UserId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> DeleteLikeAsync(Guid trackId, Guid UserId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<object>> GetTrackLikesAsync(Guid trackId)
        {
            throw new NotImplementedException();
        }
    }
}
