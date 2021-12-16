using DAL.Entities;

namespace BLL.Mappers.PhotoDAO
{
    public class PhotoWithGallery
    {

        public PhotoWithGallery(Photo photo,DAL.Entities.Gallery gallery) { 
            this.Id = photo.Id;
            this.Path = photo.Path;
            this.CreatedAt = photo.CreatedAt;
            this.UpdatedAt = photo.UpdatedAt;
            this.GalleryId = gallery.Id;
            this.GalleryName = gallery.Name;
        }

        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int GalleryId { get; set; }  
        public string GalleryName { get; set; }

    }
}
