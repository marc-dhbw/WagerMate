namespace WagerMate.Data;

public class UserBet
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BetId { get; set; }
    public int CaseId { get; set; } 
    public double Amount { get; set; }
}
