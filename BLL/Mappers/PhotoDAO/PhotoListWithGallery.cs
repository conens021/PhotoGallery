using BLL.Mappers.Gallery;

namespace BLL.Mappers.PhotoDAO
{
    public class PhotoListWithGallery
    {

        public PhotoListWithGallery(List<PhotoSingleDAO> photos, GallerySingleDAO gallery)
        {
            this.Photos = photos;
            this.Gallery = gallery;
        }

        public List<PhotoSingleDAO> Photos { get; set; }
        public GallerySingleDAO Gallery { get; set; }
    }
}
