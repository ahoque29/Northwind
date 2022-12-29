using Microsoft.EntityFrameworkCore;
using NorthwindCommon.DbModels;

namespace NorthwindDataAccess.DatabaseContext;

public class NorthwindContext : DbContext
{
	public NorthwindContext(DbContextOptions<NorthwindContext> opts) : base(opts)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Customer>(e => e.ToTable("Customer", "dbo"));

		modelBuilder.Entity<JobTitle>(e => e.ToTable("JobTitle", "dbo"));

		modelBuilder.Entity<Country>(e => e.ToTable("Country", "dbo"));
	}
}