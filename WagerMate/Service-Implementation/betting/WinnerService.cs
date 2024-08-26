using WagerMate.Data;
using WagerMate.Services.betting;
using WagerMate.Services.database;

namespace WagerMate.Service_Implementation.betting;

public class WinnerService : IWinnerService
{
    private readonly IDbService _service;

    public WinnerService(IDbService service)
    {
        _service = service;
    }

    public Winner CreateWinner(Winner createdWinner)
    {
        _service.Create<Case>("INSERT INTO public.winner(bet_id, userbet_id) VALUES(@Bet_Id, @UserBet_Id)",
            createdWinner);
        return createdWinner;
    }

    public Winner GetWinnerById(int winnerId)
    {
        var result =
            _service.GetById<Winner>("SELECT * FROM public.winner WHERE winner.Id = @Id", new { id = winnerId });
        return result;
    }

    public List<Winner> GetWinnersByBetId(int betId)
    {
        var result =
            _service.GetAllWithParams<Winner>("SELECT * FROM public.winner WHERE bet_id = @Id", new { Id = betId });
        return result;
    }

    public bool UpdateWinner(Winner newWinner)
    {
        var result =
            _service.Update<Winner>(
                "UPDATE public.winner SET Id = @Id, bet_id = @bet_Id, userbet_id = @UserBet_Id WHERE Id = @Id",
                newWinner);
        return result;
    }

    public bool DeleteWinner(int winnerId)
    {
        var result = _service.Delete<Winner>("DELETE FROM public.winner WHERE winner.Id = @Id", new { Id = winnerId });
        return result;
    }
}