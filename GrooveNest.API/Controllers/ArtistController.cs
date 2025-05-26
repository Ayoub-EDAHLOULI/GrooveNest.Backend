using GrooveNest.Domain.DTOs.ArtistDTOs;
using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController(ArtistService artistService) : ControllerBase
    {
        private readonly ArtistService _artistService = artistService;


        // -------------------------------------------------------------------------- //
        // ------------------------ GetAllArtistAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 

        [HttpGet("all")]
        public async Task<IActionResult> GetAllArtistAsync()
        {
            var response = await _artistService.GetAllArtistsAsync();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------------------ //
        // ------------------------ GetAllPaginatedArtistsAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------------ // 

        [HttpGet]
        public async Task<IActionResult> GetAllPaginatedArtistsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var response = await _artistService.GetAllPaginatedArtistsAsync(page, pageSize, search);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // --------------------------------------------------------------------------- //
        // ------------------------ GetArtistByIdAsync METHODS ----------------------- //
        // --------------------------------------------------------------------------- // 

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetArtistByIdAsync(Guid id)
        {
            var response = await _artistService.GetArtistByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ CreateArtistAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 

        [HttpPost]
        public async Task<IActionResult> CreateArtistAsync([FromBody] ArtistCreateDto artistCreateDto)
        {
            var response = await _artistService.CreateArtistAsync(artistCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ UpdateArtistAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateArtistAsync(Guid id, [FromBody] ArtistUpdateDto artistUpdateDto)
        {
            var response = await _artistService.UpdateArtistAsync(id, artistUpdateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ DeleteArtistAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- //

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteArtistAsync(Guid id)
        {
            var response = await _artistService.DeleteArtistAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
