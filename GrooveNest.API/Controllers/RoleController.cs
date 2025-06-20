﻿using GrooveNest.Domain.DTOs.RoleDTOs;
using GrooveNest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace GrooveNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(RoleService roleService) : ControllerBase
    {
        private readonly RoleService _roleService = roleService;


        // ------------------------------------------------------------------------- //
        // ------------------------ GetAllRolesAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------- // 

        [HttpGet("all")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var response = await _roleService.GetAllRolesAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ---------------------------------------------------------------------------------- //
        // ------------------------ GetAllPaginatedRolesAsync METHODS ----------------------- //
        // ---------------------------------------------------------------------------------- // 

        [HttpGet]
        public async Task<IActionResult> GetAllPaginatedRolesAsync(int page = 1, int pageSize = 10, string search = "")
        {
            var response = await _roleService.GetAllPaginatedRolesAsync(page, pageSize, search);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // -------------------------------------------------------------------------- //
        // ------------------------ GetRolesByIdAsync METHODS ----------------------- //
        // -------------------------------------------------------------------------- // 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolesByIdAsync(int id)
        {
            var response = await _roleService.GetRoleByIdAsync(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------ //
        // ------------------------ CreateRoleAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ // 

        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync([FromBody] RoleCreateDto roleCreateDto)
        {
            var response = await _roleService.CreateRoleAsync(roleCreateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------ //
        // ------------------------ UpdateRoleAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ //

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoleAsync(int id, [FromBody] RoleUpdateDto roleUpdateDto)
        {
            var response = await _roleService.UpdateRoleAsync(id, roleUpdateDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


        // ------------------------------------------------------------------------ //
        // ------------------------ DeleteRoleAsync METHODS ----------------------- //
        // ------------------------------------------------------------------------ //

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoleAsync(int id)
        {
            var response = await _roleService.DeleteRoleAsync(id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
