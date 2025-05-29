using GrooveNest.Domain.DTOs.TrackDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace GrooveNest.Service.Services
{
    public class TrackService(ITrackRepository trackRepository, IAlbumRepository albumRepository, IArtistRepository artistRepository) : ITrackService
    {
        private readonly ITrackRepository _trackRepository = trackRepository;
        private readonly IAlbumRepository _albumRepository = albumRepository;
        private readonly IArtistRepository _artistRepository = artistRepository;


        // ------------------------------------------------------------------------- //
        // ------------------------ CreateTrackAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<TrackResponseDto>> CreateTrackAsync(TrackCreateDto trackCreateDto, IFormFile AudioFile)
        {
            // Validate title
            if (!StringValidator.IsNullOrWhiteSpace(trackCreateDto.Title))
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Track title cannot be empty.");
            }

            // Check if track with the same title already exists
            var existingTrack = await _trackRepository.GetTrackByTitleAsync(trackCreateDto.Title);
            if (existingTrack != null)
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("A track with this title already exists.");
            }

            // Validate track number
            if (trackCreateDto.TrackNumber <= 0)
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Track number must be greater than zero.");
            }

            // Validate artist and album IDs
            if (trackCreateDto.ArtistId == Guid.Empty)
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Artist ID is required.");
            }

            // Check if artist exists
            var artistExists = await _artistRepository.GetByIdAsync(trackCreateDto.ArtistId);
            if (artistExists == null)
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Artist does not exist.");
            }

            if (trackCreateDto.AlbumId != Guid.Empty)
            {
                // Check if album exists
                var albumExists = await _albumRepository.GetByIdAsync(trackCreateDto.AlbumId);
                if (albumExists == null)
                {
                    return ApiResponse<TrackResponseDto>.ErrorResponse("Album does not exist.");
                }
            }

            // Ensure audio file is provided
            if (AudioFile == null || AudioFile.Length == 0)
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Audio file is required.");
            }

            var allowedTypes = new[] { "audio/mpeg", "audio/mp3", "audio/wav" };
            if (!allowedTypes.Contains(AudioFile.ContentType))
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Invalid audio file type.");
            }


            // Save to memory stream for duration extraction
            using var memoryStream = new MemoryStream();
            await AudioFile.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            int durationSec = AudioHelper.GetAudioDurationInSeconds(memoryStream);

            // Save the file to disk and get relative URL
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(AudioFile.FileName)}";
            var relativeAudioUrl = FileHelper.SaveFile(memoryStream, uniqueFileName, "uploads/tracks");


            // Create new track
            var newTrack = new Track
            {
                Title = trackCreateDto.Title,
                TrackNumber = trackCreateDto.TrackNumber,
                AudioUrl = relativeAudioUrl,
                DurationSec = durationSec,
                ArtistId = trackCreateDto.ArtistId,
                AlbumId = trackCreateDto.AlbumId
            };


            await _trackRepository.AddAsync(newTrack);

            var responseDto = new TrackResponseDto
            {
                Id = newTrack.Id,
                Title = newTrack.Title,
                DurationSec = newTrack.DurationSec,
                AudioUrl = newTrack.AudioUrl,
                TrackNumber = newTrack.TrackNumber,
                ArtistId = newTrack.ArtistId,
                ArtistName = artistExists.Name,
                AlbumId = newTrack.AlbumId,
                AlbumTitle = newTrack.AlbumId.HasValue ? (await _albumRepository.GetByIdAsync(newTrack.AlbumId.Value))?.Title : null
            };

            return ApiResponse<TrackResponseDto>.SuccessResponse(responseDto, "Track created successfully.");
        }


        // ----------------------------------------------------------------------------- //
        // ------------------------ GetTrackByTitleAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------- // 
        public async Task<ApiResponse<TrackResponseDto>?> GetTrackByTitleAsync(string title)
        {
            if (StringValidator.IsNullOrWhiteSpace(title))
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Track title cannot be empty.");
            }

            var track = await _trackRepository.GetTrackByTitleAsync(title);
            if (track == null)
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Track not found.");
            }

            var artist = await _artistRepository.GetByIdAsync(track.ArtistId);
            var album = track.AlbumId.HasValue ? await _albumRepository.GetByIdAsync(track.AlbumId.Value) : null;

            var responseDto = new TrackResponseDto
            {
                Id = track.Id,
                Title = track.Title,
                DurationSec = track.DurationSec,
                AudioUrl = track.AudioUrl,
                TrackNumber = track.TrackNumber,
                ArtistId = track.ArtistId,
                ArtistName = artist?.Name ?? string.Empty,
                AlbumId = track.AlbumId,
                AlbumTitle = album?.Title
            };

            return ApiResponse<TrackResponseDto>.SuccessResponse(responseDto, "Track retrieved successfully.");
        }


        // ----------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByAlbumTitleAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<List<TrackResponseDto>>> GetTracksByAlbumTitleAsync(string albumTitle)
        {
            if (StringValidator.IsNullOrWhiteSpace(albumTitle))
            {
                return ApiResponse<List<TrackResponseDto>>.ErrorResponse("Album title cannot be empty.");
            }

            var tracks = await _trackRepository.GetTracksByAlbumTitleAsync(albumTitle);
            if (tracks == null || !tracks.Any())
            {
                return ApiResponse<List<TrackResponseDto>>.ErrorResponse("No tracks found for this album title.");
            }

            var responses = new List<TrackResponseDto>();
            foreach (var track in tracks)
            {
                var artist = await _artistRepository.GetByIdAsync(track.ArtistId);
                var album = track.AlbumId.HasValue ? await _albumRepository.GetByIdAsync(track.AlbumId.Value) : null;

                var responseDto = new TrackResponseDto
                {
                    Id = track.Id,
                    Title = track.Title,
                    DurationSec = track.DurationSec,
                    AudioUrl = track.AudioUrl,
                    TrackNumber = track.TrackNumber,
                    ArtistId = track.ArtistId,
                    ArtistName = artist?.Name ?? string.Empty,
                    AlbumId = track.AlbumId,
                    AlbumTitle = album?.Title
                };

                responses.Add(responseDto);
            }

            return ApiResponse<List<TrackResponseDto>>.SuccessResponse(responses, "Tracks retrieved successfully.");
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ GetTrackByIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 
        public async Task<ApiResponse<TrackResponseDto>?> GetTrackByIdAsync(Guid id)
        {
            var track = await _trackRepository.GetByIdAsync(id);
            if (track == null)
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Track not found.");
            }
            var artist = await _artistRepository.GetByIdAsync(track.ArtistId);
            var album = track.AlbumId.HasValue ? await _albumRepository.GetByIdAsync(track.AlbumId.Value) : null;
            var responseDto = new TrackResponseDto
            {
                Id = track.Id,
                Title = track.Title,
                DurationSec = track.DurationSec,
                AudioUrl = track.AudioUrl,
                TrackNumber = track.TrackNumber,
                ArtistId = track.ArtistId,
                ArtistName = artist?.Name ?? string.Empty,
                AlbumId = track.AlbumId,
                AlbumTitle = album?.Title
            };
            return ApiResponse<TrackResponseDto>.SuccessResponse(responseDto, "Track retrieved successfully.");
        }


        // -------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByAlbumIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<IEnumerable<TrackResponseDto>>> GetTracksByAlbumIdAsync(Guid albumId)
        {
            var tracks = await _trackRepository.GetTracksByAlbumIdAsync(albumId);
            if (tracks == null || !tracks.Any())
            {
                return ApiResponse<IEnumerable<TrackResponseDto>>.ErrorResponse("No tracks found for this album ID.");
            }

            var responses = tracks.Select(track =>
            {
                var artist = _artistRepository.GetByIdAsync(track.ArtistId).Result;
                var album = track.AlbumId.HasValue ? _albumRepository.GetByIdAsync(track.AlbumId.Value).Result : null;
                return new TrackResponseDto
                {
                    Id = track.Id,
                    Title = track.Title,
                    DurationSec = track.DurationSec,
                    AudioUrl = track.AudioUrl,
                    TrackNumber = track.TrackNumber,
                    ArtistId = track.ArtistId,
                    ArtistName = artist?.Name ?? string.Empty,
                    AlbumId = track.AlbumId,
                    AlbumTitle = album?.Title
                };
            });

            return ApiResponse<IEnumerable<TrackResponseDto>>.SuccessResponse(responses, "Tracks retrieved successfully.");
        }


        // --------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByArtistIdAsync METHODS ----------------------- //
        // --------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<IEnumerable<TrackResponseDto>>> GetTracksByArtistIdAsync(Guid artistId)
        {
            var tracks = await _trackRepository.GetTracksByArtistIdAsync(artistId);
            if (tracks == null || !tracks.Any())
            {
                return ApiResponse<IEnumerable<TrackResponseDto>>.ErrorResponse("No tracks found for this artist ID.");
            }
            var responses = tracks.Select(track =>
            {
                var artist = _artistRepository.GetByIdAsync(track.ArtistId).Result;
                var album = track.AlbumId.HasValue ? _albumRepository.GetByIdAsync(track.AlbumId.Value).Result : null;
                return new TrackResponseDto
                {
                    Id = track.Id,
                    Title = track.Title,
                    DurationSec = track.DurationSec,
                    AudioUrl = track.AudioUrl,
                    TrackNumber = track.TrackNumber,
                    ArtistId = track.ArtistId,
                    ArtistName = artist?.Name ?? string.Empty,
                    AlbumId = track.AlbumId,
                    AlbumTitle = album?.Title
                };
            });
            return ApiResponse<IEnumerable<TrackResponseDto>>.SuccessResponse(responses, "Tracks retrieved successfully.");
        }


        // ----------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByArtistNameAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<IEnumerable<TrackResponseDto>>> GetTracksByArtistNameAsync(string artistName)
        {
            var tracks = await _trackRepository.GetTracksByArtistNameAsync(artistName);
            if (tracks == null || !tracks.Any())
            {
                return ApiResponse<IEnumerable<TrackResponseDto>>.ErrorResponse("No tracks found for this artist name.");
            }

            var responses = tracks.Select(track =>
            {
                var artist = _artistRepository.GetByIdAsync(track.ArtistId).Result;
                var album = track.AlbumId.HasValue ? _albumRepository.GetByIdAsync(track.AlbumId.Value).Result : null;
                return new TrackResponseDto
                {
                    Id = track.Id,
                    Title = track.Title,
                    DurationSec = track.DurationSec,
                    AudioUrl = track.AudioUrl,
                    TrackNumber = track.TrackNumber,
                    ArtistId = track.ArtistId,
                    ArtistName = artist?.Name ?? string.Empty,
                    AlbumId = track.AlbumId,
                    AlbumTitle = album?.Title
                };
            });

            return ApiResponse<IEnumerable<TrackResponseDto>>.SuccessResponse(responses, "Tracks retrieved successfully.");
        }


        // ----------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByArtistNameAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<IEnumerable<TrackResponseDto>>> GetTracksByArtistIdAsync(Guid artistId)
        {
            var tracks = await _trackRepository.GetTracksByArtistIdAsync(artistId);
            if (tracks == null || !tracks.Any())
            {
                return ApiResponse<IEnumerable<TrackResponseDto>>.ErrorResponse("No tracks found for this artist ID.");
            }

            var responses = tracks.Select(track =>
            {
                var artist = _artistRepository.GetByIdAsync(track.ArtistId).Result;
                var album = track.AlbumId.HasValue ? _albumRepository.GetByIdAsync(track.AlbumId.Value).Result : null;
                return new TrackResponseDto
                {
                    Id = track.Id,
                    Title = track.Title,
                    DurationSec = track.DurationSec,
                    AudioUrl = track.AudioUrl,
                    TrackNumber = track.TrackNumber,
                    ArtistId = track.ArtistId,
                    ArtistName = artist?.Name ?? string.Empty,
                    AlbumId = track.AlbumId,
                    AlbumTitle = album?.Title
                };
            });

            return ApiResponse<IEnumerable<TrackResponseDto>>.SuccessResponse(responses, "Tracks retrieved successfully.");
        }





        Task<ApiResponse<IEnumerable<TrackResponseDto>>> ITrackService.GetTracksByAlbumTitleAsync(string albumTitle)
        {
            throw new NotImplementedException();
        }









        public Task<string> DeleteTrackAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<TrackResponseDto>> UpdateTrackAsync(Guid id, TrackUpdateDto trackUpdateDto)
        {
            throw new NotImplementedException();
        }


        public static string SaveFile(IFormFile file, string subDirectory)
        {
            var fileName = Path.GetFileName(file.FileName);
            var savePath = Path.Combine("wwwroot", subDirectory, fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);
            using var stream = new FileStream(savePath, FileMode.Create);
            file.CopyTo(stream);
            return $"/{subDirectory}/{fileName}";
        }
    }
}
