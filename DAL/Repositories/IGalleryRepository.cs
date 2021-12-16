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
        public Gallery GetById(int id);

        public Gallery Add(Gallery gallery, User user);

        public IEnumerable<Gallery> GetAll();

        public Gallery Update(Gallery galleryChanges);

        public Gallery Delete(int id);

        public Gallery GetGalleryPhotos(int galleryId);

        public IEnumerable<string> GetCoverPhotos(int galleryId);

    }
}
