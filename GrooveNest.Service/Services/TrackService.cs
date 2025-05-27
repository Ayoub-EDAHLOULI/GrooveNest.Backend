using GrooveNest.Domain.DTOs.TrackDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;
using NAudio.Wave;

namespace GrooveNest.Service.Services
{
    public class TrackService(ITrackRepository trackRepository) : ITrackService
    {
        private readonly ITrackRepository _trackRepository = trackRepository;


        // ------------------------------------------------------------------------- //
        // ------------------------ CreateTrackAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<TrackResponseDto>> CreateTrackAsync(TrackCreateDto trackCreateDto)
        {
            // Validate the title
            if (!StringValidator.IsNullOrWhiteSpace(trackCreateDto.Title))
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Track title cannot be empty.");
            }

            // Validate the file
            if (trackCreateDto.AudioFile == null || trackCreateDto.AudioFile.Length == 0)
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Audio file is required.");
            }

            // Save the audio file
            var uploadsFolder = Path.Combine("wwwroot", "audio");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(trackCreateDto.AudioFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await trackCreateDto.AudioFile.CopyToAsync(stream);
            }

            var audioUrl = $"/audio/{uniqueFileName}";

            // Validate the duration


            // Create the track entity
            var track = new Track
            {
                Title = trackCreateDto.Title,
                DurationSec = trackCreateDto.DurationSec,
                TrackNumber = trackCreateDto.TrackNumber,
                AudioUrl = audioUrl,
                ArtistId = trackCreateDto.ArtistId,
                AlbumId = trackCreateDto.AlbumId
            };

            await _trackRepository.AddAsync(track);

            // Prepare the response DTO
            var trackDto = new TrackResponseDto
            {
                Id = track.Id,
                Title = track.Title,
                DurationSec = track.DurationSec,
                AudioUrl = track.AudioUrl,
                TrackNumber = track.TrackNumber,
                ArtistId = track.ArtistId,
                ArtistName = track.Artist?.Name ?? "Unknown Artist",
                AlbumId = track.AlbumId,
                AlbumTitle = track.Album?.Title
            };

            return ApiResponse<TrackResponseDto>.SuccessResponse(trackDto, "Track created successfully.");
        }


        public Task<string> DeleteTrackAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<TrackResponseDto>?> GetTrackByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<TrackResponseDto>?> GetTrackByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApiResponse<TrackResponseDto>>> GetTracksByAlbumIdAsync(Guid albumId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApiResponse<TrackResponseDto>>> GetTracksByAlbumTitleAsync(string albumTitle)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApiResponse<TrackResponseDto>>> GetTracksByArtistIdAsync(Guid artistId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApiResponse<TrackResponseDto>>> GetTracksByArtistNameAsync(string artistName)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<TrackResponseDto>> UpdateTrackAsync(Guid id, TrackUpdateDto trackUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
