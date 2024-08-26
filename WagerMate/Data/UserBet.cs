namespace WagerMate.Data;

public class UserBet
{
    public int Id { get; set; }
    public int User_Id { get; set; }
    public int Bet_Id { get; set; }
    public int Case_Id { get; set; }
    public double Amount { get; set; }

    public UserBet()
    { }

    public UserBet(int id, int userId, int betId, int caseId, double amount)
    {
        Id = id;
        User_Id = userId;
        Bet_Id = betId;
        Case_Id = caseId;
        Amount = amount;
    }
}