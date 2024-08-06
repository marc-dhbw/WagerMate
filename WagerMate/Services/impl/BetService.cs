namespace WagerMate.Services.impl;
using WagerMate.Data;


public class BetService: IBetService
{
    private readonly IDbService _service;

    public BetService(IDbService service)
    {
        _service = service;
    }
    public Bet CreateBet(Bet bet)
    {
        _service.Create<Bet>("INSERT INTO public.wagers(wageritemid, description, created, expiration, cases, access, state) VALUES(@Wageritemid, @Description, @Created, @Expiration, @Cases, @Access, @State)", bet);
        return bet;
    }
    
    public Bet CreateUser(Bet bet)
    {
        _service.Create<User>("INSERT INTO public.wagers(wageritemid, description, created, expiration, cases, access, state) VALUES(@Wageritemid, @Description, @Created, @Expiration, @Cases, @Access, @State)", bet);
        return bet;
    }

    public List<Bet> GetAllBets()
    {
        var result = _service.GetAll<Bet>("SELECT * FROM public.wagers");
        return result;
    }

    public bool UpdateBet(Bet bet)
    {
        var result = _service.Update<Bet>("UPDATE public.wagers SET Id=@Id, description = @Description, created=@Created, expiration=@Expiration, cases = @Cases, access=@Access, state=@State WHERE wagers.Id = @Id", bet);
        return result;
    }

    public Bet GetBetById(int key)
    {
        var result = _service.GetById<Bet>("SELECT * FROM public.wagers WHERE wagers.Id = @Id",new{id = key});
        return result;
    }

    public bool DeleteBet(int key)
    {
        var result = _service.Delete<Bet>("DELETE FROM wagers.wagers WHERE wagers.Id = @Id", new{id = key});
        return result;
    
    }
}