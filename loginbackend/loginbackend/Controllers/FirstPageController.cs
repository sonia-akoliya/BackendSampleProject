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

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserInfoModel loginModel)
        {
            var user = _context.UserInfo.FirstOrDefault(u => u.UserName == loginModel.UserName && u.Password == loginModel.Password);

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
    }
}
