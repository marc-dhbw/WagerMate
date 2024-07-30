using System.Data;
using Dapper;
using Npgsql;

namespace WagerMate.Services.impl;

public class DbService : IDbService
{
    private IDbConnection db;
    public DbService(IConfiguration configuration)
    {
        db = new NpgsqlConnection(configuration.GetConnectionString("Wagerdb"));
        db.Open();
    }
    public T Create<T>(string sql,string p)
    {
        var result =db.Query<T>(sql, p).FirstOrDefault();
        return result;
    }


    public T Read<T>(string sql)
    {
        throw new NotImplementedException();
    }

    public T Update<T>(string sql)
    {
        throw new NotImplementedException();
    }

    public T Delete<T>(string sql)
    {
        throw new NotImplementedException();
    }
}