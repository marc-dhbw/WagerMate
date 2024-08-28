namespace UnitTests;

public class CaseServiceTest
{
    private BetService _betService;
    private CaseService _caseService;
    private NpgsqlConnection _connection;
    private IDbService _idb;
    private ClearService _clearService;

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
        Assert.AreEqual(1, result);
    }
    
    [Test]
    public void TestCreateCaseConstructor()
    {
        _clearService.ClearDatabase();;
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, 1, "Casetype");

        var result = _caseService.CreateCase(localCase);
        Assert.AreEqual(1, result);
    }

    [Test]
    public void TestGetCaseById()
    {
        _clearService.ClearDatabase();;
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, 1, "Casetype");
        _caseService.CreateCase(localCase);

        var result = _caseService.GetCaseById(1);
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetCasesByBetId()
    {
        _clearService.ClearDatabase();;
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);

        //Creates Case
        var localCase1 = new Case(1, 1, "Casetype");
        var localCase2 = new Case(2, 1, "Casetype");
        var localCase3 = new Case(3, 1, "Casetype");
        _caseService.CreateCase(localCase1);
        _caseService.CreateCase(localCase2);
        _caseService.CreateCase(localCase3);

        var result = _caseService.GetCasesByBetId(1);
        Assert.IsNotEmpty(result);
    }
    
    [Test]
    public void TestUpdateCase()
    {
        _clearService.ClearDatabase();;
        
        //Creates Bet for the Case
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);

        //Creates Case with Constructor
        var localCase = new Case(1, 1, "Casetype");
        _caseService.CreateCase(localCase);

        var updatedCase = new Case(1,1,"updatedCasetype");
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
        var localCase = new Case(1, 1, "Casetype");

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
        var localCase1 = new Case(1, 1, "Casetype");
        var localCase2 = new Case(2, 1, "Casetype");
        var localCase3 = new Case(3, 1, "Casetype");
        _caseService.CreateCase(localCase1);
        _caseService.CreateCase(localCase2);
        _caseService.CreateCase(localCase3);

        var result = _caseService.DeleteCasesOfBetId(1);
        Assert.IsTrue(result);
    }
}