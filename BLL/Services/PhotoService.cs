using DAL.Repositories;
using DAL.Entities;
using BLL.Mappers.PhotoDAO;
using BLL.Excpetions;
using Presentation.Helpers;
using BLL.Helpers;
using BLL.Mappers.Gallery;

namespace BLL.Services
{
    public class PhotoService
    {
        private IPhotoRepository photoRepository;
        private IGalleryRepository galleryRepository;
        private IUserRepository userRepository;
        private FileTypeValidation fileTypeValidation;

        public PhotoService(IPhotoRepository _photoRepository, IGalleryRepository _galleryRepository,
                            IUserRepository _userRepository, FileTypeValidation fileTypeValidation)
        {
            photoRepository = _photoRepository;
            galleryRepository = _galleryRepository;
            userRepository = _userRepository;
            this.fileTypeValidation = fileTypeValidation;

        }

        public PhotoWithGallery GetPhotoById(int id)
        {
            Photo photo = photoRepository.GetById(id);
            if (photo == null) throw new BussinesException("Photo not found", 404);
            return new PhotoWithGallery(photo, photo.Gallery);
        }

        public PhotoListWithGallery CreateaPhoto(MultipartRequest request, string imagesFolder, string username)
        {

            Gallery gallery = galleryRepository.GetById(request.GalleryId);

            if (gallery == null) throw new BussinesException("Gallery not found", 404);
            User user = userRepository.GetById(gallery.UserId);

            if (user.Username != username) throw new BussinesException("User does not have permission to make changes to this gallery.", 403);

            List<Photo> photos = new List<Photo>();

            request.Photos.ForEach(p =>
            {
                if (p.Length > 15 * 1024 * 1024) throw new BussinesException("Individual file can not be larger then 15MB", 400);
                if (!fileTypeValidation.Validate(p.FileName)) throw new BussinesException(
                                                          "Unsupported media type. Supported files:'.jpg','.png','.svg','.gif'", 400);
            });

            request.Photos.ForEach(f =>
            {
                string uniqueName = Guid.NewGuid().ToString() + fileTypeValidation.GetExtension(f.FileName);
                string photoPath = Path.Combine(imagesFolder, uniqueName);
                Photo photo = new Photo();
                photo.Path = uniqueName;
                photo.CreatedAt = DateTime.Now;
                photo.UpdatedAt = DateTime.Now;
                photo = photoRepository.Add(photo, gallery);
                photo.GalleryId = gallery.Id;
                photos.Add(photo);

                using (var stream = System.IO.File.Create(photoPath))
                {
                    f.CopyTo(stream);
                }
            });
            return new PhotoListWithGallery(photos.Select(p => new PhotoSingleDAO(p)).ToList(), new GallerySingleDAO(gallery));
        }

        public PhotoListWithGallery CreateaPhotoBase64(PhotoUploadBase64 photoUpload, string imagesFolder, string username)
        {
            Gallery gallery = galleryRepository.GetById(photoUpload.GalleryId);

            if (gallery == null) throw new BussinesException("Gallery not found", 404);
            User user = userRepository.GetById(gallery.UserId);

            if (user.Username != username) throw new BussinesException("User does not have permission to make changes to this gallery.", 403);

            List<Photo> photos = new List<Photo>();

            Base64StringReader base64Reader = new Base64StringReader();

            photoUpload.Files.ForEach(f => {
                if (base64Reader.GetFileType(f) != "image") throw new BussinesException(
                                                             "Unsupported media type. Supported files:'.jpg','.png','.svg','.gif'", 400);

                string fileExtension = base64Reader.GetExtension(f);
                string uniqueName = Guid.NewGuid().ToString() + "." + fileExtension;

                Photo photo = new Photo();
                photo.Path = uniqueName;
                photo.CreatedAt = DateTime.Now;
                photo.UpdatedAt = DateTime.Now;
                photo = photoRepository.Add(photo, gallery);
                photo.GalleryId = gallery.Id;
                photos.Add(photo);

                string fileContent = base64Reader.GetContent(f);
                byte[] fileBytes = Convert.FromBase64String(fileContent);
                System.IO.File.WriteAllBytes(Path.Combine(imagesFolder,uniqueName), fileBytes);

            });

            return new PhotoListWithGallery(photos.Select(p => new PhotoSingleDAO(p)).ToList(), new GallerySingleDAO(gallery));

        }

        public PhotoSingleDAO Delete(int id, string imagesFolder, string username)
        {

            Photo photo = photoRepository.GetById(id);
            if (photo == null) throw new BussinesException("Photo not found", 404);

            string filePath = Path.Combine(imagesFolder, photo.Path);
            if (!File.Exists(filePath)) throw new BussinesException("File does not exists!", 404);

            Gallery gallery = galleryRepository.GetById(photo.GalleryId);
            User user = userRepository.GetById(gallery.UserId);
            if (user.Username != username) throw new BussinesException("User does not have permission to make changes to this photo.", 403);

            //First delete physical photo
            
            File.Delete(filePath);

            photoRepository.Delete(id);

            return new PhotoSingleDAO(photo);
        }

        public void DeletePhysicalPhoto(string path, string imagesFolder, int userId) {
            
            string filePath = Path.Combine(imagesFolder, path);
            File.Delete(filePath);
        }

        public PhotoWithGallery UpdatePhoto(PhotoUpdateDAO photoDAO, string username)
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
