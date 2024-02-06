using loginbackend.DataContext;
using loginbackend.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace loginbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstPageController : ControllerBase
    {
        private readonly LoginContext _context;

        public FirstPageController(LoginContext context)
        {
            _context = context;
        }

        [HttpGet("login")]
        public IActionResult Login(string userName , string password)
        {
            var user = _context.UserInfo.FirstOrDefault(u => u.UserName == userName && u.Password == password);

            if (user != null)
            {
                return Ok(new { Token = "yourAuthToken", UserInfo = user });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserInfoModel user)
        {
            if (ModelState.IsValid)
            {
                _context.UserInfo.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserInfoModel updatedUser)
        {
            var existingUser = await _context.UserInfo.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.UserName = updatedUser.UserName;
            existingUser.Password = updatedUser.Password;

            _context.UserInfo.Update(existingUser);
            await _context.SaveChangesAsync();

            return Ok(existingUser);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userToDelete = await _context.UserInfo.FindAsync(id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            _context.UserInfo.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User deleted successfully" });
        }


    }
}
