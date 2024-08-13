using Microsoft.Extensions.Configuration;
using Moq;
using Npgsql;

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
    // [Test]
    public void TestCreateUserOld()
    {
        User TestUser = new User();
        TestUser.Email = "Email";
        TestUser.Id = 1;
        TestUser.Name = "Name";
        TestUser.Password = "Password";
        
        // create a Mock for bypassing the creation of an acutal DbService used by UserService 
        var m2 = new Mock<IDbService>();
        // specify what the mock should return when a (DbService-)Function is called
        m2.Setup(service => service.Create<bool>("",new {})).Returns(true);
        // create UserService with th Mock instead of a DbService
        UserService TestUserService = new UserService(m2.Object);
        
        // Verifying if the result of the processing is correct / currently not necessary due to no further processing
        var result = TestUserService.CreateUser(TestUser);
        Assert.That(TestUser, Is.EqualTo(TestUser));
    }

    [Test]
    public void TestCreateUser()
    {
        User localUser = new User();
        localUser.Email = "Email";
        localUser.Name = "Name";
        localUser.Password = "Password";
        
        // builder.Configuration["ConnectionStrings:Wagerdb"]=connectionString;

        IConfiguration iconf = new ConfigurationManager();
        iconf["ConnectionStrings:Wagerdb"] =
            "Host=localhost;Port=5432;Database=mydatabase;UserId=postgres;Password=password";
        IDbService idb = new DbService(iconf);
        UserService TestUserService = new UserService(idb);
        var result = TestUserService.CreateUser(localUser);
        Console.WriteLine("Result of Creating: ");
        
        var r2= TestUserService.GetAllUsers();
        foreach (var sUser in r2)
        {
            Console.WriteLine(sUser);
        }
        Assert.IsNotNull(result);
    }
}