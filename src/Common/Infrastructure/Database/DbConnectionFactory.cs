using Application.Abstractions.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure.Database;

internal sealed class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public IDbConnection GetOpenConnection()
    {
        var connection = new SqlConnection(_connectionString);

        return connection;
    }
}
