using TaskManager.Domain.Common;

namespace TaskManager.Application.Interfacies;

public interface IRepository<T> where T : BaseEntity
{
	Task AddAsync(T entity);
	void Update(T entity);
	void Delete(T entity);
	Task<int> SaveChangesAsync();
}