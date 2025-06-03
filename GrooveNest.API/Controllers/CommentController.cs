using GrooveNest.Domain.DTOs.CommentDTOs;
using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController(CommentService commentService) : ControllerBase
    {
        private readonly CommentService _commentService = commentService;


        // --------------------------------------------------------------------------- //
        // ------------------------ CreateCommentAsync METHODS ----------------------- //
        // --------------------------------------------------------------------------- // 

        [HttpPost]
        public async Task<IActionResult> CreateCommentAsync([FromBody] CommentCreateDto commentCreateDto)
        {
            var response = await _commentService.CreateCommentAsync(commentCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // --------------------------------------------------------------------------- //
        // ------------------------ UpdateCommentAsync METHODS ----------------------- //
        // --------------------------------------------------------------------------- // 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommentAsync(int id, [FromBody] CommentUpdateDto commentUpdateDto)
        {
            var response = await _commentService.UpdateCommentAsync(id, commentUpdateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // --------------------------------------------------------------------------- //
        // ------------------------ DeleteCommentAsync METHODS ----------------------- //
        // --------------------------------------------------------------------------- // 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            var response = await _commentService.DeleteCommentAsync(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
