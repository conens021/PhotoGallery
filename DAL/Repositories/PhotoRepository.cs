using DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private PhotoGalleryContext context;
        public PhotoRepository() { 
            this.context = new PhotoGalleryContext();
        }
        public Photo GetById(int id)
        {
            Photo photo = context.Photos.Where(p => p.Id == id).Include(p => p.Gallery).FirstOrDefault();
            return photo;
        }
        public Photo Add(Photo photo,Gallery gallery) {
            photo.Gallery = context.Galleries.Attach(gallery).Entity;
            context.Photos.Add(photo);
            context.SaveChanges();
            return photo;
        }

        public Photo Delete(int id) {
            Photo? photo = context.Photos.Find(id);

            if (photo != null) { 
                context.Photos.Remove(photo);
                context.SaveChanges();
                return photo;
            }
            return null;
        }

        public Photo Update(Photo photoChanges) {
           var photo =  context.Attach(photoChanges);
           photo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           context.SaveChanges();
           return photoChanges;
        }


    }
}
