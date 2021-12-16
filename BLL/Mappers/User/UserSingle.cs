using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
namespace BLL.Mappers.User
{
    public class UserSingle
    {
        public UserSingle() { }
        public UserSingle(DAL.Entities.User user) { 
            this.Id = user.Id;
            this.Username = user.Username;
            this.Firstname = user.Firstname;
            this.Lastname = user.Lastname;
            this.UpdatedAt = user.UpdateAt;
            this.CreatedAt = user.CreatedAt;
            this.Email = user.Email;
        }


        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
