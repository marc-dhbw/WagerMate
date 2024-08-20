namespace WagerMate.Data;

public class Bet
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public string InvitationCode { get; set; }
    
    public DateTime Created { get; set; }
    
    public DateTime Expiration { get; set; }

    public Access BetAccess { get; set; }
    
    public State BetState { get; set; }
}
