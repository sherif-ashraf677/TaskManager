using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TaskManager.Domain.Entities;
namespace TaskManager.Infrastructure.Identity
{
	public class AppUser : IdentityUser
	{

	public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
	}
}
