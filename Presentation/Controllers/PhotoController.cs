using Microsoft.AspNetCore.Mvc;
using BLL.Mappers.PhotoDAO;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Presentation.Helpers;
using Microsoft.AspNetCore.Hosting;


namespace Presentation.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private PhotoService _photoService;
        private readonly AuthorizationHelper _authHelper;
        private readonly PathRegistry _pathRegistry;

        public PhotosController(AuthorizationHelper authHelper, PhotoService photoService, PathRegistry pathRegistry)
        {
            _photoService = photoService;
            _authHelper = authHelper;
            _pathRegistry = pathRegistry;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            PhotoWithGallery photo = _photoService.GetPhotoById(id);

            return Ok(photo);
        }

        [HttpPost]
        public ActionResult UploadPhoto([FromForm] MultipartRequest request)
        {
            PhotoListWithGallery photoListWithGallery = 
                _photoService.CreateaPhoto(request, _pathRegistry.GetUploadFileFolder(), _authHelper.GetJwtTokenUser());


            return Ok(photoListWithGallery);

        }

        [HttpPost("base64")]
        public ActionResult UploadPhoto([FromBody] PhotoUploadBase64 photoUpload)
        {

            PhotoListWithGallery photoListWithGallery =
                _photoService.CreateaPhotoBase64(photoUpload, _pathRegistry.GetUploadFileFolder(), _authHelper.GetJwtTokenUser());

            return Ok(photoListWithGallery);

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            PhotoSingleDAO photo = _photoService.Delete(id, _pathRegistry.GetUploadFileFolder(), _authHelper.GetJwtTokenUser());
            return Ok(photo);
        }

        [HttpPatch]
        public ActionResult UpdatePhoto([FromBody] PhotoUpdateDAO photoDAO)
        {
            PhotoWithGallery photo = _photoService.UpdatePhoto(photoDAO, _authHelper.GetJwtTokenUser());
            return Ok(photo);
        }

    }
}
