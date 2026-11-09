
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Interfacies
{
	public interface IAuthService
	{
		Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);

		Task<LoginResponseDto?> RegisterAsync(RegisterDto request);

		Task<LoginResponseDto?> RefreshTokenAsync(RefreshTokenDto refreshToken);

		Task LogoutAsync(RefreshTokenDto refreshToken);

	}
}
