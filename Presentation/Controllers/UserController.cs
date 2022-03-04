using Microsoft.AspNetCore.Mvc;
using BLL.Mappers.User;
using DAL.Entities;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using BLL.Mappers.PhotoDAO;

namespace Presentation.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        private readonly PhotoService _photoService;

        public UsersController(UserService userService, PhotoService photoService)
        {
            _userService = userService;
            _photoService = photoService;
        }

        [HttpGet]
        public ActionResult GetAllUSers()
        {
            return Ok(_userService.GetAll());
        }

        //Single user without galleries
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetAllGaleries(int id)
        {
            UserSingle user = _userService.GetById(id);
            return Ok(user);
        }

        //SIngle user with galleries
        [HttpGet("{id}/galleries")]
        public ActionResult GetUserGalleries(int id)
        {
            UserGalleriesWithCovers userGalleries = _userService.GetUserGalleries(id);
            return Ok(userGalleries);
        }

        //Get recently added photos by user
        [HttpGet("{id}/photos")]
        public ActionResult GetUserRecentlyPhotos(int id)
        {

            IEnumerable<PhotoWithGallery> photosWithGallery = _photoService.GetUserRecentPhotos(id);

            return Ok(photosWithGallery);
        }

        [HttpPatch]
        public ActionResult UpdateUser([FromBody] UserUpdateDAO userDAO)
        {
            UserSingle user = _userService.UpdateUser(userDAO);
            return Ok(user);
        }
    }
}