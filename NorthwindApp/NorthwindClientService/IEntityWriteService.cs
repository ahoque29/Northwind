using NorthwindCommon.DTOs;

namespace NorthwindClientService;

public interface IEntityCreateService
{
	Task Create<T>(IEnumerable<T> entities) where T : BaseDto, new();

	Task Create<T>(T entity) where T : BaseDto, new() => Create(new List<T> { entity });
}

public interface IEntityEditService
{
	Task Edit<T>(IEnumerable<T> entities) where T : BaseDto, new();

	Task Edit<T>(T entity) where T : BaseDto, new() => Edit(new List<T> { entity });
}

public interface IEntityUpsertService
{
	Task UpsertService<T>(IEnumerable<T> entities) where T : BaseDto, new();

	Task UpsertService<T>(T entity) where T : BaseDto, new() => UpsertService(new List<T> { entity });
}

public interface IEntityRemoveService
{
	Task Remove<T>(IEnumerable<T> entities) where T : BaseDto, new();

	Task Remove<T>(T entity) where T : BaseDto, new() => Remove(new List<T> { entity });
}

public interface IEntityRemoveAllService
{
	Task RemoveAll<T>() where T : BaseDto, new();
}