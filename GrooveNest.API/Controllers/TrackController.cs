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
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ----------------------------------------------------------------------------- //
        // ------------------------ GetTrackByTitleAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------- // 

        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetTrackByTitleAsync(string title)
        {
            var response = await _trackService.GetTrackByTitleAsync(title);
            if (response == null || !response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


    }
}
