using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindCommon;
using NorthwindDataAccess.DatabaseContext;

namespace NorthwindDataAccessTests;

public abstract class InMemoryTestBase : IAsyncDisposable
{
	protected readonly NorthwindContext Context;
	protected readonly IMapper Mapper;

	protected InMemoryTestBase()
	{
		var options = new DbContextOptionsBuilder<NorthwindContext>()
			.UseInMemoryDatabase("Database_InMemory")
			.Options;

		Context = new NorthwindContext(options);

		Mapper = new Mapper(new MapperConfiguration(m => m.AddProfile(new MapperProfiles())));
	}

	public async ValueTask DisposeAsync()
	{
		await Context.Database.EnsureDeletedAsync();
		GC.SuppressFinalize(this);
	}
}