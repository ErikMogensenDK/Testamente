using System.Data;
using Dapper;

namespace Testamente.Query
{
    public class QueryExecutor : IQueryExecutor
    {
		public IEnumerable<T> Query<T>(IDbConnection cnn,
									   string sql,
									   object? param = null,
									   IDbTransaction? transaction = null,
									   bool buffered = true,
									   int? commandTimeout = null,
									   CommandType? commandType = null)
		{
			return cnn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
		}
	}

	public interface IQueryExecutor
	{
		IEnumerable<T> Query<T>(IDbConnection cnn,
							   string sql,
							   object? param = null,
							   IDbTransaction? transaction = null,
							   bool buffered = true,
							   int? commandTimeout = null,
							   CommandType? commandType = null);
	}
}