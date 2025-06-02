using GrooveNest.Domain.DTOs.RatingDTOs;
using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController(RatingService ratingService) : ControllerBase
    {
        private readonly RatingService _ratingService = ratingService;


        // -------------------------------------------------------------------------- //
        // ------------------------ CreateRatingAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 

        [HttpPost]
        public async Task<IActionResult> CreateRatingAsync([FromBody] RatingCreateDto ratingCreateDto)
        {
            var response = await _ratingService.CreateRatingAsync(ratingCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ UpdateRatingAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRatingAsync(int id, [FromBody] RatingUpdateDto ratingUpdateDto)
        {
            var response = await _ratingService.UpdateRatingAsync(id, ratingUpdateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
