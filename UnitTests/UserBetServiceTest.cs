namespace UnitTests;

[TestFixture]
public class UserBetServiceTest
{
    
    private BetService _betService;
    private UserService _userService;
    private UserBetService _userBetService;
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
        _userService = new UserService(_idb);
        _userBetService = new UserBetService(_idb);
        _caseService = new CaseService(_idb);
        _clearService = new ClearService(_idb);
    }
    
    [Test]
    public void TestCreateUserBetEmptyConstructor()
    {
        _clearService.ClearDatabase();
        
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
        var localUserBet = new UserBet();
        localUserBet.Id = 1;
        localUserBet.User_Id = 1;
        localUserBet.Bet_Id = 1;
        localUserBet.Case_Id = 1;
        localUserBet.Amount = 1;
        
        var result = _userBetService.CreateUserBet(localUserBet);
        Assert.AreEqual(1, result);
    }
    
    [Test]
    public void TestCreateUserBetKonstructor()
    {
        _clearService.ClearDatabase();
        
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
        var localUserBet = new UserBet(1, 1, 1, 1, 1);
        
        var result = _userBetService.CreateUserBet(localUserBet);
        Assert.AreEqual(1, result);
    }
    
    [Test]
    public void TestDeleteUserBetByUserAndBet()
    {
        _clearService.ClearDatabase();
        
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
        var localUserBet = new UserBet(1, 1, 1, 1, 1);
        _userBetService.CreateUserBet(localUserBet);
        
        var result = _userBetService.DeleteUserBet(localUser, localBet);
        Assert.IsTrue(result);
    }
    
    [Test]
    public void TestDeleteUserBetByUserBet()
    {
        _clearService.ClearDatabase();
        
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
        var localUserBet = new UserBet(1, 1, 1, 1, 1);
        _userBetService.CreateUserBet(localUserBet);
        
        var result = _userBetService.DeleteUserBet(localUserBet);
        Assert.IsTrue(result);
    }
    
    [Test]
    public void TestGetUserBetById()
    {
        _clearService.ClearDatabase();
        
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
        var localUserBet = new UserBet(1, 1, 1, 1, 1);
        _userBetService.CreateUserBet(localUserBet);
        
        var result = _userBetService.GetUserBetById(1);
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetUserBetByUserIdAndBetId()
    {
        _clearService.ClearDatabase();
        
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
        var localUserBet = new UserBet(1, 1, 1, 1, 1);
        _userBetService.CreateUserBet(localUserBet);
        
        var result = _userBetService.GetUserBetById(1, 1);
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetAllUserIdsFromBet()
    {
        _clearService.ClearDatabase();
        
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
        var localUserBet = new UserBet(1, 1, 1, 1, 1);
        _userBetService.CreateUserBet(localUserBet);
        
        var result = _userBetService.GetAllUserIdsFromBet(localBet);
        Assert.IsNotEmpty(result);
    }
}