﻿using Booking.Domain.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Booking.Domain.Dto.User;
using Booking.Domain.Dto;
using Booking.Domain.Interfaces.Services;
using Booking.Application.Services;

namespace Booking.Api.Controllers
{
   // [Route("api/[controller]")]
  
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService = null!;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// User Registration
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<BaseResult<UserDto>>> Register([FromBody] RegisterUserDto dto)
        {
            var response = await _authService.Register(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody] LoginUserDto dto)
        {
            var response = await _authService.Login(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
