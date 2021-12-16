using DAL.Entities;
using DAL.Repositories;
using BLL.Mappers.Gallery;
using BLL.Mappers.PhotoDAO;
using BLL.Excpetions;

namespace BLL.Services
{
    public class GalleryService
    {
        private readonly IGalleryRepository _Galleryrepository;
        private readonly UserRepository userRepository;

        public GalleryService(IGalleryRepository galleryRepository,UserRepository _userRepository) {
            _Galleryrepository = galleryRepository;
            userRepository = _userRepository;
        }

        public IEnumerable<GallerySingleDAO>  GetAllGalleries() {
            return _Galleryrepository.GetAll().Select(g => new GallerySingleDAO(g));
        }

        public GalleryPhotosDAO GetGalleryWithPhotos(int id) {

            Gallery gallery = _Galleryrepository.GetGalleryPhotos(id);

            if (gallery == null) throw new BussinesException("Gallery not found",404);

            var photos = gallery.Photos.Select(p => new PhotoSingleDAO(p));

            GalleryPhotosDAO galleryPhotos = new GalleryPhotosDAO(new GallerySingleDAO(gallery),photos);

            return galleryPhotos;

        }

        public GallerySingleDAO CreateGallery(GalleryCreateDAO galleryDAO,string username)
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

        public GallerySingleDAO UpdateGallery(GalleryUpdateDAO galleryDAO,string username)
        {

            Gallery gallery = _Galleryrepository.GetById(galleryDAO.Id);

            if (gallery == null) throw new BussinesException("Gallery not found", 404);
            User user = userRepository.GetByUsernameOrEmail(username);
            
            if (user.Id != gallery.UserId) throw new BussinesException("You dont have permission to delete this gallery!", 403);
            gallery.Name = galleryDAO.Name;
            gallery.UpdatedAt = DateTime.Now;
            _Galleryrepository.Update(gallery);
            return new GallerySingleDAO(gallery);
        }

        public Gallery Delete(int id,string username)
        {
           Gallery gallery = _Galleryrepository.GetById(id);
           if(gallery == null) throw new BussinesException("Gallery not found", 404);
           
           User user = userRepository.GetByUsernameOrEmail(username);
           if (user.Id != gallery.UserId) throw new BussinesException("You dont have permission to delete this gallery!", 403);

           _Galleryrepository.Delete(id);

            return gallery;
        }
    }
}
