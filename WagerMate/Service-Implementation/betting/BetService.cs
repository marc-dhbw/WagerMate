namespace WagerMate.Services.impl;

using WagerMate.Data;

public class BetService : IBetService
{
    private IDbService _service;

    public BetService(IDbService service)
    {
        _service = service;
    }

    public Bet CreateBet(Bet bet)
    {
        _service.Create<Bet>(
            "INSERT INTO public.bet(title, description, invitation_code, access, state, created, expiration) VALUES(@Title, @Description, @InvitationCode, @BetAccess, @BetState, @Created, @Expiration)",
            bet);
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
            "UPDATE public.bet SET id=@Id, title = @Title, description = @Description, invitation_code = @InvitationCode, access=@BetAccess, state=@BetState, created=@Created, expiration=@Expiration WHERE bet.id = @Id",
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
        var result = _service.Delete<Bet>("DELETE FROM bet.bet WHERE bet.Id = @Id", new { id = key });
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
}