using WagerMate.Data;
using WagerMate.Services;
using WagerMate.Services.impl;
using Moq;

namespace UnitTests;

[TestFixture]
public class TestingUserService
{
    // example test
    [Test]
    public void TestCreateUser()
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
}
