using TaskManager.Domain.Common;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public class TaskItem : BaseEntity
{
	public string Title { get; set; } = string.Empty;
	public string? Description { get; set; }
	public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Todo;
	public Priority Priority { get; set; } = Priority.Medium;
	public DateTime? DueDate { get; set; }

	public Guid ProjectId { get; set; }
	public Project Project { get; set; } = null!;
}