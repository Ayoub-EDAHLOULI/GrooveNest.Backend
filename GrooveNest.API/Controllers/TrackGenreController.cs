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

    }
}
