namespace UnitTests;

[TestFixture]
public class BetServiceTest
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
    public void TestCreateBetEmptyConstructor()
    {
        ClearDatabase();
        
        //Creates Bet with Empty Constructor
        var localBet = new Bet();
        localBet.Id = 1;
        localBet.Title = "Title";
        localBet.Description = "Description";
        localBet.InvitationCode = "InvitationCode";
        localBet.Created = DateTime.Now;
        localBet.Expiration = DateTime.Now;
        localBet.BetAccess = Access.Public;
        localBet.BetState = State.Active;

        var result = _betService.CreateBet(localBet);
        Assert.IsNotNull(result);
    }

    [Test]
    public void TestCreateBetKonstructor()
    {
        ClearDatabase();
        
        //Creates Bet with Constructor
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Private, State.Active);
        
        var result = _betService.CreateBet(localBet);
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetAllBets()
    {
        ClearDatabase();
        
        //Create 3 Bets
        var localBet1 = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Private, State.Active);
        var localBet2 = new Bet(2, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Private, State.Active);
        var localBet3 = new Bet(3, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Private, State.Active);
        _betService.CreateBet(localBet1);
        _betService.CreateBet(localBet2);
        _betService.CreateBet(localBet3);
        
        //Get all Bets
        var result = _betService.GetAllBets();
        Assert.IsNotEmpty(result);
    }
    
    [Test]
    public void TestUpdateBet()
    {
        ClearDatabase();
        
        //Create Bet to be updated
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Private, State.Active);
        var updatedBet = new Bet(1, "updatedTitle", "updatedDescription", "updatedInvitationCode", DateTime.Now, DateTime.Now, Access.Public, State.Pending);
        _betService.CreateBet(localBet);
        
        //Update Bet
        var result = _betService.UpdateBet(updatedBet);
        Assert.IsTrue(result);
    }
    
    [Test]
    public void TestGetBetById()
    {
        ClearDatabase();
        
        //Create Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Private, State.Active);
        _betService.CreateBet(localBet);
        
        //GetBetById
        var result = _betService.GetBetById(1);
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestDeleteBet()
    {
        ClearDatabase();
        
        //Create Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Private, State.Active);
        _betService.CreateBet(localBet);
        
        //Delete Bet
        var result = _betService.DeleteBet(1);
        Assert.IsTrue(result);
    }
    
    [Test]
    public void TestGetBetsByUserId()
    {
        ClearDatabase();
        
        //Create Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Private, State.Active);
        _betService.CreateBet(localBet);
        
        //Create User
        var localUser = new User(1, "Name", "Email", "Password");
        _userService.CreateUser(localUser);
        
        //Create Case
        var localCase = new Case(1, 1, "CaseType");
        _caseService.CreateCase(localCase);
        
        //Create UserBet
        var localUserBet = new UserBet(1, 1, 1, 1, 10.0);
        _userBetService.CreateUserBet(localUserBet);

        //GetBetsByUserId
        var result = _betService.GetBetsByUserId(1);
        Assert.IsNotEmpty(result);
    }
    
    [Test]
    public void TestGetBetByInviteCode()
    {
        ClearDatabase();
        
        //Create Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Private, State.Active);
        _betService.CreateBet(localBet);
        
        //GetBetByInviteCode
        var result = _betService.GetBetByInviteCode("InvitationCode");
        Assert.IsNotNull(result);
    }
}