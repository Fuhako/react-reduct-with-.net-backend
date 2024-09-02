using backend.AuthrModule.Model;
using backend.Helpers;
using backend.UserModule.Model;
using backend.UserModule.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace backend.AuthModule.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;
        private readonly AesEncryptionHelper _aesEncryptionHelper;
        public AuthController(IUserRepository userRepository, JwtService jwtService, AesEncryptionHelper aesEncryptionHelper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _aesEncryptionHelper = aesEncryptionHelper;
        }


        [HttpPost("Register")]
        public IActionResult RegisterUser(User user)
        {
            string message = "";
            try
            {
                string encry = _aesEncryptionHelper.Encrypt(user.password);
                user.password = encry;
                var result = _userRepository.CreateUser(user, "sysadmin");
                message = result;

            } 
            catch (Exception ex)
            {
                message = ex.Message;
                return BadRequest(message);
            }

            return Ok(message);
        }
        
        [HttpPost("Login")]
        public IActionResult LoginUser(Auth auth)
        {
            string message = "";
            var user = _userRepository.GetUserByEmail(auth.Email);

            if(user == null)
            {
                message = "Email has not register!";
                return BadRequest(message);
            }
            string decry = _aesEncryptionHelper.Decrypt(user.password);

            if (decry != auth.Password)
            {
                message = "Incorect Password!";
                return BadRequest(message);

            }
            var jwtString = _jwtService.Generate(Convert.ToInt32(user.id));

            Response.Cookies.Append(key: "jwt", value: jwtString, new CookieOptions
            {
                HttpOnly = true
            });
            user.last_login = DateTime.Now;

            _userRepository.UpdateUserById(user, user.user_id);
            message = "Login Success !";

            return Ok(new
            {
                message = message,
                data = new
                {
                    token = jwtString
                }
            });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                // Ambil nilai dari header Authorization
                var authorizationHeader = Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                {
                    return Unauthorized("Bearer token not found or invalid.");
                }

                // Pisahkan "Bearer " dari token
                var jwt = authorizationHeader.Substring("Bearer ".Length).Trim();
                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user = _userRepository.GetUserById(userId);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
