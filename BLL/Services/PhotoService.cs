using DAL.Repositories;
using DAL.Entities;
using BLL.Mappers.PhotoDAO;
using BLL.Excpetions;

namespace BLL.Services
{
     public class PhotoService
    {
        private IPhotoRepository photoRepository;
        private IGalleryRepository galleryRepository;
        private IUserRepository userRepository;

        public PhotoService(IPhotoRepository _photoRepository, IGalleryRepository _galleryRepository, IUserRepository _userRepository) {
            photoRepository = _photoRepository;
            galleryRepository = _galleryRepository;
            userRepository = _userRepository;
        }

        public PhotoWithGallery GetPhotoById(int id)
        {
            Photo photo =  photoRepository.GetById(id);
            if(photo == null) throw new BussinesException("Photo not found",404);
            return new PhotoWithGallery(photo, photo.Gallery);
        }

        public  PhotoWithGallery CreateaPhoto(PhotoCreateDAO photoDAO,string username)
        {
            
            Gallery gallery = galleryRepository.GetById(photoDAO.GalleryId);

            if (gallery == null) throw new BussinesException("Gallery not found", 404);
            User user = userRepository.GetById(gallery.UserId);

            if (user.Username != username) throw new BussinesException("User does not have permission to make changes to this gallery.",403);

            Photo photo = new Photo();
            photo.Path = photoDAO.Path;
            photo.CreatedAt = DateTime.Now;
            photo.UpdatedAt = DateTime.Now;
            photo = photoRepository.Add(photo, gallery);
                
            return new PhotoWithGallery(photo, gallery);
        }

        public PhotoSingleDAO Delete(int id,string username)
        {
           Photo photo =  photoRepository.Delete(id);
           if (photo == null) throw new BussinesException("Photo not found", 404);

           Gallery gallery = galleryRepository.GetById(photo.GalleryId);
           User user = userRepository.GetById(gallery.UserId);
           if (user.Username != username) throw new BussinesException("User does not have permission to make changes to this photo.", 403);


            return new PhotoSingleDAO(photo);
        }

        public PhotoWithGallery UpdatePhoto(PhotoUpdateDAO photoDAO,string username)
        {
            Photo photo = photoRepository.GetById(photoDAO.Id);
            if (photo == null) throw new BussinesException("Photo not found", 404);

            Gallery gallery = galleryRepository.GetById(photoDAO.GalleryId);
            if (gallery == null) throw new BussinesException("Gallery not found", 404);

            User user = userRepository.GetById(gallery.UserId);
            if (user.Username != username) throw new BussinesException("User does not have permission to make changes to this photo.", 403);

            photo.Path = photoDAO.Path; 
            photo.UpdatedAt = DateTime.Now;
            photo.GalleryId = photoDAO.GalleryId;
            photo = photoRepository.Update(photo);

            return new PhotoWithGallery(photo, gallery);
        }
    }
}
