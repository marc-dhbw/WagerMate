using WagerMate.Data;
using WagerMate.Services.betting;

namespace WagerMate.Service_Implementation.betting;

public class BetHandlingService : IBetHandlingService
{
    public string GetStakeOfUserInBet<T>(User user, T bet)
    {
        throw new NotImplementedException();
    }

    public T[] FilterBetsOfUserByMoneyStake<T>(User user, T bets, bool moneyStake)
    {
        throw new NotImplementedException();
    }

    public T[] FilterBetsByDate<T>(T bets, string date, DateFilter dateFilter)
    {
        throw new NotImplementedException();
    }
}