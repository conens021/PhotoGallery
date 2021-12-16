using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IPhotoRepository
    {
        public Photo GetById(int id);
        public Photo Add(Photo photo, Gallery gallery);

        public Photo Delete(int id);

        public Photo Update(Photo photoChanges);
    }
}
