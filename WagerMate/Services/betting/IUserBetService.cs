using WagerMate.Data.bet;
using WagerMate.Data.user;

namespace WagerMate.Services.betting;

public interface IUserBetService
{
    public int CreateUserBet(UserBet userBet);
    public bool DeleteUserBet(User user, Bet bet);
    public bool DeleteUserBet(UserBet userBet);
    public UserBet GetUserBetById(int id);
    public UserBet GetUserBetById(int userId, int betId);
    public List<UserBet> GetAllUserBetsFromBet(Bet bet);

}