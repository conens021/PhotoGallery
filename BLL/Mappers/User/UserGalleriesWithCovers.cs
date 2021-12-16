using BLL.Mappers.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.User
{
     public class UserGalleriesWithCovers
    {
        public UserGalleriesWithCovers() { }
        public UserGalleriesWithCovers(UserSingle user,IEnumerable<GalleryCover> galleryCovers) { 
            this.User = user;
            this.Galleries = galleryCovers;
        }


        public UserSingle User { get; set; }
        public IEnumerable<GalleryCover> Galleries { get; set; }
    }
}
