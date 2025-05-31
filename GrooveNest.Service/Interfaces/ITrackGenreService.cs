using GrooveNest.Domain.DTOs.GenreDTOs;
using GrooveNest.Domain.DTOs.TrackDTOs;
using GrooveNest.Domain.DTOs.TrackGenreDTOs;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Interfaces
{
    public interface ITrackGenreService
    {
        Task<ApiResponse<List<TrackGenreResponseDto>>> GetAllTrackGenresAsync();
        Task<ApiResponse<TrackGenreResponseDto>> GetTrackGenreByIdAsync(Guid trackId);
        Task<ApiResponse<TrackGenreResponseDto>> AddTrackGenreAsync(TrackGenreCreateDto trackGenreCreateDto);
        Task<ApiResponse<string>> DeleteTrackGenreAsync(Guid trackId, Guid genreId);
        Task<ApiResponse<List<GenreResponseDto>>> GetGenresByTrackIdAsync(Guid trackId);
        Task<ApiResponse<List<TrackResponseDto>>> GetTracksByGenreIdAsync(Guid genreId);
    }
}
