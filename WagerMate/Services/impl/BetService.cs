namespace WagerMate.Services.impl;
using WagerMate.Data;


public class BetService: IBetService
{
    private IDbService _service;

    public BetService(IDbService service)
    {
        _service = service;
    }
    public Bet CreateBet(Bet bet)
    {
        _service.Create<Bet>("INSERT INTO public.wagers(wageritem_id, description, created, expiration, cases, access, state) VALUES(@Wageritem_id, @Description, @Created, @Expiration, @Cases, @Access, @State)", bet);
        return bet;
    }

    public List<Bet> GetAllBets()
    {
        var result = _service.GetAll<Bet>("SELECT * FROM public.wagers");
        return result;
    }

    public bool UpdateBet(Bet bet)
    {
        var result = _service.Update<Bet>("UPDATE public.wagers SET Id=@Id, wageritem_id = @Wageritem_id, description = @Description, created=@Created, expiration=@Expiration, cases = @Cases, access=@Access, state=@State WHERE wagers.Id = @Id", bet);
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