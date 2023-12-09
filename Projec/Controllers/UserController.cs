using MandD;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Projec.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly MoviesandDirectorsContext _db;
        private readonly UserManager<MovieUser> _userManager;

        public UserController(MoviesandDirectorsContext db, UserManager<MovieUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> ImportUsersAsync()
        {
            (string name, string email) = ("user", "user@gmail.com");
            MovieUser user = new()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            if (await _userManager.FindByNameAsync(name) is not null)
            {
                user.UserName = "user1";
            }

            _ = await _userManager.CreateAsync(user, "P@ssw0rd!")
                                ?? throw new InvalidOperationException();

            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await _db.SaveChangesAsync();

            return Ok();
        }

    }
}