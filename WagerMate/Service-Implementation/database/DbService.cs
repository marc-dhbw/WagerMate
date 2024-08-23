using Dapper;
using Npgsql;
using WagerMate.Services;
using WagerMate.Services.database;

namespace WagerMate.Service_Implementation.database;

public class DbService : IDbService
{
    private const string Conname = "Wagerdb";
    private readonly IConfiguration _config;
    private readonly string? ConnectionString;

    public DbService(IConfiguration configuration)
    {
        _config = configuration;
        ConnectionString = _config.GetConnectionString(Conname);
    }

    public bool Create<T>(string sql, object p)
    {
        using var connection = new NpgsqlConnection(_config.GetConnectionString("Wagerdb"));
        connection.Open();
        try
        {
            var result = connection.Query<T>(sql, p).FirstOrDefault();
            if (result != null)
                return true;
            return false;
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
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var result = connection.QuerySingleOrDefault<T>(sql, id);
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
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var queryResult = connection.Query<T>(sql);
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
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var queryResult = connection.Execute(sql, id);
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
        using var connection = new NpgsqlConnection(ConnectionString);

        connection.Open();

        try
        {
            var queryResult = connection.Execute(sql, obj);
            return queryResult > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine("DbService: Update failed");
            Console.WriteLine(e);
            throw;
        }
    }
    public List<T> GetAllWithParams<T>(string sql, object parameters)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var queryResult = connection.Query<T>(sql, parameters);
            var result = queryResult.AsList();
            return result;
        }
        catch (Exception e)
        {
            Console.Write("DbService: GetAllWithParams failed");
            Console.WriteLine(e);
            throw;
        }
    }

    public (bool, T?) GetIfExists<T>(string existsSql, object existsParameters, string getSql, object getParameters) where T : class
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();
        try
        {
            var existsResult = connection.Query<bool>(existsSql, existsParameters).FirstOrDefault();
            if (!existsResult)
            {
                return (false, null);
            }

            var getResult = connection.Query<T>(getSql, getParameters).FirstOrDefault();
            return (true, getResult);
        }
        catch (Exception e)
        {
            Console.Write("DbService: GetIfExists failed");
            Console.WriteLine(e);
            throw;
        }
    }
}