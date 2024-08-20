using WagerMate.Data;

namespace WagerMate.Services;

public interface IUserBetService
{
    public bool CreateUserBet(UserBet userBet);
    public bool DeleteUserBet(User user, Bet bet);
    public bool DeleteUserBet(UserBet userBet);
    public UserBet GetUserBetById(int id);
    public List<int> GetAllUserIdsFromBet(Bet bet);
    
}