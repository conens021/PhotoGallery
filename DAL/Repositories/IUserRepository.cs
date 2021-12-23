using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IUserRepository
    {

        public User GetById(int id);

        public User GetByUsernameOrEmailaAndPassword(string userNameOrEmail, string password);

        public User GetByUsernameOrEmail(string username,string email);
        
        public User Add(User user);

        public IEnumerable<User> GetAll();

        public User Update(User userChanges);

        public User Delete(int id);

        public User GetUserGalleries(int userId);
    }
}
