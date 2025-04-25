using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthService.DTOs;
using AuthService.Services;

namespace AuthService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var createdByIp = HttpContext.Connection.RemoteIpAddress?.ToString(); // Get the client's IP address
            return Ok(await _auth.RegisterAsync(dto, createdByIp));  // Pass IP to the RegisterAsync method
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var createdByIp = HttpContext.Connection.RemoteIpAddress?.ToString(); // Get the client's IP address
            return Ok(await _auth.LoginAsync(dto, createdByIp));  // Pass IP to the LoginAsync method
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto dto)
        {
            return Ok(await _auth.RefreshAsync(dto.RefreshToken));
        }
    }
}
