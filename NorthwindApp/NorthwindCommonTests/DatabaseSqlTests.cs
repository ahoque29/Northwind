using Microsoft.EntityFrameworkCore;
using NorthwindCommon.DbModels;
using NorthwindDataAccess.DatabaseContext;

namespace NorthwindCommonTests;

[Collection("Database")]
public class DatabaseSqlTests
{
	[Fact]
	public void CheckSql()
	{
	}
}