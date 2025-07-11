﻿using GrooveNest.Domain.DTOs.ArtistApplicationDTOs;
using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistApplicationController(ArtistApplicationService artistApplicationService) : ControllerBase
    {
        private readonly ArtistApplicationService _artistApplicationService = artistApplicationService;



        // ------------------------------------------------------------------------------------------- //
        // ------------------------ GetPaginatedArtistApplicationAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------------------------- // 

        [HttpGet]
        public async Task<IActionResult> GetPaginatedArtistApplicationAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string search = "")
        {
            var response = await _artistApplicationService.GetAllPaginatedArtistApplicationsAsync(page, pageSize, search);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------------------- //
        // ------------------------ CreateArtistApplicationAsync METHODS ------------------------ //
        // -------------------------------------------------------------------------------------- //

        [HttpPost]
        public async Task<IActionResult> CreateArtistApplicationAsync([FromBody] ArtistApplicationCreateDto artistApplicationCreateDto)
        {
            var response = await _artistApplicationService.CreateArtistApplicationAsync(artistApplicationCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------------------- //
        // ------------------------ UpdateArtistApplicationAsync METHODS ------------------------ //
        // -------------------------------------------------------------------------------------- //

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtistApplicationAsync(Guid id, [FromBody] ArtistApplicationApprovalDto artistApplicationApprovalDto)
        {
            var response = await _artistApplicationService.UpdateArtistApplicationAsync(id, artistApplicationApprovalDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------------------- //
        // ------------------------ DeleteArtistApplicationAsync METHODS ------------------------ //
        // -------------------------------------------------------------------------------------- //

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtistApplicationAsync(Guid id)
        {
            var response = await _artistApplicationService.DeleteArtistApplicationAsync(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }



        // -------------------------------------------------------------------------------------------- //
        // ------------------------ IsUserSubmitArtistApplicationAsync METHODS ------------------------ //
        // -------------------------------------------------------------------------------------------- //

        [HttpGet("isUserSubmitArtistApplication/{userId}")]
        public async Task<IActionResult> IsUserSubmitArtistApplicationAsync(Guid userId)
        {
            var response = await _artistApplicationService.IsUserSubmitArtistApplicationAsync(userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
