using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Mappers.User ;

namespace BLL.Mappers.Gallery
{
    public class GallerySingleWithUser
    {
        public GallerySingleWithUser() { }

        public GallerySingleWithUser(GallerySingleDAO gallery,UserSingle user) {
            this.Id = gallery.Id;
            this.Name = gallery.Name;
            this.CreatedAt = gallery.CreatedAt;
            this.UpdatedAt = gallery.UpdatedAt;
            this.User = user;
        }
        
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserSingle User { get; set; }
    }
}
