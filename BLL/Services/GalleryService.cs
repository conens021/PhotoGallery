using DAL.Entities;
using DAL.Repositories;
using BLL.Mappers.Gallery;
using BLL.Mappers.PhotoDAO;
using BLL.Excpetions;
using BLL.Mappers.User;

namespace BLL.Services
{
    public class GalleryService
    {
        private readonly IGalleryRepository _Galleryrepository;
        private readonly IUserRepository userRepository;
        private readonly PhotoService photoService;

        public GalleryService(IGalleryRepository galleryRepository, IUserRepository _userRepository
                                , PhotoService photoService)
        {
            _Galleryrepository = galleryRepository;
            userRepository = _userRepository;
            this.photoService = photoService;
        }

        public IEnumerable<GallerySingleWithUser> GetAllGalleries()
        {
            return _Galleryrepository.GetAllGalleriesWithUser().
                 Select(g => new GallerySingleWithUser(g, new UserGalleryList(g.User)
                     ));
        }

        public GalleryPhotosDAO GetGalleryWithPhotos(int id)
        {

            Gallery gallery = _Galleryrepository.GetGalleryPhotos(id);

            if (gallery == null) throw new BussinesException("Gallery not found", 404);

            var photos = gallery.Photos.Select(p => new PhotoSingleDAO(p));

            GalleryPhotosDAO galleryPhotos = new GalleryPhotosDAO(new GallerySingleDAO(gallery), photos);

            return galleryPhotos;

        }

        public GallerySingleDAO CreateGallery(GalleryCreateDAO galleryDAO, string username)
        {
            User user = userRepository.GetById(galleryDAO.UserId);
            if (user == null) throw new BussinesException("User not found", 404);
            if (user.Username != username) throw new BussinesException("You dont have a permission to create this gallery!", 403);

            Gallery gallery = new Gallery();
            gallery.Name = galleryDAO.Name;
            gallery.CreatedAt = DateTime.Now;
            gallery.UpdatedAt = DateTime.Now;
            _Galleryrepository.Add(gallery, user);
            return new GallerySingleDAO(gallery);
        }

        public GallerySingleDAO ChangeGalleryName(GalleryUpdateDAO galleryDAO, string username)
        {

            Gallery gallery = _Galleryrepository.GetById(galleryDAO.Id);

            if (gallery == null) throw new BussinesException("Gallery not found", 404);
            User user = userRepository.GetByUsernameOrEmail(username, username);

            if (user.Id != gallery.UserId) throw new BussinesException("You dont have permission to delete this gallery!", 403);
            gallery.Name = galleryDAO.Name;
            gallery.UpdatedAt = DateTime.Now;
            _Galleryrepository.UpdateGalleryName(gallery);
            return new GallerySingleDAO(gallery);
        }

        public Gallery Delete(int id, string imagesFolder, string username)
        {
            Gallery gallery = _Galleryrepository.GetGalleryPhotos(id);
            if (gallery == null) throw new BussinesException("Gallery not found", 404);

            User user = userRepository.GetByUsernameOrEmail(username, username);
            if (user.Id != gallery.UserId) throw new BussinesException("You dont have permission to delete this gallery!", 403);


            //delete all photos releated with this gallerz
            foreach (Photo photo in gallery.Photos)
            {
                photoService.DeletePhysicalPhoto(photo.Path, imagesFolder, photo.Id);
            }

            _Galleryrepository.Delete(id);


            return gallery;
        }


    }
}
