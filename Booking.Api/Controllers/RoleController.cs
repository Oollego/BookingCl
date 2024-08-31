using Booking.Application.Services;
using Booking.Domain.Dto.Role;
using Booking.Domain.Dto.Room;
using Booking.Domain.Dto.UserRole;
using Booking.Domain.Entity;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Booking.Api.Controllers
{
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService = null!;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Create role
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// </remarks>
        /// <response code="200">If role created</response>
        /// <response code="400">If role wasn't created</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDto>>> Create([FromBody] CreateRoleDto dto)
        {
            var response = await _roleService.CreateRoleAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        /// <summary>
        /// Delete role
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// </remarks>
        /// <response code="200">If role deleted</response>
        /// <response code="400">If role wasn't deleted</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDto>>> Delete(long id)
        {
            var response = await _roleService.DeleteRoleAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// </remarks>
        /// <response code="200">If role updated</response>
        /// <response code="400">If role wasn't updated</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDto>>> Update([FromBody] RoleDto dto)
        {
            var response = await _roleService.UpdateRoleAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Adding role to user
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// </remarks>
        /// <response code="200">If role added</response>
        /// <response code="400">If role wasn't added</response>
        [HttpPost("addRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDto>>> AddRoleForUser([FromBody] UserRoleDto dto)
        {
            var response = await _roleService.AddRoleForUserAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Delete role from user
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// </remarks>
        /// <response code="200">If role deleted</response>
        /// <response code="400">If role not deleted</response>
        [HttpDelete("delete-role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDto>>> DeleteRoleForUser([FromBody] DeleteUserRoleDto dto)
        {
            var response = await _roleService.DeleteRoleForUserAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPut("update-role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoleDto>>> UpdateRoleForUser([FromBody] UpdateUserRoleDto dto)
        {
            var response = await _roleService.UpdateRoleForUserAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
