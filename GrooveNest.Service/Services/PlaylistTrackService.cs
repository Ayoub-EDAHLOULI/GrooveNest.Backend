using GrooveNest.Domain.DTOs.PlaylistTrackDTOs;
using GrooveNest.Domain.Entities;
using GrooveNest.Repository.Interfaces;
using GrooveNest.Service.Interfaces;
using GrooveNest.Utilities;

namespace GrooveNest.Service.Services
{
    public class PlaylistTrackService(IPlaylistTrackRepository playlistTrackRepository, IPlaylistRepository playlistRepository, ITrackRepository trackRepository) : IPlaylistTrackService
    {
        private readonly IPlaylistTrackRepository _playlistTrackRepository = playlistTrackRepository;
        private readonly IPlaylistRepository _playlistRepository = playlistRepository;
        private readonly ITrackRepository _trackRepository = trackRepository;


        // -------------------------------------------------------------------------------- //
        // ------------------------ AddTrackToPlaylistAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<PlaylistTrackResponseDto>> AddTrackToPlaylistAsync(Guid playlistId, Guid trackId)
        {
            // Check if the playlist exists
            var playlist = await _playlistRepository.GetByIdAsync(playlistId);
            if (playlist == null)
            {
                return ApiResponse<PlaylistTrackResponseDto>.ErrorResponse("Playlist not found.");
            }

            // Check if the track exists
            var track = await _trackRepository.GetByIdAsync(trackId);
            if (track == null)
            {
                return ApiResponse<PlaylistTrackResponseDto>.ErrorResponse("Track not found.");
            }

            // Check if the track is already in the playlist
            var existingTrack = await _playlistTrackRepository.GetByPlaylistAndTrackAsync(playlistId, trackId);
            if (existingTrack != null)
            {
                return ApiResponse<PlaylistTrackResponseDto>.ErrorResponse("Track is already in the playlist.");
            }

            // Create a new PlaylistTrack entity
            var playlistTrack = new PlaylistTrack
            {
                PlaylistId = playlistId,
                TrackId = trackId,
                TrackNumber = await _playlistTrackRepository.GetNextTrackNumberAsync(playlistId)
            };

            // Add the track to the playlist
            await _playlistTrackRepository.AddAsync(playlistTrack);

            // Create a response DTO
            var playlistTrackResponse = new PlaylistTrackResponseDto
            {
                PlaylistId = playlistTrack.PlaylistId,
                PlaylistTitle = playlist.Name,
                TrackId = playlistTrack.TrackId,
                TrackTitle = track.Title,
                TrackNumber = playlistTrack.TrackNumber,
            };

            // Return the added track with a success response
            return ApiResponse<PlaylistTrackResponseDto>.SuccessResponse(playlistTrackResponse, "Track added to playlist successfully.");
        }



        // ----------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByPlaylistIdAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------------- // 
        public async Task<ApiResponse<List<PlaylistTrackResponseDto>>> GetTracksByPlaylistIdAsync(Guid playlistId)
        {
            // Check if the playlist exists
            var playlist = await _playlistRepository.GetByIdAsync(playlistId);
            if (playlist == null)
            {
                return ApiResponse<List<PlaylistTrackResponseDto>>.ErrorResponse("Playlist not found.");
            }
            // Get all tracks in the playlist
            var tracks = await _playlistTrackRepository.GetTracksByPlaylistIdAsync(playlistId);
            // Map the tracks to response DTOs
            var trackResponses = tracks.Select(pt => new PlaylistTrackResponseDto
            {
                PlaylistId = pt.PlaylistId,
                PlaylistTitle = playlist.Name,
                TrackId = pt.TrackId,
                TrackTitle = pt.Track.Title,
                TrackNumber = pt.TrackNumber
            }).ToList();
            // Return the list of tracks with a success response
            return ApiResponse<List<PlaylistTrackResponseDto>>.SuccessResponse(trackResponses, "Tracks retrieved successfully.");
        }

        public Task<ApiResponse<string>> RemoveTrackFromPlaylistAsync(Guid playlistId, Guid trackId)
        {
            throw new NotImplementedException();
        }
    }
}
