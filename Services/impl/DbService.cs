using System.Data;
using Dapper;
using Npgsql;

namespace WagerMate.Services.impl;

public class DbService : IDbService
{
    private IDbConnection _db;

    public DbService(IDbConnection dbConnection)
    {
        // _db = new NpgsqlConnection(configuration.GetConnectionString("Wagerdb"));
        _db = dbConnection;
        _db.Open();
    }

    public bool Create<T>(string sql, object p)
    {
        try
        {
            var result = _db.Query<T>(sql, p).FirstOrDefault();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine("DbService Create failed");
            Console.WriteLine(e);
            throw;
        }
    }


    public T GetById<T>(string sql, object id)
    {
        try
        {
            T result = _db.QuerySingleOrDefault<T>(sql, id);
            if (result == null)
                throw new Exception();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine("DbService GetById failed");
            Console.WriteLine(e);
            throw;
        }
    }

    public List<T> GetAll<T>(string sql)
    {
        try
        {
            var queryResult = _db.Query<T>(sql);
            var result = queryResult.AsList();
            return result;
        }
        catch (Exception e)
        {
            Console.Write("DbService: GetAllFailed");
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Delete<T>(string sql, object id)
    {
        try
        {
            var queryResult = _db.Execute(sql, id);
            if (queryResult > 0) return true;
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine("DbService: Delete Failed");
            Console.WriteLine(e);
            throw;
        }
    }


    public bool Update<T>(string sql, object obj)
    {
        try
        {
            var queryResult = _db.Execute(sql, obj);
            return queryResult > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine("DbService: Update failed");
            Console.WriteLine(e);
            throw;
        }
    }
}