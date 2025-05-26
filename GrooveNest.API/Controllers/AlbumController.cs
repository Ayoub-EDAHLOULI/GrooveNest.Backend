using GrooveNest.Domain.DTOs.AlbumDTOs;
using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController(AlbumService albumService) : ControllerBase
    {
        private readonly AlbumService _albumService = albumService;


        // ------------------------------------------------------------------------- //
        // ------------------------ GetAllAlbumAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAlbumAsync()
        {
            var response = await _albumService.GetAllAlbumAsync();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ---------------------------------------------------------------------------------- //
        // ------------------------ GetAllPaginatedAlbumAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------------- // 

        [HttpGet]
        public async Task<IActionResult> GetAllPaginatedAlbumAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var response = await _albumService.GetAllPaginatedAlbumAsync(page, pageSize, search);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ GetAlbumByIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAlbumByIdAsync(Guid id)
        {
            var response = await _albumService.GetAlbumByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------- //
        // ------------------------ CreateAlbumAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- //

        [HttpPost]
        public async Task<IActionResult> CreateAlbumAsync([FromForm] AlbumCreateDto albumCreateDto)
        {
            var response = await _albumService.CreateAlbumAsync(albumCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------- //
        // ------------------------ UpdateAlbumAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAlbumAsync(Guid id, [FromForm] AlbumUpdateDto albumUpdateDto)
        {
            var response = await _albumService.UpdateAlbumAsync(id, albumUpdateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------- //
        // ------------------------ DeleteAlbumAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAlbumAsync(Guid id)
        {
            var response = await _albumService.DeleteAlbumAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
