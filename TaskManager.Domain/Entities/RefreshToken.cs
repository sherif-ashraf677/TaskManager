using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Domain.Entities
{
	public class RefreshToken
	{
		public Guid Id { get; set; }
		public string Token { get; set; } = null!;
		public DateTime Expiration { get; set; }
		public string UserId { get; set; } = null!;
		public bool IsRevoked { get; set; }
		
	}
}
