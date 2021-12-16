using Microsoft.AspNetCore.Mvc;
using BLL.Mappers.PhotoDAO;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Presentation.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private PhotoService photoService;
        private readonly AuthorizationHelper authHelper;

        public PhotoController(AuthorizationHelper _authHelper,PhotoService _photoService) {
            photoService = _photoService;
            this.authHelper = _authHelper;
        }

        // GET api/<PhotoController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            PhotoWithGallery photo = photoService.GetPhotoById(id);

            return Ok(photo);
        }

        [HttpPost]
        public ActionResult Post([FromBody] PhotoCreateDAO photoDAO)
        {
            PhotoWithGallery photo = photoService.CreateaPhoto(photoDAO,authHelper.GetJwtTokenUser());

            return Created($"/photo/{photo.Id}",photo);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            PhotoSingleDAO photo = photoService.Delete(id, authHelper.GetJwtTokenUser());
            return Ok(photo);
        }

        [HttpPatch]
        public ActionResult UpdatePhoto([FromBody] PhotoUpdateDAO photoDAO)
        {
            PhotoWithGallery photo = photoService.UpdatePhoto(photoDAO,authHelper.GetJwtTokenUser());
            return Ok(photo);
        }
    }
}
