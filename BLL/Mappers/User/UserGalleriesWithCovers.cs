using BLL.Mappers.Gallery;

namespace BLL.Mappers.User
{
    public class UserGalleriesWithCovers
    {
        public UserGalleriesWithCovers(UserSingle user, IEnumerable<GallerySingleDAO> galleryCovers)
        {
            User = user;
            Galleries = galleryCovers;
        }

        public UserSingle User { get; set; }
        public IEnumerable<GallerySingleDAO> Galleries { get; set; }
    }
}
