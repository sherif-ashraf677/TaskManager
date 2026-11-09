using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfacies
{
	public interface ITokenService
	{
		AccessTokenResult GenerateAccessToken(string userId, string email, IEnumerable<string> roles);

		RefreshToken GenerateRefreshToken(string userId);
	}

	public record AccessTokenResult(string Token, DateTime Expiration);
}
