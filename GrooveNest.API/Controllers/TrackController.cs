﻿using GrooveNest.Domain.DTOs.TrackDTOs;
using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackController(TrackService trackService) : ControllerBase
    {
        private readonly TrackService _trackService = trackService;

        // ------------------------------------------------------------------------- //
        // ------------------------ CreateTrackAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateTrackAsync([FromForm] TrackCreateDto trackCreateDto, IFormFile audioFile)
        {
            var response = await _trackService.CreateTrackAsync(trackCreateDto, audioFile);
            if (response == null || !response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        // ----------------------------------------------------------------------------- //
        // ------------------------ GetTrackByTitleAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------- // 

        [HttpGet("title")]
        public async Task<IActionResult> GetTrackByTitleAsync([FromBody] string title)
        {
            var response = await _trackService.GetTrackByTitleAsync(title);
            if (response == null || !response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        // ----------------------------------------------------------------------------- //
        // ------------------------ GetTracksByAlbumTitleAsync METHODS ----------------- //
        // ----------------------------------------------------------------------------- // 

        [HttpGet("album")]
        public async Task<IActionResult> GetTracksByAlbumTitleAsync([FromForm] string albumTitle)
        {
            var response = await _trackService.GetTracksByAlbumTitleAsync(albumTitle);

            if (response == null || !response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ GetTrackByIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrackByIdAsync(Guid id)
        {
            var response = await _trackService.GetTrackByIdAsync(id);
            if (response == null || !response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByAlbumIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------------- // 

        [HttpGet("album/{albumId}")]
        public async Task<IActionResult> GetTracksByAlbumIdAsync(Guid albumId)
        {
            var response = await _trackService.GetTracksByAlbumIdAsync(albumId);
            if (response == null || !response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // --------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByArtistIdAsync METHODS ----------------------- //
        // --------------------------------------------------------------------------------- // 

        [HttpGet("artist/{artistId}")]
        public async Task<IActionResult> GetTracksByArtistIdAsync(Guid artistId)
        {
            var response = await _trackService.GetTracksByArtistIdAsync(artistId);
            if (response == null || !response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ----------------------------------------------------------------------------------- //
        // ------------------------ GetTracksByArtistNameAsync METHODS ----------------------- //
        // ----------------------------------------------------------------------------------- // 

        [HttpGet("artist/name")]
        public async Task<IActionResult> GetTracksByArtistNameAsync([FromForm] string artistName)
        {
            var response = await _trackService.GetTracksByArtistNameAsync(artistName);
            if (response == null || !response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------- //
        // ------------------------ UpdateTrackAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrackAsync(Guid id, [FromBody] TrackUpdateDto trackUpdateDto)
        {
            var response = await _trackService.UpdateTrackAsync(id, trackUpdateDto);
            if (response == null || !response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------- //
        // ------------------------ DeleteTrackAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrackAsync(Guid id)
        {
            var response = await _trackService.DeleteTrackAsync(id);
            if (response == null || !response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
