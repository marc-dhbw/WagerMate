namespace UnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        TestingUserService tus = new TestingUserService();
        tus.TestCreateUser();
    }

    [Test]
    public void Test2()
    {
        IntegrationTests it = new IntegrationTests();
        it.TestDbConnection();
    }
}