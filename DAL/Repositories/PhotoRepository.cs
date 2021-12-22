using DAL.Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
       
        public Photo Add(Photo photo, Gallery gallery)
        {
            throw new NotImplementedException();
        }

        public Photo Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Photo GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Photo Update(Photo photoChanges)
        {
            throw new NotImplementedException();
        }
    }
}
