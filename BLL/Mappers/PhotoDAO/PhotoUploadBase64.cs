using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.PhotoDAO
{
    public class PhotoUploadBase64
    {
        public List<string> Files { get; set; }
        public int GalleryId { get; set; }  

    }
}
