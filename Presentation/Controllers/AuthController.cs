using BLL.Mappers.User;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService userService;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        public AuthController(UserService userService, JwtAuthenticationManager jwtAuthenticationManager) {
            this.userService = userService;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [AllowAnonymous]
        [HttpPost("/auth")]
        public ActionResult UserAuthenticate([FromBody] UserAuthenticate user) {
            UserAuthorize userFromDb = userService.GetByUsernameOrEmailAndPassword(user.UsernameOrEmail, user.Password);
            return Ok(new UserSession() { User = userFromDb, Jwt = jwtAuthenticationManager.Authenticate(userFromDb) });
        }
    }
}
