using System.Data;
using Dapper;
using Npgsql;

namespace WagerMate.Services.impl;

public class DbService : IDbService
{
    private IDbConnection _db;
    public DbService(IConfiguration configuration)
    {
        _db = new NpgsqlConnection(configuration.GetConnectionString("Wagerdb"));
        _db.Open();
    }
    public bool Create<T>(string sql,object p)
    {
        var result =_db.Query<T>(sql, p).FirstOrDefault();
        return true;
    }


    public T GetByKey<T>(string sql, object key)
    {
        T result = _db.QuerySingleOrDefault<T>(sql,key);
        if(result == null)
            throw new Exception();
        return result;
    }

    public List<T> GetAll<T>(string sql)
    {
        var queryResult = _db.Query<T>(sql);
        var result = queryResult.AsList();
        return result;
    }

    public bool Delete<T>(string sql, int key)
    {
        var queryResult = _db.Execute(sql, key);
        if (queryResult > 0) return true;
        return false;
    }
    
    
    public bool Update<T>(string sql, object obj)
    {
        var queryResult = _db.Execute(sql, obj);
        if (queryResult > 0) return true;
        return false;
    }
    

}