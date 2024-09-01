using backend.UserModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.UserModule.Repository
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(long id);
        User GetUserByUserId(string userid);
        string CreateUser(User User, string userid);
        string UpdateUserById(User User, string user);
        string DeleteUserById(long id);
    }
}
