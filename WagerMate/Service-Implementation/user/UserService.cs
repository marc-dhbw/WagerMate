using WagerMate.Data;
using WagerMate.Services;
using WagerMate.Services.database;
using WagerMate.Services.user;

namespace WagerMate.Service_Implementation.user;

public class UserService : IUserService
{
    private readonly IDbService _service;

    public UserService(IDbService service)
    {
        _service = service;
    }

    public User CreateUser(User user)
    {
        _service.Create<User>("INSERT INTO public.user(name, email, password) VALUES(@Name, @Email, @Password)", user);
        return user;
    }

    public List<User> GetAllUsers()
    {
        var result = _service.GetAll<User>("SELECT * FROM public.user");
        return result;
    }

    public bool UpdateUser(User user)
    {
        var result = _service.Update<User>("UPDATE public.user SET Id=@Id, name = @Name, email=@Email, password=@Password WHERE Id = @Id", user);
        return result;
    }

    public User GetUserById(int key)
    {
        var result = _service.GetById<User>("SELECT * FROM public.user WHERE id = @Id",new{id = key});
        return result;
    }

    public bool DeleteUser(int key)
    {
        var result = _service.Delete<User>("DELETE FROM public.user WHERE id = @Id", new{id = key});
        return result;
    }

    public User GetUserByEmail(string email)
    {
        var result = _service.GetById<User>("SELECT * FROM public.user WHERE email = @Id",
            new { Id = email });
        return result;
    }

    public User GetUserByPassword(string password)
    {
        var result = _service.GetById<User>("SELECT * FROM public.user WHERE password = @Id",
            new { Id = password });
        return result;
    }

    public bool DoesUserPasswordExist(string password)
    {
        var result = _service.GetById<int>("SELECT COUNT(1) FROM public.user WHERE password = @Id", new { Id = password });
        return (result == 1);
    }

    public bool EmailIsRegistered(string email)
    {
        var result = _service.GetById<int>("SELECT COUNT(1) FROM public.user where email = @Id", new { Id = email });
        return (result == 1);
    }
}