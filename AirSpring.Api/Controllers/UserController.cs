using AirSpring.DAL.DataContext;
using AirSpring.DAL.Interface;
using AirSpring.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AirSpring.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        // Validate user credentias
        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginDTO data)
        {
            try
            {
                var user = new User()
                {
                    Username = data.Username.ToLower(),
                    Password = data.Password
                };
                var result = _user.LoginUser(user);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
