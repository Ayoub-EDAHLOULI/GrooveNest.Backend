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
    }
}
