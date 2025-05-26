using GrooveNest.Domain.DTOs.AlbumDTOs;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class AlbumService(IAlbumRepository albumRepository) : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository = albumRepository;

        public Task<ApiResponse<AlbumResponseDto>> CreateAlbumAsync(AlbumCreateDto albumCreateDto)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> DeleteAlbumAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<AlbumResponseDto>> GetAlbumByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<AlbumResponseDto>> GetAllAlbumAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<object>> GetAllPaginatedAlbumAsync(int page = 1, int pageSize = 10, string searchQuery = "")
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<AlbumResponseDto>> UpdateAlbumAsync(Guid id, AlbumUpdateDto albumUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
