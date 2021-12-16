using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers.User
{
    public  class UserAuthenticate
    {
        public UserAuthenticate() { }
        public UserAuthenticate(string UserNameOrEmail,string password) { 
            this.UsernameOrEmail = UserNameOrEmail;
            this.Password = password;
        }

        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
