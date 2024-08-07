﻿﻿using System.Data;
using Dapper;
using Npgsql;

namespace WagerMate.Services.impl;

public class DbService : IDbService
{
    private IConfiguration _config;

    private const string Conname = "Wagerdb";
    private string? ConnectionString;

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
            if(result != null)
                return true;
            else
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
            T result = connection.QuerySingleOrDefault<T>(sql, id);
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
}