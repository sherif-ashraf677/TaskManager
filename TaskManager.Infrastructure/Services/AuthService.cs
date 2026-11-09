using Microsoft.AspNetCore.Identity;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfacies;
using TaskManager.Domain.Enums;
using TaskManager.Infrastructure.Identity;
using TaskManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace TaskManager.Infrastructure.Services
{
	public class AuthService : IAuthService
	{
	private readonly UserManager<AppUser> _userManager;
		private readonly ITokenService _tokenService;
		private readonly ApplicationDbContext _context;

		public AuthService(UserManager<AppUser> userManager, ITokenService tokenService, ApplicationDbContext context)
		{
			_userManager = userManager;
			_tokenService = tokenService;
			_context = context;
		}



		public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
		{

			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user is null) return null;
			if (!await _userManager.CheckPasswordAsync(user, request.Password)) return null;

			var response = await CreateTokensAsync(user);
			return response;
		}

		public async Task LogoutAsync(RefreshTokenDto refreshToken)
		{
			var token = await _context.RefreshTokens
				.FirstOrDefaultAsync(rt => rt.Token == refreshToken.RefreshToken);

			if (token is null) return;

			token.IsRevoked = true;
			await _context.SaveChangesAsync();
		}

		public async Task<LoginResponseDto?> RefreshTokenAsync(RefreshTokenDto refreshToken)
		{
			var token = await _context.RefreshTokens
				.FirstOrDefaultAsync(rt => rt.Token == refreshToken.RefreshToken);

			if (token is null || token.IsRevoked==true || token.Expiration <= DateTime.UtcNow) return null;
			var user = await _userManager.FindByIdAsync(token.UserId);
			if (user is null) return null;
			token.IsRevoked = true;



			var response = await CreateTokensAsync(user);
			return response;
		}

		public async Task<LoginResponseDto?> RegisterAsync(RegisterDto request)
		{
			var user = new AppUser
			{
				Email = request.Email,
				UserName = request.Email
			};
			var result = await _userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded) return null;

			var roleResult = await _userManager.AddToRoleAsync(user, nameof(UserRole.User));
			if (!roleResult.Succeeded)
			{
				await _userManager.DeleteAsync(user);   
				return null;
			}


			var response = await CreateTokensAsync(user);
			return response;

		}



		private async Task<LoginResponseDto> CreateTokensAsync(AppUser user){
			var roles = await _userManager.GetRolesAsync(user);
			var token = _tokenService.GenerateAccessToken(user.Id, user.Email!, roles);
			var refreshToken = _tokenService.GenerateRefreshToken(user.Id);
			_context.RefreshTokens.Add(refreshToken);
			await _context.SaveChangesAsync();
			return new LoginResponseDto
			{
				Token = token.Token,
				RefreshToken = refreshToken.Token,
				AccessTokenExpiration = token.Expiration
			};
		}
	}
}
