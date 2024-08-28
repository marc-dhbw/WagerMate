using Microsoft.Extensions.Configuration;
using Npgsql;
using WagerMate.Service_Implementation.database;
using WagerMate.Service_Implementation.user;
using WagerMate.Services.database;

namespace UnitTests;

[TestFixture]
public class UserServiceTest
{
    private UserService _userService;
    private NpgsqlConnection _connection;
    private IDbService _idb;

    [SetUp]
    public void Setup()
    {
        _connection =
            new NpgsqlConnection("Host=localhost;Port=5432;Database=mydatabase;UserId=postgres;Password=password");
        _connection.Open();
        IConfiguration iconf = new ConfigurationManager();
        iconf["ConnectionStrings:Wagerdb"] =
            "Host=localhost;Port=5432;Database=mydatabase;UserId=postgres;Password=password";
        _idb = new DbService(iconf);
        _userService = new UserService(_idb);
    }

    public void ClearDatabase()
    {
        _idb.CustomSql("DELETE FROM public.user");
        _idb.CustomSql("ALTER SEQUENCE user_id_seq RESTART WITH 1");
    }
    
    [Test]
    public void TestCreateUserEmptyConstructor()
    {
        ClearDatabase();
        
        //Creates User with Empty Constructor
        var localUser = new User();
        localUser.Email = "Email";
        localUser.Name = "Name";
        localUser.Password = "Password";

        var result = _userService.CreateUser(localUser);
        Assert.IsNotNull(result);
    }

    [Test]
    public void TestCreateUserKonstructor()
    {
        ClearDatabase();
        
        //Creates User with Constructor
        User localUser = new User(1, "test@gmail.com", "TestName", "TestPassword");
        
        var result = _userService.CreateUser(localUser);
        Assert.IsNotNull(result);
    }

    [Test]
    public void TestGetAllUser()
    {
        ClearDatabase();
        
        //Create 3 users
        var localUser1 = new User(2, "Name", "Email", "Password");
        var localUser2 = new User(3, "Name", "Email", "Password");
        var localUser3 = new User(4, "Name", "Email", "Password");
        _userService.CreateUser(localUser1);
        _userService.CreateUser(localUser2);
        _userService.CreateUser(localUser3);
        
        //Get all users
        var result = _userService.GetAllUsers();
        Assert.IsNotNull(result);
    }

    [Test]
    public void TestUpdateUser()
    {
        ClearDatabase();
        
        //Create User to be updated
        var localUser = new User(1, "Name", "Email", "Password");
        var id = _userService.CreateUser(localUser);
        
        //Update User
        var updatedUser = new User(1, "updatedName", "updatedEmail", "updatedPassword");
        var result = _userService.UpdateUser(updatedUser);
        Assert.IsTrue(result);
    }

    [Test]
    public void TestGetUserById()
    {
        ClearDatabase();
        
        //Create User
        var localUser = new User(1, "Name", "Email", "Password");
        _userService.CreateUser(localUser);
        
        //Get User by ID
        var result = _userService.GetUserById(1);
        Assert.IsNotNull(result);
    }

    [Test]
    public void TestDeletUser()
    {
        ClearDatabase();
        
        //Create User
        var localUser = new User(1, "Name", "Email", "Password");
        _userService.CreateUser(localUser);
        
        //Delete User
        var result = _userService.DeleteUser(1);
        Assert.IsTrue(result);
    }
    
    [Test]
    public void TestGetUserByEmail()
    {
        ClearDatabase();
        
        //Create User
        var localUser = new User(1, "Name", "mail@mail.de", "Password");
        _userService.CreateUser(localUser);
        
        //Get User by Email
        var result = _userService.GetUserByEmail("mail@mail.de");
        Assert.IsNotNull(result);
    }

    [Test]
    public void TestGetUserByPassword()
    {
        ClearDatabase();
        
        //Create User
        var localUser = new User(1, "Name", "Email", "Password");
        _userService.CreateUser(localUser);
        
        //Get User by Password
        var result = _userService.GetUserByPassword("Password");
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestDoesUserPasswordExist()
    {
        ClearDatabase();
        
        //Create User
        var localUser = new User(1, "Name", "Email", "Password");
        _userService.CreateUser(localUser);
        
        //Check if Password exists
        var result = _userService.DoesUserPasswordExist("Password");
        Assert.IsTrue(result);
    }

    [Test]
    public void TestEmailIsRegistered()
    {
        ClearDatabase();

        //Create User
        var localUser = new User(1, "Name", "mail@mail.de", "Password");
        _userService.CreateUser(localUser);
        
        //Tests if Email exists
        var result = _userService.EmailIsRegistered("mail@mail.de");
        Assert.IsTrue(result);
    }
    
    [Test]
    public void TestGetUserIfPasswordExists()
    {
        ClearDatabase();
        
        //Create User
        var localUser = new User(1, "Name", "Email", "Password");
        _userService.CreateUser(localUser);
        
        //Get User if Password exists
        var result = _userService.GetUserIfPasswordExists("Password");
        Assert.IsNotNull(result);
    }
}