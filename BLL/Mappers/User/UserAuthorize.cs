using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.User
{
    public class UserAuthorize
    {

        public UserAuthorize(DAL.Entities.User user) {
            this.Id = user.Id;
            this.Username  = user.Username;
        }

        public int Id { get; set; }
        public string Username { get; set; }
    }
}
