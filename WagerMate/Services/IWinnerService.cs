using WagerMate.Data;

namespace WagerMate.Services;

public interface IWinnerService
{
    public Winner CreateWinner(Winner createdWinner);
    public Winner GetWinnerById(int winnerId);
    public List<Winner> GetWinnerByBetId(int betId);
    public bool UpdateWinner(Winner newWinner);
    public bool DeleteWinner(int winnerId);
}