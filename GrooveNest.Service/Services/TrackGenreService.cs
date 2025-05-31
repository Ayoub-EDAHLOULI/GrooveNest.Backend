using GrooveNest.Domain.DTOs.GenreDTOs;
using GrooveNest.Domain.DTOs.TrackDTOs;
using GrooveNest.Domain.DTOs.TrackGenreDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class TrackGenreService(ITrackGenreRepository trackGenreRepository, ITrackRepository trackRepository, IGenreRepository genreRepository) : ITrackGenreService
    {
        private readonly ITrackGenreRepository _trackGenreRepository = trackGenreRepository;
        private readonly ITrackRepository _trackRepository = trackRepository;
        private readonly IGenreRepository _genreRepository = genreRepository;


        // ---------------------------------------------------------------------------- //
        // ------------------------ CreateUserRoleAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------- //
        public async Task<ApiResponse<TrackGenreResponseDto>> CreateTrackGenreAsync(TrackGenreCreateDto trackGenreCreateDto)
        {
            // Check if the track exists
            var track = await _trackRepository.GetByIdAsync(trackGenreCreateDto.TrackId);
            if (track == null)
            {
                return ApiResponse<TrackGenreResponseDto>.ErrorResponse("Track not found");
            }

            // Check if the genre exists
            var genre = await _genreRepository.GetByIdAsync(trackGenreCreateDto.GenreId);
            if (genre == null)
            {
                return ApiResponse<TrackGenreResponseDto>.ErrorResponse("Genre not found");
            }

            // Check if the track genre already exists
            var existingTrackGenre = await _trackGenreRepository.GetByIdAsync(trackGenreCreateDto.TrackId, trackGenreCreateDto.GenreId);
            if (existingTrackGenre == null)
            {
                return ApiResponse<TrackGenreResponseDto>.ErrorResponse("Track already on this genre");
            }

            // Create the new track genre
            var newTrackGenre = new TrackGenre
            {
                TrackId = trackGenreCreateDto.TrackId,
                GenreId = trackGenreCreateDto.GenreId
            };

            await _trackGenreRepository.AddAsync(newTrackGenre);

            // Create the response DTO
            var trackGenreResponseDto = new TrackGenreResponseDto
            {
                TrackId = newTrackGenre.TrackId,
                GenreId = newTrackGenre.GenreId,
                TrackName = track.Title,
                GenreName = genre.Name,
                ArtistName = track.Artist?.Name ?? "Unknown Artist",
                AlbumName = track.Album?.Title ?? "Unknown Album"
            };

            return ApiResponse<TrackGenreResponseDto>.SuccessResponse(trackGenreResponseDto, "Track genre created successfully");
        }

        public Task<ApiResponse<string>> DeleteTrackGenreAsync(Guid trackId, Guid genreId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<GenreResponseDto>>> GetGenresByTrackIdAsync(Guid trackId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<TrackGenreResponseDto>> GetTrackGenreByIdAsync(Guid trackId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<List<TrackResponseDto>>> GetTracksByGenreIdAsync(Guid genreId)
        {
            throw new NotImplementedException();
        }
    }
}
