using WagerMate.Data.bet;
using WagerMate.Services.betting;
using WagerMate.Services.database;

namespace WagerMate.Service_Implementation.betting;

public class BetService : IBetService
{
    private readonly IDbService _service;

    public BetService(IDbService service)
    {
        _service = service;
    }

    public Bet CreateBet(Bet bet)
    {
        var betId = _service.CreateWithReturn(
            "INSERT INTO public.bet(title, description, invitation_code, access, state, created, expiration) VALUES(@Title, @Description, @Invitation_Code, @Access, @State, @Created, @Expiration)  RETURNING id",
            bet);
        bet.Id = betId;
        return bet;
    }

    public List<Bet> GetAllBets()
    {
        var result = _service.GetAll<Bet>("SELECT * FROM public.bet");
        return result;
    }

    public bool UpdateBet(Bet bet)
    {
        var result = _service.Update<Bet>(
            "UPDATE public.bet SET id=@Id, title = @Title, description = @Description, invitation_code = @Invitation_Code, access=@Access, state=@State, created=@Created, expiration=@Expiration WHERE bet.id = @Id",
            bet);
        return result;
    }

    public Bet GetBetById(int key)
    {
        var result = _service.GetById<Bet>("SELECT * FROM public.bet WHERE bet.Id = @Id", new { id = key });
        return result;
    }

    public bool DeleteBet(int key)
    {
        var result = _service.Delete<Bet>("DELETE FROM public.bet WHERE bet.Id = @Id", new { id = key });
        return result;
    }

    public List<Bet> GetBetsByUserId(int userId)
    {
        var result = _service.GetAllWithParams<Bet>(
            "SELECT bet.* FROM public.bet INNER JOIN public.userbet ON bet.id = userbet.bet_id WHERE userbet.user_id = @UserId",
            new { UserId = userId }
        );
        return result;
    }

    public Bet? GetBetByInviteCode(string inviteCode)
    {
        var result =
            _service.GetIfExists<Bet>("SELECT EXISTS ( SELECT 1 FROM public.bet where Invitation_code = @inviteCode)",
                new { inviteCode },"SELECT * FROM public.bet WHERE Invitation_code = @inviteCode", new {inviteCode});
        return result.Item1 ? result.Item2 : null;
    }
}