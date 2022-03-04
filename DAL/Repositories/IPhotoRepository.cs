using DAL.Entities;

namespace DAL.Repositories
{
    public interface IPhotoRepository
    {
        public Photo GetById(int id);

        public Photo Add(Photo photo, Gallery gallery);

        public Photo Delete(int id);

        public Photo Update(Photo photoChanges);

        public IEnumerable<Photo> GetRecentlyAdded(int userId);
    }
}
