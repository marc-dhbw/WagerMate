namespace UnitTests;

public class HashServiceTest
{
    private readonly IHashService _hashService;
    public HashServiceTest()
    {
        _hashService = new HashService();
    }
    [Test]
    //Tests if you get the same Hash with the same values (is it deterministic)
    public void TestCreateHash()
    {
        string email = "test@mail.de";
        string password = "&/()!§/SasSDFäÖ#";
        var firstHash = _hashService.CreateHash(email, password);
        var secondHash = _hashService.CreateHash(email, password);

        Assert.AreEqual(firstHash, secondHash);
    }
    
    [Test]
    //Tests if it can output diffrent Hashes for diffrent values
    public void TestCreateHashDiffrent()
    {
        string email = "test@mail.de";
        string password = "&/()!§/SasSDFäÖ#";
        string newemail = "asdf@mail.de";
        string newpassword = "iwejfisd897%$%&/()*'";
        
        var firstHash = _hashService.CreateHash(email, password);
        var secondHash = _hashService.CreateHash(newemail, newpassword);
        Assert.AreNotEqual(firstHash, secondHash);
    }
}