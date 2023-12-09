using MandD;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projec.Dtos;
using System.Text.RegularExpressions;

namespace Projec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtHandler _jwtHandler;
        private readonly Regex passwordCheck = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+=])[A-Za-z\\d!@#$%^&*()_+=]{8,}$");
        private readonly MoviesandDirectorsContext _db;
        private readonly PasswordHasherWrapper _passwordHasherWrapper;

        public UserController(UserManager<User> userManager, JwtHandler jwtHandler, IPasswordHasher<User> passwordHasher,
         MoviesandDirectorsContext db, PasswordHasherWrapper passwordHasherWrapper)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _passwordHasherWrapper = passwordHasherWrapper;
            
            _db = db;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.Username);

            if (user == null || _passwordHasherWrapper.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password) != PasswordVerificationResult.Success)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok("Login successful");
        }
    }
}