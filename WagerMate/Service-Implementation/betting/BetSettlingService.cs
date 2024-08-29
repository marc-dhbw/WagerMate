using WagerMate.Data;
using WagerMate.Service_Implementation.user;
using WagerMate.Services.betting;

namespace WagerMate.Service_Implementation.betting;

public class BetSettlingService : IBetSettlingService
{
    private IBetService _betService;
    private IUserBetService _userBetService;
    private IWinnerService _winnerService;

    public BetSettlingService(IBetService betService, IUserBetService userBetService, IWinnerService winnerService)
    {
        _betService = betService;
        _userBetService = userBetService;
        _winnerService = winnerService;
    }
    
    public bool CloseBet(UserBet userBet)
    {
        try
        {
            Bet activeBet = _betService.GetBetById(userBet.Bet_Id);
            if (activeBet.State == State.Closed) return false;
            activeBet.State = State.Closed;
            _betService.UpdateBet(activeBet);
            return true;
        }
        catch (Exception)
        {
            Console.WriteLine("BetSettlingService: closeBet failed");
        }
        return false;
    }

    public bool SettleBet(Bet bet, int[] caseIds)
    {
        if (bet.State == State.Closed) return false;
        try
        {
            double totalAmount = 0;
            List<UserBet> userBets = _userBetService.GetAllUserBetsFromBet(bet);
            List<Winner> winners = new List<Winner>();
            
            foreach (var userBet in userBets)
            {
                if (caseIds.Contains(userBet.Case_Id))
                {
                    Winner newWinner = new Winner();
                    newWinner.Bet_Id = bet.Id;
                    newWinner.UserBet_Id = userBet.Id;
                    newWinner.Amount = userBet.Amount;
                    winners.Add(newWinner);
                }
                totalAmount += userBet.Amount;
            }
            
            double totalAmountWinner = winners.Sum(winner => winner.Amount);
            // Calculation of the winning amount in relation to the money bet
            winners.ForEach(winner => winner.Amount = totalAmount *(winner.Amount/totalAmountWinner));
            
            // Create all winners in the winner db
            winners.ForEach(winner => _winnerService.CreateWinner(winner));
            
            bet.State = State.Closed;
            _betService.UpdateBet(bet);

            return true;
        }
        
        catch (Exception)
        {
            Console.WriteLine("BetSettlingService: settleBet failed");
        }
        return false;
    }
    
}