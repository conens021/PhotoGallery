using BLL.Mappers.User;

namespace BLL.Mappers.Gallery
{
    public class GallerySingleWithUser
    {
        public GallerySingleWithUser(DAL.Entities.Gallery gallery, UserGalleryList user)
        {
            this.GalleryId = gallery.Id;
            this.Name = gallery.Name;
            this.CreatedAt = gallery.CreatedAt;
            this.UpdatedAt = gallery.UpdatedAt;
            this.User = user;
        }

        public int GalleryId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserGalleryList User { get; set; }
    }
}
