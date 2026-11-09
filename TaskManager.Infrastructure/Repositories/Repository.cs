using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Interfacies;
using TaskManager.Domain.Common;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
	{
	protected readonly ApplicationDbContext _context;
		public Repository(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task AddAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
		}

		public void Update(T entity)
		{
			_context.Set<T>().Update(entity);
		}

		public void Delete(T entity)
		{
			_context.Set<T>().Remove(entity);
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
		}
}
