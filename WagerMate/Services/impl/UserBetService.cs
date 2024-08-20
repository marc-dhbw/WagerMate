using WagerMate.Data;

namespace WagerMate.Services.impl;

public class UserBetService : IUserBetService
{
    private IDbService _service;

    public UserBetService(IDbService service)
    {
        _service = service;
    }
    
    public bool CreateUserBet(UserBet userBet)
    {
        var result = _service.Create<UserBet>("INSERT INTO public.userbet(user_id, bet_id, case_id, amount) VALUES(@UserId, @BetId, @CaseId, @Amount)", userBet);
        return result;
    }

    public bool DeleteUserBet(User user, Bet bet)
    {
        var userBet = new UserBet();
        userBet.UserId = user.Id;
        userBet.BetId = bet.Id;
        var result =
            _service.Delete<User>("Delete from public.userbet where user_id = @UserId and bet_id = @BetID", userBet);
        return result;
    }

    public bool DeleteUserBet(UserBet userBet)
    {
        var result = _service.Delete<User>("Delete from public.userbet where id =@Id", userBet);
        return result;
    }

    public UserBet GetUserBetById(int id)
    {
        var result = _service.GetById<UserBet>("SELECT * FROM public.userbet WHERE Id = @Id",new{id = id});
        return result;
    }

    public List<int> GetAllUserIdsFromBet(Bet bet)
    {
        var result = _service.GetById<List<int>>("SELECT * FROM public.userbet WHERE bet_id = @Id",bet);
        return result;
    }
}