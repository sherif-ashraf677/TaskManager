
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfacies
{
	public interface IProjectRepository : IRepository<Project>
	{
		Task<IEnumerable<Project>> GetAllByOwnerAsync(string ownerId);
		Task<Project?> GetByIdAsync(Guid id, string ownerId);

	}
}
