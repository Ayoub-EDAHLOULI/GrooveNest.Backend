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
    }
}
