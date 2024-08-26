namespace WagerMate.Data;

public class Bet
{
    public Bet()
    {
    }

    public Bet(int id, string? title, string? description, string? invitationCode, DateTime created,
        DateTime expiration,
        Access betAccess, State betState)
    {
        Id = id;
        Title = title;
        Description = description;
        InvitationCode = invitationCode;
        Created = created;
        Expiration = expiration;
        BetAccess = betAccess;
        BetState = betState;
    }

    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? InvitationCode { get; set; }
    public DateTime Created { get; set; }
    public DateTime Expiration { get; set; }
    public Access BetAccess { get; set; }
    public State BetState { get; set; }
}