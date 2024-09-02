using backend.Context;
using backend.UserModule.Model;
using backend.UserModule.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.UserModule.Controller
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        public UserController(IUserRepository UserRepository, ApplicationDbContext context) 
        {
            _UserRepository = UserRepository;
        }

        [Route("api/[controller]/GetUser")]
        [HttpGet]
        public IActionResult GetUser()
        {
            var result = _UserRepository.GetUsers();
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Route("api/[controller]/GetUserById")]
        [HttpGet]
        public IActionResult GetUserById(long id)
        {
            var result = _UserRepository.GetUserById(id);
            if(result == null)
            {
                return NotFound("User not found!");
            }

            return Ok(result);
        }
        
        [Route("api/[controller]/GetUserEmail")]
        [HttpGet]
        public IActionResult GetUserByEmail(string email)
        {
            var result = _UserRepository.GetUserByEmail(email);
            if(result == null)
            {
                return NotFound("User not found!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/GetUserByUserId")]
        [HttpGet]
        public IActionResult GetUserByUserId(string userid)
        {
            var result = _UserRepository.GetUserByUserId(userid);
            if (result == null)
            {
                return NotFound("User not found!");
            }

            return Ok(result);
        }


        [Route("api/[controller]/CreateUser")]
        [HttpPost]
        public IActionResult CreateUser(User User, string userid)
        {
            if (User == null)
            {
                return BadRequest("User cannot be null");
            }

            var result = _UserRepository.CreateUser(User, userid);
            if (result == null)
            {
                return NotFound("Create User Failed!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/UpdateUserById")]
        [HttpPut]
        public IActionResult UpdateUserById(User User, string userid)
        {
            if (User == null)
            {
                return BadRequest("User cannot be null");
            }

            var result = _UserRepository.UpdateUserById(User, userid);
            if (result == null)
            {
                return NotFound("Update User Failed!");
            }

            return Ok(result);
        }

        [Route("api/[controller]/DeleteUserById")]
        [HttpDelete]
        public IActionResult DeleteUserById(long id)
        {

            var result = _UserRepository.DeleteUserById( id);
            if (result == null)
            {
                return NotFound("Delete User Failed!");
            }

            return Ok(result);
        }
    }
}
