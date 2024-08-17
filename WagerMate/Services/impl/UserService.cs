using WagerMate.Data;

namespace WagerMate.Services.impl;

public class UserService : IUserService
{
    private readonly IDbService _service;

    public UserService(IDbService service)
    {
        _service = service;
    }

    public User CreateUser(User user)
    {
        _service.Create<User>("INSERT INTO public.users(name, email, password) VALUES(@Name, @Email, @Password)", user);
        return user;
    }

    public List<User> GetAllUsers()
    {
        var result = _service.GetAll<User>("SELECT * FROM public.users");
        return result;
    }

    public bool UpdateUser(User user)
    {
        var result =
            _service.Update<User>(
                "UPDATE public.users SET Id=@Id, name = @Name, email=@Email, password=@Password WHERE users.Id = @Id",
                user);
        return result;
    }

    public User GetUserById(int key)
    {
        var result = _service.GetById<User>("SELECT * FROM public.users WHERE users.Id = @Id", new { id = key });
        return result;
    }

    public bool DeleteUser(int key)
    {
        var result = _service.Delete<User>("DELETE FROM public.users WHERE users.Id = @Id", new { id = key });
        return result;
    }

    public User GetUserByEmail(string email)
    {
        var result = _service.GetById<User>("SELECT * FROM public.users WHERE users.email = @Id",
            new { Id = email });
        return result;
    }

    public User GetUserByPassword(string password)
    {
        var result = _service.GetById<User>("SELECT * FROM public.users WHERE users.password = @Id",
            new { Id = password });
        Console.WriteLine("result name", result.Name);
        Console.WriteLine("id: ", result.Id.ToString());
        return result;
    }
}