namespace WagerMate.Data;

public class UserBet
{
    public int Id { get; set; }
    public int User_Id { get; set; }
    public int Bet_Id { get; set; }
    public int Case_Id { get; set; } 
    public double Amount { get; set; }
}
