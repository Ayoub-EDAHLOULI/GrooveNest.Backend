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
            if (StringValidator.IsNullOrWhiteSpace(trackCreateDto.Title))
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

            if (trackCreateDto.AlbumId.HasValue)
            {
                var albumExists = await _albumRepository.GetByIdAsync(trackCreateDto.AlbumId.Value);
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
                IsPublished = trackCreateDto.IsPublished,
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
                AlbumTitle = newTrack.AlbumId.HasValue
                            ? (await _albumRepository.GetByIdAsync(newTrack.AlbumId.Value))?.Title
                            : null

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
        public async Task<ApiResponse<IEnumerable<TrackResponseDto>>> GetTracksByAlbumTitleAsync(string albumTitle)
        {
            if (StringValidator.IsNullOrWhiteSpace(albumTitle))
            {
                return ApiResponse<IEnumerable<TrackResponseDto>>.ErrorResponse("Album title cannot be empty.");
            }

            var tracks = await _trackRepository.GetTracksByAlbumTitleAsync(albumTitle);
            if (tracks == null || !tracks.Any())
            {
                return ApiResponse<IEnumerable<TrackResponseDto>>.ErrorResponse("No tracks found for this album title.");
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

            return ApiResponse<IEnumerable<TrackResponseDto>>.SuccessResponse(responses, "Tracks retrieved successfully.");
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


        // ------------------------------------------------------------------------- //
        // ------------------------ UpdateTrackAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<TrackResponseDto>> UpdateTrackAsync(Guid id, TrackUpdateDto trackUpdateDto)
        {
            // Check if track exists
            var existingTrack = await _trackRepository.GetByIdAsync(id);
            if (existingTrack == null)
            {
                return ApiResponse<TrackResponseDto>.ErrorResponse("Track not found.");
            }

            // Validate and update title
            if (!StringValidator.IsNullOrWhiteSpace(trackUpdateDto.Title))
            {
                var trimmedTitle = StringValidator.TrimOrEmpty(trackUpdateDto.Title);
                if (trimmedTitle!.Length < 1 || trimmedTitle.Length > 100)
                {
                    return ApiResponse<TrackResponseDto>.ErrorResponse("Track title must be between 1 and 100 characters.");
                }

                var existingTitleTrack = await _trackRepository.GetTrackByTitleAsync(trimmedTitle);
                if (existingTitleTrack != null && existingTitleTrack.Id != id)
                {
                    return ApiResponse<TrackResponseDto>.ErrorResponse("A track with this title already exists.");
                }

                existingTrack.Title = trimmedTitle;
            }

            // Update track number
            if (trackUpdateDto.TrackNumber.HasValue && trackUpdateDto.TrackNumber.Value > 0)
            {
                existingTrack.TrackNumber = trackUpdateDto.TrackNumber.Value;
            }

            // Update file if provided
            if (trackUpdateDto.AudioFile != null && trackUpdateDto.AudioFile.Length > 0)
            {
                var allowedTypes = new[] { "audio/mpeg", "audio/mp3", "audio/wav" };
                if (!allowedTypes.Contains(trackUpdateDto.AudioFile.ContentType))
                {
                    return ApiResponse<TrackResponseDto>.ErrorResponse("Invalid audio file type.");
                }
                // Save to memory stream for duration extraction
                using var memoryStream = new MemoryStream();
                await trackUpdateDto.AudioFile.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                int durationSec = AudioHelper.GetAudioDurationInSeconds(memoryStream);
                // Save the file to disk and get relative URL
                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(trackUpdateDto.AudioFile.FileName)}";
                var relativeAudioUrl = FileHelper.SaveFile(memoryStream, uniqueFileName, "uploads/tracks");
                existingTrack.AudioUrl = relativeAudioUrl;
                existingTrack.DurationSec = durationSec;
            }

            await _trackRepository.UpdateAsync(existingTrack);

            var responseDto = new TrackResponseDto
            {
                Id = existingTrack.Id,
                Title = existingTrack.Title,
                DurationSec = existingTrack.DurationSec,
                AudioUrl = existingTrack.AudioUrl,
                TrackNumber = existingTrack.TrackNumber,
                ArtistId = existingTrack.ArtistId,
                ArtistName = (await _artistRepository.GetByIdAsync(existingTrack.ArtistId))?.Name ?? string.Empty,
                AlbumId = existingTrack.AlbumId,
                AlbumTitle = existingTrack.AlbumId.HasValue ? (await _albumRepository.GetByIdAsync(existingTrack.AlbumId.Value))?.Title : null
            };

            return ApiResponse<TrackResponseDto>.SuccessResponse(responseDto, "Track updated successfully.");
        }


        // ------------------------------------------------------------------------- //
        // ------------------------ DeleteTrackAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 
        public async Task<ApiResponse<string>> DeleteTrackAsync(Guid id)
        {
            // Check if track exists
            var existingTrack = await _trackRepository.GetByIdAsync(id);
            if (existingTrack == null)
            {
                return ApiResponse<string>.ErrorResponse("Track not found.");
            }
            // Delete the track
            await _trackRepository.DeleteAsync(existingTrack);
            // Optionally, delete the audio file from disk
            if (!string.IsNullOrEmpty(existingTrack.AudioUrl))
            {
                var filePath = Path.Combine("wwwroot", existingTrack.AudioUrl.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            return ApiResponse<string>.SuccessResponse(string.Empty, "Track deleted successfully.");
        }


        // ----------------------------------------------------------------- //
        // ------------------------ SaveFile METHODS ----------------------- //
        // ----------------------------------------------------------------- // 
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
