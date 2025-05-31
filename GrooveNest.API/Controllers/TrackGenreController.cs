using GrooveNest.Domain.DTOs.TrackGenreDTOs;
using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackGenreController(TrackGenreService trackGenreService) : ControllerBase
    {
        private readonly TrackGenreService _trackGenreService = trackGenreService;


        // ------------------------------------------------------------------------------ //
        // ------------------------ CreateTrackGenreAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------ //

        [HttpPost]
        public async Task<IActionResult> CreateTrackGenreAsync([FromBody] TrackGenreCreateDto trackGenreCreateDto)
        {
            var response = await _trackGenreService.CreateTrackGenreAsync(trackGenreCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response.Data);
        }


        // -------------------------------------------------------------------------------- //
        // ------------------------ GetGenresByTrackIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------------- //

        [HttpGet("track/{trackId}")]
        public async Task<IActionResult> GetGenresByTrackIdAsync(Guid trackId)
        {
            var response = await _trackGenreService.GetGenresByTrackIdAsync(trackId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response.Data);
        }


        // -------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByGenreIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------------- //

        [HttpGet("genre/{genreId}")]
        public async Task<IActionResult> GetTracksByGenreIdAsync(Guid genreId)
        {
            var response = await _trackGenreService.GetTracksByGenreIdAsync(genreId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response.Data);
        }

    }
}
