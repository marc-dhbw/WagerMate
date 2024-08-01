using WagerMate.Data;

namespace WagerMate.Services.impl;

public class UserService : IUserService
{
    private IDbService _service;

    public UserService(IDbService service)
    {
        _service = service;
    }

    public User CreateUser(User user)
    {
        try
        {
            _service.Create<User>("INSERT INTO public.users(name, email, password) VALUES(@Name, @Email, @Password)", user);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return user;
        }
    }

    public List<User> GetAllUsers()
    {
        try
        {
            var result = _service.GetAll<User>("SELECT * FROM public.users");
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine("UserServie: GetAllUsers failed");
            Console.WriteLine(e);
            throw;
        }
    }

    public bool UpdateUser(User user)
    {
        try
        {
            var result = _service.Update<User>("UPDATE public.users SET name = @Name, email=@Email, password=@Passoword WHERE users.Id = @Id", user);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public User GetUserByID(int key)
    {
        try
        {
            var result = _service.GetByKey<User>("SELECT * FROM public.users WHERE users.Id = @Id",new{id = key});
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine("UserService: GetUserByID failed");
            Console.WriteLine(e);
            throw;
        }
    }

    public bool DeleteUser(int key)
    {
        try
        {
            var result = _service.Delete<User>("DELETE FROM public.users WHERE users.id = @Id", key);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine("UserService: DeleteUser failed");
            Console.WriteLine(e);
            throw;
        }
    }
}