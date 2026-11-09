
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Identity;

namespace TaskManager.Infrastructure.Persistence
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Project> Projects { get; set; }
		public DbSet<TaskItem> Tasks { get; set; }

		public DbSet<RefreshToken> RefreshTokens { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Project>()
				.HasMany(p => p.Tasks)
				.WithOne(t => t.Project)
				.HasForeignKey(t => t.ProjectId)
				.OnDelete(DeleteBehavior.Cascade);
				
			modelBuilder.Entity<AppUser>()
			.HasMany(u => u.RefreshTokens)
			.WithOne()
			.HasForeignKey(rt => rt.UserId)
			.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<RefreshToken>(entity =>
			{
				entity.Property(t => t.Token).HasMaxLength(200);
				entity.HasIndex(t => t.Token).IsUnique();
			});
		}

	}
}
