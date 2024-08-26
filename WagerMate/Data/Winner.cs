namespace WagerMate.Data;

public class Winner
{
    public Winner()
    {
    }

    public Winner(int id, int betId, int userBetId)
    {
        Id = id;
        Bet_Id = betId;
        UserBet_Id = userBetId;
    }

    public int Id { get; set; }
    public int Bet_Id { get; set; }
    public int UserBet_Id { get; set; }
}