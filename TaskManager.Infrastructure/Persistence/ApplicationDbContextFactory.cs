using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskManager.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
	public ApplicationDbContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
		optionsBuilder.UseSqlServer(
			"Data Source=.\\SQLEXPRESS;Initial Catalog=TaskManager;Integrated Security=True;TrustServerCertificate=True");

		return new ApplicationDbContext(optionsBuilder.Options);
	}
}