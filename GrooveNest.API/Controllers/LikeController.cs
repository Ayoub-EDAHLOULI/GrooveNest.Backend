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


        // ---------------------------------------------------------------------------------- //
        // ------------------------ GetLikedTracksByUserAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------------- // 

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetLikedTracksByUserAsync(Guid userId)
        {
            var response = await _likeService.GetLikedTracksByUserAsync(userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------------- //
        // ------------------------ GetLikesByTrackIdAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------- // 

        [HttpGet("track/{trackId}")]
        public async Task<IActionResult> GetLikesByTrackIdAsync(Guid trackId)
        {
            var response = await _likeService.GetLikesByTrackIdAsync(trackId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------------- //
        // ------------------------ HasUserLikedTrackAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------- // 

        [HttpGet("hasLiked/{trackId}/{userId}")]
        public async Task<IActionResult> HasUserLikedTrackAsync(Guid trackId, Guid userId)
        {
            var response = await _likeService.HasUserLikedTrackAsync(trackId, userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
