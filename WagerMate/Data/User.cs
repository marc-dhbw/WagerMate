namespace WagerMate.Data;

public class User
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? Password { get; set; }

    public User()
    {
        Id = 0;
        Name = "";
        Email = "";
        Password = "";
    }

    public User(int id, string name, string email, string password)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
    }
    
}