using TaskManager.Domain.Common;

namespace TaskManager.Domain.Entities;

public class Project : BaseEntity
{
	public string Name { get; set; } = string.Empty;
	public string? Description { get; set; }
	public string OwnerId { get; set; } = string.Empty; // Identity user id, added later

	public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}