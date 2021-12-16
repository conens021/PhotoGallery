using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {

        private readonly PhotoGalleryContext context;
        
        public GalleryRepository() {
            this.context = new PhotoGalleryContext();
        }
        public Gallery GetById(int id)
        {
            return context.Galleries.Find(id);
        }

        public Gallery Add (Gallery gallery,User user) {
            //var userFromDb =  context.Users.Find(user.Id);
            //gallery.User = userFromDb;
            //context.Galleries.Add(gallery);
            //context.SaveChanges();
            gallery.User = context.Users.Attach(user).Entity;
            context.Galleries.Add(gallery);
            context.SaveChanges();
            return gallery;
        }

        public IEnumerable<Gallery> GetAll() {
            return context.Galleries;
        }

        public Gallery Update(Gallery galleryChanges) {

            var gallery = context.Galleries.Attach(galleryChanges);
            gallery.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();

            return galleryChanges;
        }

        public Gallery Delete(int id) {

            Gallery gallery =  context.Galleries.Find(id);
            if (gallery != null) { 
                context.Galleries.Remove(gallery);
                context.SaveChanges();
            }

            return gallery;
        }

        public Gallery GetGalleryPhotos(int galleryId) {

           return  context.Galleries.Where(g => g.Id == galleryId).Include(g => g.Photos).FirstOrDefault();
        }

        public IEnumerable<string> GetCoverPhotos(int galleryId) {

            var data = context.Galleries.Where( g=> g.Id == galleryId).Select(g => new
            {
                Photos = g.Photos.OrderByDescending(p => p.CreatedAt).Take(3).Select(p => p.Path)

            }).FirstOrDefault();

            return data.Photos;
           
        }
     

    }
}
