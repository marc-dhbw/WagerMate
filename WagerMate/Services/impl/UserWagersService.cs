using WagerMate.Data;

namespace WagerMate.Services.impl;

public class UserWagersService : IUserWagersService
{
    private IDbService _service;

    public UserWagersService(IDbService service)
    {
        _service = service;
    }
    
    public bool AddUserWager(UserWager userWager)
    {
        var result = _service.Create<UserWager>("INSERT INTO public.userwagers(user_id, wager_id) VALUES(@UserId, @BetId)", userWager);
        return result;
    }

    public bool DeleteUserWager(User user, Bet bet)
    {
        var userWager = new UserWager();
        userWager.UserId = user.Id;
        userWager.BetId = bet.Id;
        var result =
            _service.Delete<User>("Delete from public.userwagers where user_id = @UserId and wager_id = @BetID", userWager);
        return result;
    }

    public bool DeleteUserWager2(UserWager userWager)
    {
        var result = _service.Delete<User>("Delete from public.userwagers where id =@Id", userWager);
        return result;
    }

    public UserWager GetUserWagerById(int id)
    {
        var result = _service.GetById<UserWager>("SELECT * FROM public.userwagers WHERE Id = @Id",new{id = id});
        return result;
    }

    public List<int> GetAllUserIdsFromBet(Bet bet)
    {
        var result = _service.GetById<List<int>>("SELECT * FROM public.userwagers WHERE wager_id = @Id",bet);
        return result;
    }
}