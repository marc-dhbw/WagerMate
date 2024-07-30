using WagerMate.Data;

namespace WagerMate.Services;

public interface IUserService
{
    public bool CreateUser(User user);

    List<User> GetEmployeeList();

    User UpdateEmployee(User user);

    bool DeleteEmployee(int key);
}