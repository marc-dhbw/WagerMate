using WagerMate.Data.bet;
using WagerMate.Data.enums;
using WagerMate.Data.user;

namespace WagerMate.Services.betting;

public interface IBetHandlingService
{
    // TODO adjust the type of Bets in the functions

    /// <summary>
    ///     Returns the stake that the user has in the bet
    /// </summary>
    /// <param name="user">User Object</param>
    /// <param name="bet">Bet Object</param>
    /// <returns>String representation of the stake</returns>
    public string GetStakeOfUserInBet<T>(User user, T bet);

    /// <summary>
    ///     Filters if bets of a user with or without money should be returned
    /// </summary>
    /// <param name="user">User Object</param>
    /// <param name="bets">Array Of Bets</param>
    /// <param name="moneyStake">Bool if money is bet</param>
    /// <returns>Array of Bets</returns>
    public T[] FilterBetsOfUserByMoneyStake<T>(User user, T bets, bool moneyStake);

    /// <summary>
    ///     Filter bets weather they are set before, after or at the given date
    /// </summary>
    /// <param name="bets">Array of Bets</param>
    /// <param name="date">Date in form of string formatted like the date of the DB</param>
    /// <param name="dateFilter">Filter specifying based on what is being filtered</param>
    /// <returns>Array of Bets</returns>
    public T[] FilterBetsByDate<T>(T bets, string date, DateFilter dateFilter);
}