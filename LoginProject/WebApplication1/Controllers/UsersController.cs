using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel request)
        {
            var createdUser = UserModel.AddUser(request.Username, request.Email, request.Password);

            if (createdUser == null)
                return Conflict(new { message = "User already exists" });

            return Ok(new { message = "Register successful" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var existingUser = UserModel.ValidateUser(request.Username, request.Password);

            if (existingUser == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(new { message = "Login successful" });
        }
    }
}
