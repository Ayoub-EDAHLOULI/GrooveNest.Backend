using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController(GenreService genreService) : ControllerBase
    {
        private readonly GenreService _genreService = genreService;


        // -------------------------------------------------------------------------- //
        // ------------------------ GetAllGenresAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 

        [HttpGet("all")]
        public async Task<IActionResult> GetAllGenresAsync()
        {
            var response = await _genreService.GetAllGenresAsync();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ----------------------------------------------------------------------------------- //
        // ------------------------ GetAllPaginatedGenresAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------------- // 

        [HttpGet]
        public async Task<IActionResult> GetAllPaginatedGenresAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var response = await _genreService.GetAllPaginatedGenresAsync(page, pageSize, search);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ GetGenreByIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGenreByIdAsync(int id)
        {
            var response = await _genreService.GetGenreByIdAsync(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
