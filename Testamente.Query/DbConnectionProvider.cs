using System.Data;
using Microsoft.Data;
using Microsoft.Data.SqlClient;

namespace Testamente.Query;
public class DbConnectionProvider : IDbConnectionProvider
{
    private readonly string _connStr;
    public DbConnectionProvider(string connStr)
    {
        _connStr = connStr;
    }

    public IDbConnection Get()
    {
        return new SqlConnection(_connStr);
    }
}

public interface IDbConnectionProvider
{
    IDbConnection Get();
}