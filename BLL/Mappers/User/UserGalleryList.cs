using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.User
{
    public class UserGalleryList
    {
        public UserGalleryList(DAL.Entities.User user) {
            Id = user.Id;
            Username = user.Username;
        }

        public int Id { get; set; }
        public string Username { get; set; }
    }
}
