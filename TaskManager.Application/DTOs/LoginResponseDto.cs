using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.DTOs
{
	public class LoginResponseDto
	{
		public string Token { get; set; } = null!;
		public string RefreshToken { get; set; } = null!;
		public DateTime AccessTokenExpiration { get; set; }
	}
}
