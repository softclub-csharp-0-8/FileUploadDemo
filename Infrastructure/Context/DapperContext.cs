using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Context;

public class DapperContext
{
    private readonly IConfiguration _configuration;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_configuration.GetConnectionString("Default"));
    }
}