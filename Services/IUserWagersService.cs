using WagerMate.Data;

namespace WagerMate.Services;

public interface IUserWagersService
{
    public bool AddUserWager(UserWager userWager);
    public bool DeleteUserWager(User user, Bet bet);
    public bool DeleteUserWager(UserWager userWager);
    public UserWager GetUserWagerById(int id);
    public List<int> GetAllUserIdsFromBet(Bet bet);
    
}