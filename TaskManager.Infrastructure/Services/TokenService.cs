using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Application.Interfacies;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Authentication;

namespace TaskManager.Infrastructure.Services
{
	public class TokenService : ITokenService
	{

		private readonly JwtSettings _jwtSettings;
		public TokenService(IOptions<JwtSettings> options)
		{
			_jwtSettings = options.Value;
		}
		public RefreshToken GenerateRefreshToken(string userId)
		{
			var bytes = RandomNumberGenerator.GetBytes(64);
			var token = Convert.ToBase64String(bytes);
			var refreshToken = new RefreshToken
			{
				Token = token,
				Expiration = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
				UserId = userId,
				IsRevoked = false
			};

			return refreshToken;
		}

		public AccessTokenResult GenerateAccessToken(string userId, string email, IEnumerable<string> roles)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, userId),
				new Claim(ClaimTypes.Email, email)
			};
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
			var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
			var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes);
			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: expiration,
				signingCredentials: credentials
				);

				 var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

			return new AccessTokenResult(tokenString, expiration);
		}
	}
}
