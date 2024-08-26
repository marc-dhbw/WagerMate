namespace WagerMate.Data;

public class Case
{
    public int Id { get; set; }
    public int Bet_Id { get; set; }
    public string? Casetype { get; set; }

    public Case()
    { }

    public Case(int id, int betId, string? casetype)
    {
        Id = id;
        Bet_Id = betId;
        Casetype = casetype;
    }
}