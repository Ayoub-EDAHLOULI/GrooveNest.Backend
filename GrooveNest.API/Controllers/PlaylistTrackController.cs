using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistTrackController(PlaylistTrackService playlistTrackService) : ControllerBase
    {
        private readonly PlaylistTrackService _playlistTrackService = playlistTrackService;


        // -------------------------------------------------------------------------------- //
        // ------------------------ AddTrackToPlaylistAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------------- // 

        [HttpPost("{playlistId}/{trackId}")]
        public async Task<IActionResult> AddTrackToPlaylistAsync(Guid playlistId, Guid trackId)
        {
            var response = await _playlistTrackService.AddTrackToPlaylistAsync(playlistId, trackId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ----------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByPlaylistIdAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------------- // 

        [HttpGet("{playlistId}")]
        public async Task<IActionResult> GetTracksByPlaylistIdAsync(Guid playlistId)
        {
            var response = await _playlistTrackService.GetTracksByPlaylistIdAsync(playlistId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ----------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByPlaylistIdAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------------- // 

        [HttpDelete("{playlistId}/{trackId}")]
        public async Task<IActionResult> RemoveTrackFromPlaylistAsync(Guid playlistId, Guid trackId)
        {
            var response = await _playlistTrackService.RemoveTrackFromPlaylistAsync(playlistId, trackId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
