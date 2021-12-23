using BLL.Mappers.Gallery;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.PhotoDAO
{
    public class PhotoListWithGallery
    {

        public PhotoListWithGallery(List<PhotoSingleDAO> photos, GallerySingleDAO gallery)
        {
           this.Photos = photos;
           this.Gallery = gallery;
        }

        public List<PhotoSingleDAO> Photos { get; set; }
        public GallerySingleDAO Gallery { get; set; }
    }
}
