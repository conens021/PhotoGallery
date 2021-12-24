using Microsoft.AspNetCore.Mvc;
using BLL.Mappers.Gallery;
using DAL.Entities;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using Presentation.Attributes;
using Presentation.Helpers;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    public class GalleryController : ControllerBase
    {

        private readonly GalleryService _galleryService;
        private readonly AuthorizationHelper authorizationHelper;
        private readonly IHostEnvironment hostingEnvironment;
        public GalleryController(AuthorizationHelper _authorizationHelper,GalleryService galleryService,
                                  IHostEnvironment hostingEnvironment)
        {
            _galleryService = galleryService;
            authorizationHelper = _authorizationHelper;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("/galleries")]
        public ActionResult GetAllGalleries() {
            return Ok(new GalleryList() { 
                Galleries = _galleryService.GetAllGalleries().ToList() 
            });
        }

        [HttpGet("/gallery/{galleryId}")]
        public ActionResult GetGalleryPhotos(int galleryId)
        {
            GalleryPhotosDAO galleries = _galleryService.GetGalleryWithPhotos(galleryId);
            return Ok(galleries);
        }


        [HttpPost("/gallery")]
        public ActionResult<GallerySingleDAO> CreateGallery([FromBody] GalleryCreateDAO galleryDAO)
        {
                GallerySingleDAO gallery = _galleryService.CreateGallery(galleryDAO,authorizationHelper.GetJwtTokenUser());
                return Created($"/gallery/{gallery.Id}", gallery);
           
        }

        [HttpDelete("/gallery/{id}")]
        public ActionResult DeleteGallery(int id)
        {
            string imagesFolder = Path.Combine(hostingEnvironment.ContentRootPath, "wwwroot", "Images");
            Gallery gallery = _galleryService.Delete(id, imagesFolder, authorizationHelper.GetJwtTokenUser());
            return Ok(gallery);

        }

        [HttpPatch("/gallery")]
        public ActionResult<GallerySingleDAO> UpdateGallery([FromBody] GalleryUpdateDAO galleryDAO)
        {
              GallerySingleDAO gallery = _galleryService.ChangeGalleryName(galleryDAO, authorizationHelper.GetJwtTokenUser());
              return Created($"/gallery/{gallery.Id}", gallery);
            
        }

    }
}