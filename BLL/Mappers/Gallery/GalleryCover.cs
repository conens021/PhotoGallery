using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Mappers.PhotoDAO;

namespace BLL.Mappers.Gallery
{
    public class GalleryCover
    {
        public GalleryCover() { }
        public GalleryCover(GallerySingleDAO gallery, IEnumerable<DAL.Entities.Gallery>photos) {

            this.gallery = gallery;
            this.photos = photos;
        }
        public GallerySingleDAO gallery { get; set; }
        public IEnumerable<DAL.Entities.Gallery> photos { get; set; }
    }

}
