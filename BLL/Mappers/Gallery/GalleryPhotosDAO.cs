namespace BLL.Mappers.Gallery
{
    using BLL.Mappers.PhotoDAO;
    using DAL.Entities;
    public class GalleryPhotosDAO
    {

        public GalleryPhotosDAO(GallerySingleDAO gallery, IEnumerable<PhotoSingleDAO> photos) {
            this.Photos = photos;
            this.Gallery = gallery;
        }
        
        public GallerySingleDAO Gallery { get; set; }
        public IEnumerable<PhotoSingleDAO> Photos{ get; set; }   
    }
}
