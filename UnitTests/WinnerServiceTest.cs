namespace UnitTests;

[TestFixture]
public class WinnerServiceTest
{
    
    private BetService _betService;
    private UserService _userService;
    private UserBetService _userBetService;
    private CaseService _caseService;
    private WinnerService _winnerService;
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
        _winnerService = new WinnerService(_idb);
    }
    
    public void ClearDatabase()
    {
        _idb.CustomSql("DELETE FROM public.winner");
        _idb.CustomSql("DELETE FROM public.userbet");
        _idb.CustomSql("DELETE FROM public.case");
        _idb.CustomSql("DELETE FROM public.user");
        _idb.CustomSql("DELETE FROM public.bet");
        _idb.CustomSql("ALTER SEQUENCE userbet_id_seq RESTART WITH 1");
        _idb.CustomSql("ALTER SEQUENCE case_id_seq RESTART WITH 1");
        _idb.CustomSql("ALTER SEQUENCE user_id_seq RESTART WITH 1");
        _idb.CustomSql("ALTER SEQUENCE bet_id_seq RESTART WITH 1");
        _idb.CustomSql("ALTER SEQUENCE winner_id_seq RESTART WITH 1");
    }
    
    [Test]
    public void TestCreateWinnerEmptyConstructor()
    {
        ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, 1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner = new Winner();
        localWinner.Id = 1;
        localWinner.Bet_Id = 1;
        localWinner.UserBet_Id = 1;
        
        var result = _winnerService.CreateWinner(localWinner);
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestCreateWinnerConstructor()
    {
        ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, 1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner = new Winner(1, 1, 1);
        var result = _winnerService.CreateWinner(localWinner);
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetWinnerById()
    {
        ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, 1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner = new Winner(1, 1, 1);
        _winnerService.CreateWinner(localWinner);
        
        //GetWinnerById
        var result = _winnerService.GetWinnerById(1);
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetWinnersByBetId()
    {
        ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, 1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner = new Winner(1, 1, 1);
        _winnerService.CreateWinner(localWinner);
        
        //GetWinnersByBetId
        var result = _winnerService.GetWinnersByBetId(1);
        Assert.IsNotEmpty(result);
    }
    
    [Test]
    public void TestUpdateWinner()
    {
        ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, 1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner = new Winner(1, 1, 1);
        _winnerService.CreateWinner(localWinner);
        
        //UpdateWinner
        var result = _winnerService.UpdateWinner(localWinner);
        Assert.IsTrue(result);
    }
    
    [Test]
    public void TestDeleteWinner()
    {
        ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, 1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner = new Winner(1, 1, 1);
        _winnerService.CreateWinner(localWinner);
        
        //DeleteWinner
        var result = _winnerService.DeleteWinner(1);
        Assert.IsTrue(result);
    }
    
}