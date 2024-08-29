namespace UnitTests;

public class CaseServiceTest
{
    private BetService _betService;
    private CaseService _caseService;
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
        _betService = new BetService(_idb);
        _caseService = new CaseService(_idb);
        _clearService = new ClearService(_idb);
    }
    
    [Test]
    public void TestCreateCaseEmptyConstructor()
    {
        _clearService.ClearDatabase();;
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case with Empty Constructor
        var localCase = new Case();
        localCase.Id = 1;
        localCase.Bet_Id = 1;
        localCase.Casetype = "Casetype";
        
        var result = _caseService.CreateCase(localCase);
        Assert.That(result, Is.EqualTo(1));
    }
    
    [Test]
    public void TestCreateCaseConstructor()
    {
        _clearService.ClearDatabase();;
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, "Casetype");

        var result = _caseService.CreateCase(localCase);
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void TestGetCaseById()
    {
        _clearService.ClearDatabase();;
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, "Casetype");
        _caseService.CreateCase(localCase);

        var result = _caseService.GetCaseById(1);
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Bet_Id, Is.EqualTo(1));
        Assert.That(result.Casetype, Is.EqualTo("Casetype"));
    }
    
    [Test]
    public void TestGetCasesByBetId()
    {
        _clearService.ClearDatabase();;
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);

        //Creates Case
        var localCase1 = new Case(1, "Casetype1");
        var localCase2 = new Case(1, "Casetype2");
        var localCase3 = new Case(1, "Casetype3");
        _caseService.CreateCase(localCase1);
        _caseService.CreateCase(localCase2);
        _caseService.CreateCase(localCase3);

        var result = _caseService.GetCasesByBetId(1);
        Assert.IsNotEmpty(result);
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0].Id, Is.EqualTo(1));
        Assert.That(result[1].Id, Is.EqualTo(2));
        Assert.That(result[2].Id, Is.EqualTo(3));
        Assert.That(result[0].Bet_Id, Is.EqualTo(1));
        Assert.That(result[1].Bet_Id, Is.EqualTo(1));
        Assert.That(result[2].Bet_Id, Is.EqualTo(1));
        Assert.That(result[0].Casetype, Is.EqualTo("Casetype1"));
        Assert.That(result[1].Casetype, Is.EqualTo("Casetype2"));
        Assert.That(result[2].Casetype, Is.EqualTo("Casetype3"));
    }
    
    [Test]
    public void TestUpdateCase()
    {
        _clearService.ClearDatabase();;
        
        //Creates Bet for the Case
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);

        //Creates Case with Constructor
        var localCase = new Case(1, "Casetype");
        _caseService.CreateCase(localCase);

        var updatedCase = new Case(1, "updatedCasetype");
        updatedCase.Id = 1;
        _caseService.CreateCase(updatedCase);
        
        var result = _caseService.UpdateCase(updatedCase);
        Assert.IsTrue(result);
    }
    
    [Test]
    public void TestDeleteCase()
    {
        _clearService.ClearDatabase();;
        
        //Creates Bet for the Case
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);

        //Creates Case with Constructor
        var localCase = new Case(1, "Casetype");

        _caseService.CreateCase(localCase);

        var result = _caseService.DeleteCase(1);
        Assert.IsTrue(result);
    }
    
    [Test]
    public void DeleteCasesOfBetId()
    {
        _clearService.ClearDatabase();;
        
        //Creates Bet for the Case
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);

        //Creates Case with Constructor
        var localCase1 = new Case(1, "Casetype");
        var localCase2 = new Case(1, "Casetype");
        var localCase3 = new Case(1, "Casetype");
        _caseService.CreateCase(localCase1);
        _caseService.CreateCase(localCase2);
        _caseService.CreateCase(localCase3);

        var result = _caseService.DeleteCasesOfBetId(1);
        Assert.IsTrue(result);
    }
}