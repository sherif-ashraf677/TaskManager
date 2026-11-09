using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.API.Controllers
{

	public abstract class ApiControllerBase : ControllerBase
	{
		protected string CurrentUserId =>
	   User.FindFirst(ClaimTypes.NameIdentifier)?.Value
	   ?? throw new InvalidOperationException("User id claim missing from token.");
	}
}
