namespace UnitTests;

[TestFixture]
public class BetServiceTest
{
    private BetService _betService;
    private UserService _userService;
    private UserBetService _userBetService;
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
        _userService = new UserService(_idb);
        _userBetService = new UserBetService(_idb);
        _caseService = new CaseService(_idb);
        _clearService = new ClearService(_idb);
    }

    [Test]
    public void TestCreateBetEmptyConstructor()
    {
        _clearService.ClearDatabase();

        //Creates Bet with Empty Constructor
        var localBet = new Bet();
        localBet.Id = 1;
        localBet.Title = "Title";
        localBet.Description = "Description";
        localBet.InvitationCode = "InvitationCode";
        localBet.Created = DateTime.Today;
        localBet.Expiration = DateTime.Today;
        localBet.BetAccess = Access.Public;
        localBet.BetState = State.Active;

        var result = _betService.CreateBet(localBet);

        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Title, Is.EqualTo("Title"));
        Assert.That(result.Description, Is.EqualTo("Description"));
        Assert.That(result.InvitationCode, Is.EqualTo("InvitationCode"));
        Assert.That(result.Created, Is.EqualTo(DateTime.Today));
        Assert.That(result.Expiration, Is.EqualTo(DateTime.Today));
        //Assert.That(result.BetAccess, Is.EqualTo(Access.Private));
        Assert.That(result.BetState, Is.EqualTo(State.Active));
    }

    [Test]
    public void TestCreateBetKonstructor()
    {
        _clearService.ClearDatabase();

        //Creates Bet with Constructor
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Today, DateTime.Today,
            Access.Private, State.Active);

        var result = _betService.CreateBet(localBet);

        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Title, Is.EqualTo("Title"));
        Assert.That(result.Description, Is.EqualTo("Description"));
        Assert.That(result.InvitationCode, Is.EqualTo("InvitationCode"));
        Assert.That(result.Created, Is.EqualTo(DateTime.Today));
        Assert.That(result.Expiration, Is.EqualTo(DateTime.Today));
        Assert.That(result.BetAccess, Is.EqualTo(Access.Private));
        Assert.That(result.BetState, Is.EqualTo(State.Active));
    }

    [Test]
    public void TestGetAllBets()
    {
        _clearService.ClearDatabase();

        //Create 3 Bets
        var localBet1 = new Bet(1, "Title1", "Description1", "InvitationCode1", DateTime.Today, DateTime.Today,
            Access.Private,
            State.Active);
        var localBet2 = new Bet(2, "Title2", "Description2", "InvitationCode2", DateTime.Today, DateTime.Today,
            Access.Public,
            State.Pending);
        var localBet3 = new Bet(3, "Title3", "Description3", "InvitationCode3", DateTime.Today, DateTime.Today,
            Access.Restricted,
            State.Closed);
        _betService.CreateBet(localBet1);
        _betService.CreateBet(localBet2);
        _betService.CreateBet(localBet3);

        //Get all Bets
        var result = _betService.GetAllBets();
        Assert.That(result.Count(), Is.EqualTo(3));
        Assert.That(result[0].Id, Is.EqualTo(1));
        Assert.That(result[1].Id, Is.EqualTo(2));
        Assert.That(result[2].Id, Is.EqualTo(3));
        Assert.That(result[0].Title, Is.EqualTo("Title1"));
        Assert.That(result[1].Title, Is.EqualTo("Title2"));
        Assert.That(result[2].Title, Is.EqualTo("Title3"));
        Assert.That(result[0].Description, Is.EqualTo("Description1"));
        Assert.That(result[1].Description, Is.EqualTo("Description2"));
        Assert.That(result[2].Description, Is.EqualTo("Description3"));
        //Assert.That(result[0].InvitationCode, Is.EqualTo("InvitationCode1"));
        //Assert.That(result[1].InvitationCode, Is.EqualTo("InvitationCode2"));
        //Assert.That(result[2].InvitationCode, Is.EqualTo("InvitationCode3"));
        Assert.That(result[0].Created, Is.EqualTo(DateTime.Today));
        Assert.That(result[1].Created, Is.EqualTo(DateTime.Today));
        Assert.That(result[2].Created, Is.EqualTo(DateTime.Today));
        Assert.That(result[0].Expiration, Is.EqualTo(DateTime.Today));
        Assert.That(result[1].Expiration, Is.EqualTo(DateTime.Today));
        Assert.That(result[2].Expiration, Is.EqualTo(DateTime.Today));
        //Assert.That(result[0].BetAccess, Is.EqualTo(Access.Private));
        Assert.That(result[1].BetAccess, Is.EqualTo(Access.Public));
        //Assert.That(result[2].BetAccess, Is.EqualTo(Access.Restricted));
        //Assert.That(result[0].BetState, Is.EqualTo(State.Active));
        Assert.That(result[1].BetState, Is.EqualTo(State.Pending));
        //Assert.That(result[2].BetState, Is.EqualTo(State.Closed));
    }

    [Test]
    public void TestUpdateBet()
    {
        _clearService.ClearDatabase();

        //Create Bet to be updated
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Private,
            State.Active);
        var updatedBet = new Bet(1, "updatedTitle", "updatedDescription", "updatedInvitationCode", DateTime.Now,
            DateTime.Now, Access.Public, State.Pending);
        _betService.CreateBet(localBet);

        //Update Bet
        var result = _betService.UpdateBet(updatedBet);
        Assert.IsTrue(result);
    }

    [Test]
    public void TestGetBetById()
    {
        _clearService.ClearDatabase();

        //Create Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Today, DateTime.Today,
            Access.Private,
            State.Active);
        _betService.CreateBet(localBet);

        //GetBetById
        var result = _betService.GetBetById(1);
        Assert.IsNotNull(result);
        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Title, Is.EqualTo("Title"));
        Assert.That(result.Description, Is.EqualTo("Description"));
        //Assert.That(result.InvitationCode, Is.EqualTo("InvitationCode"));
        Assert.That(result.Created, Is.EqualTo(DateTime.Today));
        Assert.That(result.Expiration, Is.EqualTo(DateTime.Today));
        //Assert.That(result.BetAccess, Is.EqualTo(Access.Private));
        //Assert.That(result.BetState, Is.EqualTo(State.Active));
    }

    [Test]
    public void TestDeleteBet()
    {
        _clearService.ClearDatabase();

        //Create Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Now, DateTime.Now, Access.Private,
            State.Active);
        _betService.CreateBet(localBet);

        //Delete Bet
        var result = _betService.DeleteBet(1);
        Assert.IsTrue(result);
    }

    [Test]
    public void TestGetBetsByUserId()
    {
        _clearService.ClearDatabase();

        //Create Bet
        var localBet1 = new Bet(1, "Title1", "Description1", "InvitationCode1", DateTime.Now, DateTime.Now,
            Access.Private,
            State.Active);
        var localBet2 = new Bet(2, "Title2", "Description2", "InvitationCode2", DateTime.Now, DateTime.Now,
            Access.Public,
            State.Pending);
        var localBet3 = new Bet(3, "Title3", "Description3", "InvitationCode3", DateTime.Now, DateTime.Now,
            Access.Restricted,
            State.Closed);
        _betService.CreateBet(localBet1);
        _betService.CreateBet(localBet2);
        _betService.CreateBet(localBet3);

        //Create User
        var localUser = new User(1, "Name", "Email", "Password");
        _userService.CreateUser(localUser);

        //Create Case
        var localCase = new Case(1, 1, "CaseType");
        _caseService.CreateCase(localCase);

        //Create UserBet
        var localUserBet1 = new UserBet(1, 1, 1, 1, 10.0);
        var localUserBet2 = new UserBet(2, 1, 2, 1, 10.0);
        var localUserBet3 = new UserBet(3, 1, 3, 1, 10.0);
        _userBetService.CreateUserBet(localUserBet1);
        _userBetService.CreateUserBet(localUserBet2);
        _userBetService.CreateUserBet(localUserBet3);

        //GetBetsByUserId
        var result = _betService.GetBetsByUserId(1);
        Assert.That(result.Count(), Is.EqualTo(3));
        Assert.That(result[0].Id, Is.EqualTo(1));
        Assert.That(result[1].Id, Is.EqualTo(2));
        Assert.That(result[2].Id, Is.EqualTo(3));
        Assert.That(result[0].Title, Is.EqualTo("Title1"));
        Assert.That(result[1].Title, Is.EqualTo("Title2"));
        Assert.That(result[2].Title, Is.EqualTo("Title3"));
        Assert.That(result[0].Description, Is.EqualTo("Description1"));
        Assert.That(result[1].Description, Is.EqualTo("Description2"));
        Assert.That(result[2].Description, Is.EqualTo("Description3"));
        //Assert.That(result[0].InvitationCode, Is.EqualTo("InvitationCode1"));
        //Assert.That(result[1].InvitationCode, Is.EqualTo("InvitationCode2"));
        //Assert.That(result[2].InvitationCode, Is.EqualTo("InvitationCode3"));
        Assert.That(result[0].Created, Is.EqualTo(DateTime.Today));
        Assert.That(result[1].Created, Is.EqualTo(DateTime.Today));
        Assert.That(result[2].Created, Is.EqualTo(DateTime.Today));
        Assert.That(result[0].Expiration, Is.EqualTo(DateTime.Today));
        Assert.That(result[1].Expiration, Is.EqualTo(DateTime.Today));
        Assert.That(result[2].Expiration, Is.EqualTo(DateTime.Today));
        //Assert.That(result.[0].BetAccess, Is.EqualTo(Access.Private));
        Assert.That(result[1].BetAccess, Is.EqualTo(Access.Public));
        //Assert.That(result[2].BetAccess, Is.EqualTo(Access.Restricted));
        //Assert.That(result.[0].BetState, Is.EqualTo(State.Active));
        Assert.That(result[1].BetState, Is.EqualTo(State.Pending));
        //Assert.That(result[2].BetState, Is.EqualTo(State.Closed));
    }

    [Test]
    public void TestGetBetByInviteCode()
    {
        _clearService.ClearDatabase();

        //Create Bet
        var localBet = new Bet(1, "Title", "Description", "InvitationCode", DateTime.Today, DateTime.Today,
            Access.Private,
            State.Active);
        _betService.CreateBet(localBet);

        //GetBetByInviteCode
        var result = _betService.GetBetByInviteCode("InvitationCode");

        Assert.That(result.Id, Is.EqualTo(1));
        Assert.That(result.Title, Is.EqualTo("Title"));
        Assert.That(result.Description, Is.EqualTo("Description"));
        //Assert.That(result.InvitationCode, Is.EqualTo("InvitationCode"));
        Assert.That(result.Created, Is.EqualTo(DateTime.Today));
        Assert.That(result.Expiration, Is.EqualTo(DateTime.Today));
        //Assert.That(result.BetAccess, Is.EqualTo(Access.Private));
        //Assert.That(result.BetState, Is.EqualTo(State.Active));
    }
}