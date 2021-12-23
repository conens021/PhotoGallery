using BLL.Mappers.User;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly UserService userService;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        public SignUpController(UserService _userService, JwtAuthenticationManager jwtAuthenticationManager)
        {
            userService = _userService;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost]
        public ActionResult<UserSingle> CreateUser([FromBody] UserCreateDAO userDAO)
        {
            UserAuthorize user = userService.CreateUser(userDAO);
            string jwt = 
                jwtAuthenticationManager.Authenticate(userService.GetByUsernameOrEmailAndPassword(userDAO.Username,userDAO.Password));

            return Ok(new UserSession { User = user,Jwt = jwt});

        }
    }
}
