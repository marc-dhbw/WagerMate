namespace WagerMate.Data;

public class Bet
{
    public int Id { get; set; }
    
    public int WageritemId { get; set; }
    
    public string Description { get; set; }
    
    public DateTime Created { get; set; }
    
    public DateTime Expiration { get; set; }

    public string[] Cases { get; set; }

    public Access BetAccess { get; set; }
    public State BetState { get; set; }
}
