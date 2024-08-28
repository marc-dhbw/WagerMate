namespace WagerMate.Data;

public class Case
{
    public Case()
    {
    }

    public Case(int betId, string? casetype)
    {
        Bet_Id = betId;
        Casetype = casetype;
    }

    public int Id { get; set; }
    public int Bet_Id { get; set; }
    public string? Casetype { get; set; }
}