using GrooveNest.Domain.DTOs.AlbumDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface IAlbumService
    {
        Task<ApiResponse<AlbumResponseDto>> GetAllAlbumAsync();
        Task<ApiResponse<object>> GetAllPaginatedAlbumAsync(int page = 1, int pageSize = 10, string searchQuery = "");
        Task<ApiResponse<AlbumResponseDto>> GetAlbumByIdAsync(Guid id);
        Task<ApiResponse<AlbumResponseDto>> CreateAlbumAsync(AlbumCreateDto albumCreateDto);
        Task<ApiResponse<AlbumResponseDto>> UpdateAlbumAsync(Guid id, AlbumUpdateDto albumUpdateDto);
        Task<ApiResponse<string>> DeleteAlbumAsync(Guid id);
    }
}
