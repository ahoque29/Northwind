using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NorthwindCommon;

public static class ExtensionMethods
{
	public static void Delete<TEntity>(this DbSet<TEntity> entities, Expression<Func<TEntity, bool>> predicate)
		where TEntity : class
	{
		var records = entities.Where(predicate).ToList();

		if (records.Any()) entities.RemoveRange(records);
	}
}