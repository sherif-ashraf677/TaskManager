using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.DTOs
{
	public class LoginRequestDto
	{
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
	}
}
