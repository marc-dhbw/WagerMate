using WagerMate.Data.user;

namespace UnitTests;

[TestFixture]
public class UserServiceTest
{
    private UserService _userService;
    private IDbService _idb;
    private ClearService _clearService;

    [SetUp]
    public void Setup()
    {
        NpgsqlConnection connection =
            new NpgsqlConnection("Host=localhost;Port=5432;Database=mydatabase;UserId=postgres;Password=password");
        connection.Open();
        IConfiguration iconf = new ConfigurationManager();
        iconf["ConnectionStrings:Wagerdb"] =
            "Host=localhost;Port=5432;Database=mydatabase;UserId=postgres;Password=password";
        _idb = new DbService(iconf);
        _userService = new UserService(_idb);
        _clearService = new ClearService(_idb);
    }

    [Test]
    public void TestCreateUserEmptyConstructor()
    {
        _clearService.ClearDatabase();;
        
        //Creates User with Empty Constructor
        var localUser = new User();
        localUser.Id = 1;
        localUser.Email = "Email";
        localUser.Name = "Name";
        localUser.Password = "Password";

        var result = _userService.CreateUser(localUser);
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Email, Is.EqualTo("Email"));
        Assert.That(result.Name, Is.EqualTo("Name"));
        Assert.That(result.Password, Is.EqualTo("Password"));
    }

    [Test]
    public void TestCreateUserKonstructor()
    {
        _clearService.ClearDatabase();;
        
        //Creates User with Constructor
        User localUser = new User(1, "TestName", "test@gmail.com", "TestPassword");
        
        var result = _userService.CreateUser(localUser);
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("TestName"));
        Assert.That(result.Email, Is.EqualTo("test@gmail.com"));
        Assert.That(result.Password, Is.EqualTo("TestPassword"));
    }

    [Test]
    public void TestGetAllUser()
    {
        _clearService.ClearDatabase();;
        
        //Create 3 users
        var localUser1 = new User(2, "Name1", "Email1", "Password1");
        var localUser2 = new User(3, "Name2", "Email2", "Password2");
        var localUser3 = new User(4, "Name3", "Email3", "Password3");
        _userService.CreateUser(localUser1);
        _userService.CreateUser(localUser2);
        _userService.CreateUser(localUser3);
        
        //Get all users
        var result = _userService.GetAllUsers();
        Assert.IsNotEmpty(result);
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0].Id, Is.EqualTo(1));
        Assert.That(result[1].Id, Is.EqualTo(2));
        Assert.That(result[2].Id, Is.EqualTo(3));
        Assert.That(result[0].Name, Is.EqualTo("Name1"));
        Assert.That(result[1].Name, Is.EqualTo("Name2"));
        Assert.That(result[2].Name, Is.EqualTo("Name3"));
        Assert.That(result[0].Email, Is.EqualTo("Email1"));
        Assert.That(result[1].Email, Is.EqualTo("Email2"));
        Assert.That(result[2].Email, Is.EqualTo("Email3"));
        Assert.That(result[0].Password, Is.EqualTo("Password1"));
        Assert.That(result[1].Password, Is.EqualTo("Password2"));
        Assert.That(result[2].Password, Is.EqualTo("Password3"));
    }

    [Test]
    public void TestUpdateUser()
    {
        _clearService.ClearDatabase();
        
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
        _clearService.ClearDatabase();;
        
        //Create User
        var localUser = new User(1, "Name", "Email", "Password");
        _userService.CreateUser(localUser);
        
        //Get User by ID
        var result = _userService.GetUserById(1);
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Name"));
        Assert.That(result.Email, Is.EqualTo("Email"));
        Assert.That(result.Password, Is.EqualTo("Password"));
    }

    [Test]
    public void TestDeletUser()
    {
        _clearService.ClearDatabase();;
        
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
        _clearService.ClearDatabase();;
        
        //Create User
        var localUser = new User(1, "Name", "mail@mail.de", "Password");
        _userService.CreateUser(localUser);
        
        //Get User by Email
        var result = _userService.GetUserByEmail("mail@mail.de");
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Name"));
        Assert.That(result.Email, Is.EqualTo("mail@mail.de"));
        Assert.That(result.Password, Is.EqualTo("Password"));
    }

    [Test]
    public void TestGetUserByPassword()
    {
        _clearService.ClearDatabase();;
        
        //Create User
        var localUser = new User(1, "Name", "Email", "Password");
        _userService.CreateUser(localUser);
        
        //Get User by Password
        var result = _userService.GetUserByPassword("Password");
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Name"));
        Assert.That(result.Email, Is.EqualTo("Email"));
        Assert.That(result.Password, Is.EqualTo("Password"));
    }
    
    [Test]
    public void TestDoesUserPasswordExist()
    {
        _clearService.ClearDatabase();;
        
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
        _clearService.ClearDatabase();;

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
        _clearService.ClearDatabase();;
        
        //Create User
        var localUser = new User(1, "Name", "Email", "Password");
        _userService.CreateUser(localUser);
        
        //Get User if Password exists
        var result = _userService.GetUserIfPasswordExists("Password");
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Name, Is.EqualTo("Name"));
        Assert.That(result.Email, Is.EqualTo("Email"));
        Assert.That(result.Password, Is.EqualTo("Password"));
    }
}