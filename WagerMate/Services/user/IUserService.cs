﻿using WagerMate.Data.user;

namespace WagerMate.Services.user;

public interface IUserService
{
    /// <summary>
    ///     Create a new User in the DB, given a User Object. The User ID is
    ///     created by the DB and a new User Object is returned.
    /// </summary>
    /// <param name="user"> User </param>
    /// <returns>new User Object</returns>
    public User CreateUser(User user);

    /// <summary>
    ///     Returns all Users of the DB
    /// </summary>
    /// <returns>List of Users</returns>
    public List<User> GetAllUsers();

    /// <summary>
    ///     Returns the information corresponding to the User ID
    /// </summary>
    /// <param name="key">Key of the User</param>
    /// <returns>User</returns>
    public User GetUserById(int key);

    /// <summary>
    ///     Returns updated user
    /// </summary>
    /// <param name="user">User</param>
    /// <returns></returns>
    bool UpdateUser(User user);

    /// <summary>
    ///     Deletes a user in the DB
    /// </summary>
    /// <param name="key">Key of the user</param>
    /// <returns></returns>
    bool DeleteUser(int key);

    public User GetUserByEmail(string email);
    public User GetUserByPassword(string password);
    public bool EmailIsRegistered(string email);
    public bool DoesUserPasswordExist(string password);
    public User? GetUserIfPasswordExists(string password);
}