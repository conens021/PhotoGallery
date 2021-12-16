using Microsoft.AspNetCore.Mvc;
using BLL.Mappers.User;
using DAL.Entities;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("/users")]
        public ActionResult GetAllUSers() {

            return Ok(userService.GetAll());
        }

        //Single user without galleries
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetAllGaleries(int id)
        {
            UserSingle user = userService.GetById(id);
            return Ok(user);
        }

        //SIngle user with galleries
        [HttpGet("{UserId}/galleries")]
        public ActionResult GetUserGalleries( int UserId)
        {
            UserGalleriesWithCovers userGalleries = userService.GetUserGalleries(UserId);
            return Ok(userGalleries);
        }

        [HttpPost]
        [Route("")]
        public ActionResult<User> CreateUser([FromBody] UserCreateDAO userDAO) {
            UserSingle user = userService.CreateUser(userDAO);
            return Created($"/user/{user.Id}",user);

        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<User> RemoveUser(int id)
        {
            UserSingle user = userService.Delete(id);

            return Ok(user);
        }

        [HttpPatch("")]
        public ActionResult UpdateUser([FromBody] UserUpdateDAO userDAO) {

            UserSingle user = userService.UpdateUser(userDAO);

            return Ok(user);
        }

    }
}