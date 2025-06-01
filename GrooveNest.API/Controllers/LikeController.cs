using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController(LikeService likeService) : ControllerBase
    {
        private readonly LikeService _likeService = likeService;


        // ------------------------------------------------------------------------ //
        // ------------------------ CreateLikeAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 

        [HttpPost("{trackId}/{userId}")]
        public async Task<IActionResult> CreateLikeAsync(Guid trackId, Guid userId)
        {
            var response = await _likeService.CreateLikeAsync(trackId, userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------ //
        // ------------------------ DeleteLikeAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 

        [HttpDelete("{trackId}/{userId}")]
        public async Task<IActionResult> DeleteLikeAsync(Guid trackId, Guid userId)
        {
            var response = await _likeService.DeleteLikeAsync(trackId, userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
