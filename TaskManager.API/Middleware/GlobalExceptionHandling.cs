using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.API.Middleware
{
	public sealed class GlobalExceptionHandling : IExceptionHandler
	{
	private readonly ILogger<GlobalExceptionHandling> _logger;

		public GlobalExceptionHandling(ILogger<GlobalExceptionHandling> logger)
		{
			_logger = logger;
		}
		public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
		{
			_logger.LogError(exception, "Unhandled exception while processing {Path}", httpContext.Request.Path);
			
			var problemDetails = new ProblemDetails
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "An unexpected error occurred.",
			};
			httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

			await httpContext.Response.WriteAsJsonAsync(
				   problemDetails,
				   options: null,
				   contentType: "application/problem+json",
				   cancellationToken: cancellationToken);

			return true;
		}
	}
}
