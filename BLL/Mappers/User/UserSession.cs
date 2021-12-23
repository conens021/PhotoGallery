using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.User
{
    public class UserSession
    {
        public UserAuthorize User { get; set; }
        public string Jwt { get; set; }
    }
}
