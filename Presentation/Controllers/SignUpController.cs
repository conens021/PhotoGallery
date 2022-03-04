using BLL.Mappers.User;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtAuthenticationManager _jwtAuthenticationManager;
        public SignUpController(UserService userService, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _userService = userService;
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost]
        public ActionResult<UserSingle> CreateUser([FromBody] UserCreateDAO userDAO)
        {
            UserAuthorize user = _userService.CreateUser(userDAO);
            string jwt =
                _jwtAuthenticationManager.Authenticate(_userService.GetByUsernameOrEmailAndPassword(userDAO.Username,userDAO.Password));

            return Created("/",new UserSession() { User = user, Jwt = _jwtAuthenticationManager.Authenticate(user) });

        }
    }
}
