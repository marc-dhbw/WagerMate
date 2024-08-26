using WagerMate.Data;

namespace WagerMate.Services.betting;

public interface IBetService
{
    /// <summary>
    ///     Create a new Bet in the DB, given a Bet Object. The Bet ID is
    ///     created by the DB and a new Bet Object is returned.
    /// </summary>
    /// <param name="bet"> Bet </param>
    /// <returns>new bet Object</returns>
    public Bet CreateBet(Bet bet);

    /// <summary>
    ///     Returns all Bets of the DB
    /// </summary>
    /// <returns>List of Bets</returns>
    public List<Bet> GetAllBets();

    /// <summary>
    ///     Returns the information corresponding to the Bet ID
    /// </summary>
    /// <param name="key">Key of the Bet</param>
    /// <returns>Bet</returns>
    public Bet GetBetById(int key);

    /// <summary>
    ///     Returns updated bet
    /// </summary>
    /// <param name="bet">Bet</param>
    /// <returns></returns>
    bool UpdateBet(Bet bet);

    /// <summary>
    ///     Deletes a bet
    /// </summary>
    /// <param name="key">Key of the bet</param>
    /// <returns></returns>
    bool DeleteBet(int key);

    public List<Bet> GetBetsByUserId(int userId);
}