using WagerMate.Data.bet;

namespace WagerMate.Services.betting;

public interface IBetSettlingService
{
    public bool CloseBet(UserBet userBet);
    public bool SettleBet(Bet bet, int[] caseIds);
}