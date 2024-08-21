using Microsoft.Extensions.Configuration;
using Npgsql;
using NUnit.Framework.Internal;
using IConfiguration = Castle.Core.Configuration.IConfiguration;

namespace UnitTests;

[TestFixture]
public class IntegrationTests
{
    [Test]
    public void TestDbConnection()
    {
        var TestConnection =
            new NpgsqlConnection("Host=localhost;Port=5432;Database=mydatabase;UserId=postgres;Password=password");
        TestConnection.Open();
        TestConnection.Close();
        Assert.Pass();
    }
}