
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfacies
{
	public interface ITaskRepository : IRepository<TaskItem>
	{
		Task<IEnumerable<TaskItem>> GetAllByProjectAsync(Guid projectId);
		Task<TaskItem?> GetByIdAsync(Guid taskId, Guid projectId);
	}
}
