using Microsoft.Extensions.Configuration;
using Npgsql;
using WagerMate.Service_Implementation.database;
using WagerMate.Service_Implementation.user;
using WagerMate.Services.database;

namespace UnitTests;

[TestFixture]
public class TestingUserService
{
    [SetUp]
    public void Setup()
    {
        var TestConnection =
            new NpgsqlConnection("Host=localhost;Port=5432;Database=mydatabase;UserId=postgres;Password=password");
        TestConnection.Open();
    }
    
    [Test]
    public void TestCreateUser()
    {
        User localUser = new User();
        localUser.Email = "Email";
        localUser.Name = "Name";
        localUser.Password = "Password";
        
        IConfiguration iconf = new ConfigurationManager();
        iconf["ConnectionStrings:Wagerdb"] =
            "Host=localhost;Port=5432;Database=mydatabase;UserId=postgres;Password=password";
        IDbService idb = new DbService(iconf);
        UserService TestUserService = new UserService(idb);
        var result = TestUserService.CreateUser(localUser);
        Assert.IsNotNull(result);
    }
    

}