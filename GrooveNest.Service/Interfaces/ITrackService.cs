using GrooveNest.Domain.DTOs.TrackDTOs;
using GrooveNest.Utilities;
using Microsoft.AspNetCore.Http;

namespace GrooveNest.Service.Interfaces
{
    public interface ITrackService
    {
        Task<ApiResponse<TrackResponseDto>?> GetTrackByIdAsync(Guid id);
        Task<ApiResponse<TrackResponseDto>?> GetTrackByTitleAsync(string title);
        Task<IEnumerable<ApiResponse<TrackResponseDto>>> GetTracksByArtistIdAsync(Guid artistId);
        Task<IEnumerable<ApiResponse<TrackResponseDto>>> GetTracksByAlbumIdAsync(Guid albumId);
        Task<IEnumerable<ApiResponse<TrackResponseDto>>> GetTracksByArtistNameAsync(string artistName);
        Task<IEnumerable<ApiResponse<TrackResponseDto>>> GetTracksByAlbumTitleAsync(string albumTitle);
        Task<ApiResponse<TrackResponseDto>> CreateTrackAsync(TrackCreateDto trackCreateDto, IFormFile formFile);
        Task<ApiResponse<TrackResponseDto>> UpdateTrackAsync(Guid id, TrackUpdateDto trackUpdateDto);
        Task<string> DeleteTrackAsync(Guid id);
    }
}
