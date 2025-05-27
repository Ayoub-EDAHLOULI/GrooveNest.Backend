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

    }
}
