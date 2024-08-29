namespace UnitTests;

[TestFixture]
public class WinnerServiceTest
{
    
    private BetService _betService;
    private UserService _userService;
    private UserBetService _userBetService;
    private CaseService _caseService;
    private WinnerService _winnerService;
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
        _userService = new UserService(_idb);
        _userBetService = new UserBetService(_idb);
        _caseService = new CaseService(_idb);
        _winnerService = new WinnerService(_idb);
        _clearService = new ClearService(_idb);
    }
    
    [Test]
    public void TestCreateWinnerEmptyConstructor()
    {
        _clearService.ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner = new Winner();
        localWinner.Id = 1;
        localWinner.Bet_Id = 1;
        localWinner.UserBet_Id = 1;
        localWinner.Amount = 10.0;
        
        var result = _winnerService.CreateWinner(localWinner);
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Bet_Id, Is.EqualTo(1));
        Assert.That(result.UserBet_Id, Is.EqualTo(1));
        Assert.That(result.Amount, Is.EqualTo(10.0));
    }
    
    [Test]
    public void TestCreateWinnerConstructor()
    {
        _clearService.ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner = new Winner(1, 1, 1, 10.0);
        var result = _winnerService.CreateWinner(localWinner);
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Bet_Id, Is.EqualTo(1));
        Assert.That(result.UserBet_Id, Is.EqualTo(1));
        Assert.That(result.Amount, Is.EqualTo(10.0));
    }
    
    [Test]
    public void TestGetWinnerById()
    {
        _clearService.ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner = new Winner(1, 1, 1, 10.1);
        _winnerService.CreateWinner(localWinner);
        
        //GetWinnerById
        var result = _winnerService.GetWinnerById(1);
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Bet_Id, Is.EqualTo(1));
        Assert.That(result.UserBet_Id, Is.EqualTo(1));
        Assert.That(result.Amount, Is.EqualTo(10.1));
    }
    
    [Test]
    public void TestGetWinnersByBetId()
    {
        _clearService.ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner1 = new Winner(1, 1, 1, 10.1);
        var localWinner2 = new Winner(2, 1, 1, 10.2);
        var localWinner3 = new Winner(3, 1, 1, 10.3);
        _winnerService.CreateWinner(localWinner1);
        _winnerService.CreateWinner(localWinner2);
        _winnerService.CreateWinner(localWinner3);
        
        //GetWinnersByBetId
        var result = _winnerService.GetWinnersByBetId(1);
        Assert.IsNotEmpty(result);
        Assert.That(result.Count(), Is.EqualTo(3));
        Assert.That(result[0].Id, Is.EqualTo(1));
        Assert.That(result[1].Id, Is.EqualTo(2));
        Assert.That(result[2].Id, Is.EqualTo(3));
        Assert.That(result[0].Bet_Id, Is.EqualTo(1));
        Assert.That(result[1].Bet_Id, Is.EqualTo(1));
        Assert.That(result[2].Bet_Id, Is.EqualTo(1));
        Assert.That(result[0].UserBet_Id, Is.EqualTo(1));
        Assert.That(result[1].UserBet_Id, Is.EqualTo(1));
        Assert.That(result[2].UserBet_Id, Is.EqualTo(1));
        Assert.That(result[0].Amount, Is.EqualTo(10.1));
        Assert.That(result[1].Amount, Is.EqualTo(10.2));
        Assert.That(result[2].Amount, Is.EqualTo(10.3));
    }
    
    [Test]
    public void TestUpdateWinner()
    {
        _clearService.ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner = new Winner(1, 1, 1, 10.1);
        _winnerService.CreateWinner(localWinner);
        
        //UpdateWinner
        var result = _winnerService.UpdateWinner(localWinner);
        Assert.IsTrue(result);
    }
    
    [Test]
    public void TestDeleteWinner()
    {
        _clearService.ClearDatabase();
        
        //Creates User
        var localUser = new User(1, "Username", "Password", "Email");
        _userService.CreateUser(localUser);
        
        //Creates Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Active);
        _betService.CreateBet(localBet);
        
        //Creates Case
        var localCase = new Case(1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Creates UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 100);
        _userBetService.CreateUserBet(localUserBet);
        
        //Creates Winner
        var localWinner = new Winner(1, 1, 1, 10.1);
        _winnerService.CreateWinner(localWinner);
        
        //DeleteWinner
        var result = _winnerService.DeleteWinner(1);
        Assert.IsTrue(result);
    }
    
}