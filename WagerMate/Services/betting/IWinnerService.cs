using WagerMate.Data.bet;

namespace WagerMate.Services.betting;

public interface IWinnerService
{
    public Winner CreateWinner(Winner createdWinner);
    public Winner GetWinnerById(int winnerId);
    public List<Winner> GetWinnersByBetId(int betId);
    public bool UpdateWinner(Winner newWinner);
    public bool DeleteWinner(int winnerId);
    public Winner? GetWinnerByUserBetIdIfExists(int userBetId);
}