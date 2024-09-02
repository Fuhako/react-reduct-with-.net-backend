using backend.Context;
using backend.UserModule.Model;
using Microsoft.AspNetCore.Mvc;

namespace backend.UserModule.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context ) 
        {
            _context = context;   
        }

        public List<User> GetUsers()
        {
            return _context.User.OrderBy(p => p.id).ToList();
        }
        
        public User GetUserById(long id)
        {
            return _context.User.Where(a => a.id == id).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            return _context.User.Where(a => a.email == email).FirstOrDefault();
        }

        public string CreateUser(User User, string userid)
        {
            try
            {

                // Fetch the existing User from the database
                var existingUserbyUserId = GetUserByUserId(User.user_id);
                if (existingUserbyUserId != null)
                {
                    // User with the given ID does not exist
                    return "User id is exists!";
                }


                User.active = true;
                User.created_date = DateTime.Now;
                User.created_user = userid;

                _context.User.Add(User);
                _context.SaveChanges(); 

                return "Insert User Success!";
            }
            catch (Exception ex)
            {
                return $"Insert User Failed with Error :{ex.InnerException}"; // Failure
            }
        }

        public string UpdateUserById(User User, string userid)
        {
            try
            {
                // Fetch the existing User from the database
                var existingUserById = GetUserById(User.id);
                var existingUserbyUserId = GetUserByUserId(User.user_id);
                if (existingUserbyUserId != null && existingUserById.id != existingUserbyUserId.id)
                {
                    // User with the given ID does not exist
                    return "User id is exists!";
                }

                // Update properties
                existingUserById.user_id = User.user_id;
                existingUserById.password = User.password;
                existingUserById.email = User.email;
                existingUserById.is_lock = User.is_lock;
                existingUserById.is_use = User.is_use;
                existingUserById.active = User.active;
                existingUserById.updated_user = userid;
                existingUserById.updated_date = DateTime.Now;

                // Mark the entity as modified
                _context.User.Update(existingUserById);
                _context.SaveChanges();
                return "Update User Success!";
            }
            catch (Exception ex)
            {
                return $"Update User Failed with Error :{ex.InnerException}"; // Failure
            }

        }

        public User GetUserByUserId(string userid)
        {
            return _context.User.Where(a => a.user_id == userid).FirstOrDefault();
        }

        public string DeleteUserById(long id)
        {
            try
            {

                // Fetch the existing User from the database
                var existingUser = GetUserById(id);
                if (existingUser == null)
                {
                    // User with the given ID does not exist
                    return "User id is not exists!";
                }

                // Mark the entity for deletion
                _context.User.Remove(existingUser);
                _context.SaveChanges(); // Persist changes to the database
                return "Delete User Success!";
            }
            catch (Exception ex)
            {
                return $"Delete User Failed with Error :{ex.InnerException}"; // Failure
            }

        }
    }
}
