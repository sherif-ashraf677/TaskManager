
namespace TaskManager.Application.DTOs
{
	public record ProjectDto(Guid Id, string Name, string? Description, int TaskCount,int DoneTaskCount);
	public record CreateProjectDto(string Name, string? Description);
	public record UpdateProjectDto(string Name, string? Description);
}
