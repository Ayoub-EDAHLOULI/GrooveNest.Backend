using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistApplicationController(ArtistApplicationService artistApplicationService) : ControllerBase
    {
        private readonly ArtistApplicationService _artistApplicationService = artistApplicationService;


        // ------------------------------------------------------------------------------------- //
        // ------------------------ GetAllArtistApplicationAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------------- // 

        [HttpGet("all")]
        public async Task<IActionResult> GetAllArtistApplicationAsync()
        {
            var response = await _artistApplicationService.GetAllArtistApplicationsAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------------------------- //
        // ------------------------ GetPaginatedArtistApplicationAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------------------- // 

        [HttpGet]
        public async Task<IActionResult> GetPaginatedArtistApplicationAsync([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string search = "")
        {
            var response = await _artistApplicationService.GetAllPaginatedArtistApplicationsAsync(pageNumber, pageSize, search);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------------------- //
        // ------------------------ GetArtistApplicationByIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------------------- //

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistApplicationByIdAsync(int id)
        {
            var response = await _artistApplicationService.GetArtistApplicationByIdAsync(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
