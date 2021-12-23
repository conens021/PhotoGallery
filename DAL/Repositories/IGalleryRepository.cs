using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IGalleryRepository
    {

        public Gallery GetById(int galleryId);
        public Gallery Add(Gallery gallery, User user);

        public bool UpdateGalleryName(Gallery galleryChanges);

        public Gallery Delete(int id);

        public Gallery GetGalleryPhotos(int galleryId);

        public IEnumerable<Gallery> GetAllGalleriesWithUser();


    }
}
