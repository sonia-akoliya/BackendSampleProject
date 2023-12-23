using loginbackend.Modal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace loginbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FirstPageController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Validate the login credentials (you can replace this with your own logic)
            if (loginModel.Username == "user" && loginModel.Password == "password")
            {
                // Authentication successful
                return Ok(new { Token = "yourAuthToken" });
            }
            else
            {
                // Authentication failed
                return Unauthorized();
            }
        }
    }
}
