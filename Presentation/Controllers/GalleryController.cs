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
    [Route("[controller]")]
    [ApiController]
    public class GalleriesController : ControllerBase
    {

        private readonly GalleryService _galleryService;
        private readonly AuthorizationHelper _authorizationHelper;
        private readonly PathRegistry _pathRegistry;

        public GalleriesController(AuthorizationHelper authorizationHelper, GalleryService galleryService, PathRegistry pathRegistry)
        {
            _galleryService = galleryService;
            _authorizationHelper = authorizationHelper;
            _pathRegistry = pathRegistry;
        }


        [HttpGet]
        public ActionResult GetAllGalleries()
        {
            return Ok(new GalleryList()
            {
                Galleries = _galleryService.GetAllGalleries().ToList()
            });
        }

        [HttpGet("{id}")]
        public ActionResult GetGalleryPhotos(int id)
        {
            GalleryPhotosDAO galleries = _galleryService.GetGalleryWithPhotos(id);
            return Ok(galleries);
        }


        [HttpPost]
        public ActionResult<GallerySingleDAO> CreateGallery([FromBody] GalleryCreateDAO galleryDAO)
        {
            GallerySingleDAO gallery = _galleryService.CreateGallery(galleryDAO, _authorizationHelper.GetJwtTokenUser());
            return Created($"/gallery/{gallery.Id}", gallery);

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteGallery(int id)
        {
            Gallery gallery = _galleryService.Delete(id, _pathRegistry.GetUploadFileFolder(), _authorizationHelper.GetJwtTokenUser());
            return Ok(gallery);

        }

        [HttpPatch]
        public ActionResult<GallerySingleDAO> UpdateGallery([FromBody] GalleryUpdateDAO galleryDAO)
        {
            GallerySingleDAO gallery = _galleryService.ChangeGalleryName(galleryDAO, _authorizationHelper.GetJwtTokenUser());
            return Created($"/gallery/{gallery.Id}", gallery);
        }

    }
}