using Npgsql;
using NUnit.Framework.Internal;

namespace UnitTests;

[TestFixture]
public class IntegrationTests
{
    [Test]
    public void TestDbConnection()
    {
        // Npqsql has to be changed to the actual connection
        var TestConnection = new NpgsqlConnection("InsertString");
        try
        {
            TestConnection.Open();
            TestConnection.Close();
            Assert.Pass();
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
    }
}