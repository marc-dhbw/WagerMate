namespace UnitTests;

public class CaseServiceTest
{
    private BetService _betService;
    private UserService _userService;
    private UserBetService _userBetService;
    private CaseService _caseService;
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
        _betService = new BetService(_idb);
        _userService = new UserService(_idb);
        _userBetService = new UserBetService(_idb);
        _caseService = new CaseService(_idb);
    }
    
    public void ClearDatabase()
    {
        _idb.CustomSql("DELETE FROM public.userbet");
        _idb.CustomSql("DELETE FROM public.case");
        _idb.CustomSql("DELETE FROM public.user");
        _idb.CustomSql("DELETE FROM public.bet");
        _idb.CustomSql("ALTER SEQUENCE userbet_id_seq RESTART WITH 1");
        _idb.CustomSql("ALTER SEQUENCE case_id_seq RESTART WITH 1");
        _idb.CustomSql("ALTER SEQUENCE user_id_seq RESTART WITH 1");
        _idb.CustomSql("ALTER SEQUENCE bet_id_seq RESTART WITH 1");
    } 
   
    [Test]
    public void TestCreateCaseEmptyConstructor()
    {
        ClearDatabase();
        
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
        ClearDatabase();
        
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
        ClearDatabase();
        
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
        ClearDatabase();
        
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
        ClearDatabase();
        
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
        ClearDatabase();
        
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
        ClearDatabase();
        
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