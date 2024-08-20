using WagerMate.Data;

namespace WagerMate.Services.impl;

public class WinnerService : IWinnerService
{
    private IDbService _service;

    public WinnerService(IDbService service)
    {
        _service = service;
    }

    public Winner CreateWinner(Winner createdWinner)
    {
        _service.Create<Case>("INSERT INTO public.winner(bet_id, userbet_id) VALUES(@BetId, @UserBetId)", createdWinner);
        return createdWinner;
    }

    public Winner GetWinnerById(int winnerId)
    {
        var result = _service.GetById<Winner>("SELECT * FROM public.winner WHERE winner.Id = @Id", new { id = winnerId });
        return result;
    }

    public List<Winner> GetWinnerByBetId(int betId)
    {
        var result = _service.GetAllWithParams<Winner>("SELECT * FROM public.winner WHERE bet_id = @Id",new{Id = betId});
        return result;
    }

    public bool UpdateWinner(Winner newWinner)
    {
        var result =
            _service.Update<Winner>(
                "UPDATE public.winner SET Id=@Id, bet_id=@betId, userbet_id = @UserBetId WHERE winner.Id = @Id", newWinner);
        return result;
    }

    public bool DeleteWinner(int winnerId)
    {
        var result  = _service.Delete<Winner>("DELETE FROM public.winner WHERE winner.Id = @Id", new { Id = winnerId });
        return result;
    }
}