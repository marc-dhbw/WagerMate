using WagerMate.Data;
using WagerMate.Services;
using WagerMate.Services.betting;
using WagerMate.Services.database;

namespace WagerMate.Service_Implementation.betting;

public class UserBetService : IUserBetService
{
    private IDbService _service;

    public UserBetService(IDbService service)
    {
        _service = service;
    }
    
    public bool CreateUserBet(UserBet userBet)
    {
        var result = _service.Create<UserBet>("INSERT INTO public.userbet(user_id, bet_id, case_id, amount) VALUES(@User_Id, @Bet_Id, @Case_Id, @Amount)", userBet);
        return result;
    }

    public bool DeleteUserBet(User user, Bet bet)
    {
        var userBet = new UserBet();
        userBet.User_Id = user.Id;
        userBet.Bet_Id = bet.Id;
        var result =
            _service.Delete<UserBet>("Delete from public.userbet where user_id = @User_Id and bet_id = @Bet_ID", userBet);
        return result;
    }

    public bool DeleteUserBet(UserBet userBet)
    {
        var result = _service.Delete<UserBet>("Delete from public.userbet where id =@Id", userBet);
        return result;
    }

    public UserBet GetUserBetById(int id)
    {
        var result = _service.GetById<UserBet>("SELECT * FROM public.userbet WHERE Id = @Id",new{id = id});
        Console.WriteLine("result");
        Console.WriteLine("id: " + result.Id);
        Console.WriteLine("userId: " + result.User_Id);
        Console.WriteLine("betid: " + result.Bet_Id);
        Console.WriteLine("amount: " + result.Amount);
        Console.WriteLine("caseid: " + result.Case_Id);
        return result;
    }

    public List<int> GetAllUserIdsFromBet(Bet bet)
    {
        var result = _service.GetAllWithParams<int>("SELECT * FROM public.userbet WHERE bet_id = @Id", bet);
        return result;
    }
}