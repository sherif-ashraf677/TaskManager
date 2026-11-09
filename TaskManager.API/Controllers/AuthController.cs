using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfacies;

namespace TaskManager.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login( LoginRequestDto request)
		{
			var response = await _authService.LoginAsync(request);
			if (response is null) return Unauthorized("Invalid email or password");

			return Ok(response);
		}
		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto request)
		{
			var response = await _authService.RegisterAsync(request);
			if (response is null) return BadRequest("Registration failed");

			return Ok(response);
		}
		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken(RefreshTokenDto request)
		{
			var response = await _authService.RefreshTokenAsync(request);
			if (response is null) return Unauthorized("Invalid or expired refresh token");

			return Ok(response);
		}
		[HttpPost("logout")]
		public async Task<IActionResult> Logout(RefreshTokenDto request)
		{
			await _authService.LogoutAsync(request);
			return NoContent();
		}
	}
}
