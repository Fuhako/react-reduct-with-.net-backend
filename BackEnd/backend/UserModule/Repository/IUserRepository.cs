using backend.UserModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.UserModule.Repository
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(long id);
        User GetUserByUserId(string userid);
        User GetUserByEmail(string email);
        string CreateUser(User User, string userid);
        string UpdateUserById(User User, string user);
        string DeleteUserById(long id);
    }
}
