using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfacies;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Persistence;

namespace TaskManager.Infrastructure.Repositories
{
	public class ProjectRepository: Repository<Project>, IProjectRepository
	{
		public ProjectRepository(ApplicationDbContext context) : base(context)
		{
		}
		public async Task<IEnumerable<Project>> GetAllByOwnerAsync(string ownerId)
		{
			return await _context.Projects.Include(p => p.Tasks).Where(p => p.OwnerId == ownerId).ToListAsync();
		}

		public async Task<Project?> GetByIdAsync(Guid id, string ownerId)
		{
			return await _context.Projects.Include(p=>p.Tasks).FirstOrDefaultAsync(p=>p.Id == id && p.OwnerId == ownerId);
		}
	}
}
