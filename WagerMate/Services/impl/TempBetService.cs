using WagerMate.Data;

namespace WagerMate.Services.impl;

public class TempBetService : IBetService {
    public Bet CreateBet(Bet bet) {
        bet.Id = 1;
        return bet;
    }

    public List<Bet> GetAllBets() {
        throw new NotImplementedException();
    }

    public Bet GetBetById(int key) {
        throw new NotImplementedException();
    }

    public bool UpdateBet(Bet bet) {
        throw new NotImplementedException();
    }

    public bool DeleteBet(int key) {
        throw new NotImplementedException();
    }

    public List<Bet> GetBetsByUserId(int userId) {
        throw new NotImplementedException();
    }
}