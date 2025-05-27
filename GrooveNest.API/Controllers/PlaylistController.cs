using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController(PlaylistService playlistService) : ControllerBase
    {
        private readonly PlaylistService _playlistService = playlistService;


        // ------------------------------------------------------------------------- //
        // ------------------------ GetAllAlbumAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- //

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPlaylistAsync()
        {
            var response = await _playlistService.GetAllPlaylistsAsync();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
