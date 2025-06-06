using LoginSignupApi.Models;
using LoginSignupApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LoginSignupApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>(); // 👈 Fix: initialize PasswordHasher
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] User newUser)
        {
            if (await _userRepository.UserExistsAsync(newUser.Username, newUser.Email))
                return BadRequest("Username or Email already exists.");

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, newUser.PasswordHash); // 👈 Hashing

            newUser.CreatedAt = DateTime.Now;
            newUser.IsActive = true;

            await _userRepository.AddUserAsync(newUser);
            await _userRepository.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetUserByUsernameOrEmailAsync(loginRequest.UsernameOrEmail);
            if (user == null || !user.IsActive)
            {
                return Unauthorized("Invalid username/email or password.");
            }

            // 👇 Verify password using PasswordHasher
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid username/email or password.");
            }

            return Ok(new { message = "User logged in successfully", userId = user.UserId, username = user.Username });
        }
    }
}
