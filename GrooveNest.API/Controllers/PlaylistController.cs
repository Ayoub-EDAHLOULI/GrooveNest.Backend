using GrooveNest.Domain.DTOs.PlaylistDTOs;
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


        // -------------------------------------------------------------------------------------- //
        // ------------------------ GetAllPaginatedPlaylistsAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------------------- //

        [HttpGet]
        public async Task<IActionResult> GetAllPaginatedPlaylistsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var response = await _playlistService.GetAllPaginatedPlaylistsAsync(page, pageSize, search);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ----------------------------------------------------------------------------- //
        // ------------------------ GetPlaylistByIdAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------- //

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPlaylistByIdAsync(Guid id)
        {
            var response = await _playlistService.GetPlaylistByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ---------------------------------------------------------------------------- //
        // ------------------------ CreatePlaylistAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------- //

        [HttpPost]
        public async Task<IActionResult> CreatePlaylistAsync([FromBody] PlaylistCreateDto playlistCreateDto)
        {
            var response = await _playlistService.CreatePlaylistAsync(playlistCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }




    }
}
