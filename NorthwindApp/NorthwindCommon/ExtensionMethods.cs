using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace NorthwindCommon;

public static class ExtensionMethods
{
	public static void Delete<TEntity>(this DbSet<TEntity> entities, Expression<Func<TEntity, bool>> predicate)
		where TEntity : class
	{
		var records = entities.Where(predicate).ToList();

		if (records.Any()) entities.RemoveRange(records);
	}

	public static async Task<T> Deserialize<T>(this HttpResponseMessage response)
	{
		await using var stream = await response.Content.ReadAsStreamAsync();

		if (!stream.CanRead) return default;

		using var sr = new StreamReader(stream);
		using var jtr = new JsonTextReader(sr);
		var js = JsonSerializer.Create();
		var deserialized = js.Deserialize<T>(jtr);

		return deserialized;
	}
}