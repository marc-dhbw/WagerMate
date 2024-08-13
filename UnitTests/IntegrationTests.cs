using Microsoft.Extensions.Configuration;
using Npgsql;
using NUnit.Framework.Internal;
using IConfiguration = Castle.Core.Configuration.IConfiguration;
using WagerMate.Data;
using WagerMate.Services;
using WagerMate.Services.impl;

namespace UnitTests;

[TestFixture]
public class IntegrationTests
{
    [Test]
    public void TestDbConnection()
    {
        // CON_STR=Host=localhost;Port=5432;Database=mydatabase;UserId=postgres;Password=password
        // Npqsql has to be changed to the actual connection
        // ConfigurationManager iconf = new ConfigurationManager();
        // var connection = iconf.GetConnectionString("Wagerdb");

        Console.WriteLine("Hier!");
        var TestConnection =
            new NpgsqlConnection("Host=localhost;Port=5432;Database=mydatabase;UserId=postgres;Password=password");
        Console.WriteLine("Pre open connection");
        TestConnection.Open();
        Console.WriteLine("Pre close connection");
        TestConnection.Close();
        Console.WriteLine("Post close connection");
        Assert.Pass();
    }
}