using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.Gallery
{
     public class GallerySingleDAO
    {

        public GallerySingleDAO() { }
        public GallerySingleDAO(DAL.Entities.Gallery gallery) {
            this.Id = gallery.Id;
            this.Name = gallery.Name; 
            this.CreatedAt= gallery.CreatedAt;
            this.UpdatedAt= gallery.UpdatedAt;
            this.UserId = gallery.UserId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
