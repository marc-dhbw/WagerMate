using WagerMate.Data;

namespace WagerMate.Services.impl;

public class UserService : IUserService
{
    private IDbService _service;

    public UserService(IDbService service)
    {
        _service = service;
    }

    public bool CreateUser(User user)
    {
        try
        {
            var result = _service.Create<User>("INSERT INTO public.users(id, name, email) VALUES(@Id, @Name,@Email)", user);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public List<User> GetEmployeeList()
    {
        throw new NotImplementedException();
    }

    public User UpdateEmployee(User user)
    {
        throw new NotImplementedException();
    }

    public bool DeleteEmployee(int key)
    {
        throw new NotImplementedException();
    }
}