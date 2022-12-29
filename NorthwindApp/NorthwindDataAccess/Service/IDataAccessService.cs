using NorthwindCommon.DTOs;

namespace NorthwindDataAccess.Service;

public interface IReadService<out T>
{
	IQueryable<T> Get();
}

public interface IReadServiceAsync<T> where T : BaseDto
{
	Task<IQueryable<T>> GetAsync();
}

public interface ICreateService<in T> where T : BaseDto
{
	Task Create(IEnumerable<T> entities, string user);
	Task Create(T entity, string user) => Create(new List<T> { entity }, user);
}

public interface IEditService<in T> where T : BaseDto
{
	Task Edit(IEnumerable<T> entities, string user);
	Task Edit(T entity, string user) => Edit(new List<T> { entity }, user);
}

public interface IUpsertService<in T> where T : BaseDto
{
	Task Upsert(IEnumerable<T> entities, string user);
	Task Upsert(T entity, string user) => Upsert(new List<T> { entity }, user);
}

public interface IRemoveService<in T> where T : BaseDto
{
	Task Remove(IEnumerable<T> entities, string user);
	Task Remove(T entities, string user) => Remove(new List<T> { entities }, user);
}

public interface IRemoveAllService<T> where T : BaseDto
{
	Task RemoveAll();
}