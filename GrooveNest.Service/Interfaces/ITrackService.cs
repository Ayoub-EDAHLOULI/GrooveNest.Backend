using GrooveNest.Domain.DTOs.TrackDTOs;
using GrooveNest.Utilities;
using Microsoft.AspNetCore.Http;

namespace GrooveNest.Service.Interfaces
{
    public interface ITrackService
    {
        Task<ApiResponse<TrackResponseDto>?> GetTrackByIdAsync(Guid id);
        Task<ApiResponse<TrackResponseDto>?> GetTrackByTitleAsync(string title);
        Task<ApiResponse<IEnumerable<TrackResponseDto>>> GetTracksByArtistIdAsync(Guid artistId);
        Task<ApiResponse<IEnumerable<TrackResponseDto>>> GetTracksByAlbumIdAsync(Guid albumId);
        Task<ApiResponse<IEnumerable<TrackResponseDto>>> GetTracksByArtistNameAsync(string artistName);
        Task<ApiResponse<IEnumerable<TrackResponseDto>>> GetTracksByAlbumTitleAsync(string albumTitle);
        Task<ApiResponse<TrackResponseDto>> CreateTrackAsync(TrackCreateDto trackCreateDto, IFormFile formFile);
        Task<ApiResponse<TrackResponseDto>> UpdateTrackAsync(Guid id, TrackUpdateDto trackUpdateDto);
        Task<ApiResponse<string>> DeleteTrackAsync(Guid id);
    }
}
