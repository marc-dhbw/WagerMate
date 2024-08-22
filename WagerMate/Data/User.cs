namespace WagerMate.Data;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public User()
    { }

    public User(int id, string email, string name, string password)
    {
        Id = id;
        Email = email;
        Name = name;
        Password = password;
    }
}