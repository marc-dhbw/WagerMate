using WagerMate.Data;
using WagerMate.Services.betting;
using WagerMate.Services.database;

namespace WagerMate.Service_Implementation.betting;

public class UserBetService : IUserBetService
{
    private readonly IDbService _service;

    public UserBetService(IDbService service)
    {
        _service = service;
    }

    public int CreateUserBet(UserBet userBet)
    {
        var result = _service.CreateWithReturn(
            "INSERT INTO public.userbet(user_id, bet_id, case_id, amount) VALUES(@User_Id, @Bet_Id, @Case_Id, @Amount) RETURNING id",
            userBet);
        return result;
    }

    public bool DeleteUserBet(User user, Bet bet)
    {
        var userBet = new UserBet();
        userBet.User_Id = user.Id;
        userBet.Bet_Id = bet.Id;
        var result =
            _service.Delete<UserBet>("Delete from public.userbet where user_id = @User_Id and bet_id = @Bet_ID",
                userBet);
        return result;
    }

    public bool DeleteUserBet(UserBet userBet)
    {
        var result = _service.Delete<UserBet>("Delete from public.userbet where id =@Id", userBet);
        return result;
    }

    public UserBet GetUserBetById(int id)
    {
        var result = _service.GetById<UserBet>("SELECT * FROM public.userbet WHERE Id = @Id", new { id });
        return result;
    }

    public UserBet GetUserBetById(int userId, int betId)
    {
        var userbet =
            _service.GetById<UserBet>("SELECT * FROM public.userbet WHERE user_id = @userId AND bet_id = @betId", new { userId, betId });
        return userbet;
    }

    public List<int> GetAllUserIdsFromBet(Bet bet)
    {
        var result = _service.GetAllWithParams<int>("SELECT * FROM public.userbet WHERE bet_id = @Id", bet);
        return result;
    }
}