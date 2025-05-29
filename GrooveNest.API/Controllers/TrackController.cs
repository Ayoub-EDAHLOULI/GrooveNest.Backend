using GrooveNest.Domain.DTOs.TrackDTOs;
using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController(TrackService trackService) : ControllerBase
    {
        private readonly TrackService _trackService = trackService;

        // ------------------------------------------------------------------------- //
        // ------------------------ CreateTrackAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateTrackAsync([FromForm] TrackCreateDto trackCreateDto, IFormFile audioFile)
        {
            var response = await _trackService.CreateTrackAsync(trackCreateDto, audioFile);
            if (response == null || !response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // ----------------------------------------------------------------------------- //
        // ------------------------ GetTrackByTitleAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------- // 

        [HttpGet("title")]
        public async Task<IActionResult> GetTrackByTitleAsync([FromBody] string title)
        {
            var response = await _trackService.GetTrackByTitleAsync(title);
            if (response == null || !response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        // ----------------------------------------------------------------------------- //
        // ------------------------ GetTracksByAlbumTitleAsync METHODS ----------------- //
        // ----------------------------------------------------------------------------- // 

        [HttpGet("album")]
        public async Task<IActionResult> GetTracksByAlbumTitleAsync([FromForm] string albumTitle)
        {
            var response = await _trackService.GetTracksByAlbumTitleAsync(albumTitle);

            if (response == null || !response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ GetTrackByIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrackByIdAsync(Guid id)
        {
            var response = await _trackService.GetTrackByIdAsync(id);
            if (response == null || !response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
