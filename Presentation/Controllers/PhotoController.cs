using Microsoft.AspNetCore.Mvc;
using BLL.Mappers.PhotoDAO;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Presentation.Helpers;
using Microsoft.AspNetCore.Hosting;

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
        //to get wwwrot folder
        private readonly IHostEnvironment hostingEnvironment;

        public PhotoController(AuthorizationHelper _authHelper, PhotoService _photoService,
                                IHostEnvironment hostingEnvironment)
        {
            photoService = _photoService;
            this.authHelper = _authHelper;
            this.hostingEnvironment = hostingEnvironment;
        }

     
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            PhotoWithGallery photo = photoService.GetPhotoById(id);

            return Ok(photo);
        }

        [HttpPost]
        public ActionResult UploadPhoto([FromForm] MultipartRequest request)
        {
            string imagesFolder = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot", "Images");
            PhotoListWithGallery photoListWithGallery = photoService.CreateaPhoto(request, imagesFolder, authHelper.GetJwtTokenUser());

            return Ok(photoListWithGallery);

        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            string imagesFolder = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot", "Images");
            PhotoSingleDAO photo = photoService.Delete(id, imagesFolder, authHelper.GetJwtTokenUser());
            return Ok(photo);
        }

        [HttpPatch]
        public ActionResult UpdatePhoto([FromBody] PhotoUpdateDAO photoDAO)
        {
            PhotoWithGallery photo = photoService.UpdatePhoto(photoDAO, authHelper.GetJwtTokenUser());
            return Ok(photo);
        }
    }
}
