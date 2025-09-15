using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/users/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("All fields are required.");
            }

            var success = _userService.Register(user);
            if (!success)
            {
                return Conflict("Username already exists.");
            }

            return Ok("User registered successfully.");
        }

        // POST: api/users/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginData)
        {
            if (string.IsNullOrWhiteSpace(loginData.Username) ||
                string.IsNullOrWhiteSpace(loginData.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var user = _userService.Login(loginData.Username, loginData.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new
            {
                Message = "Login successful.",
                User = new { user.Id, user.Username, user.Email }
            });
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}
