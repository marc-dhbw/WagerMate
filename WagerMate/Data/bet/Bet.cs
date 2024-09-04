using WagerMate.Data.enums;

namespace WagerMate.Data.bet;

public class Bet
{
    public Bet()
    {
    }

    public Bet(int id, string? title, string? description, string? invitationCode, DateTime created,
        DateTime expiration,
        Access access, State state)
    {
        Id = id;
        Title = title;
        Description = description;
        Invitation_Code = invitationCode;
        Created = created;
        Expiration = expiration;
        Access = access;
        State = state;
    }

    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Invitation_Code { get; set; }
    public DateTime Created { get; set; }
    public DateTime Expiration { get; set; }
    public Access Access { get; set; }
    public State State { get; set; }
}