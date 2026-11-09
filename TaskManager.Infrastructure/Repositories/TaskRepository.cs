using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfacies;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories
{
	public class TaskRepository : Repository<TaskItem>, ITaskRepository
	{
		public TaskRepository(ApplicationDbContext context) : base(context)
		{
		}
		public async Task<IEnumerable<TaskItem>> GetAllByProjectAsync(Guid projectId)
		{
			return await _context.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();
		}

		public async Task<TaskItem?> GetByIdAsync(Guid taskId, Guid projectId)
		{
			return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.ProjectId == projectId);
		}
		
	}
}
