using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private PhotoGalleryContext context;
        
        public UserRepository() {
            context = new PhotoGalleryContext();
        }

        public IEnumerable<User> GetUsers() {

            return context.Users;
        }
        public User GetById(int id)
        {
            return context.Users.Find(id);
        }

        public User GetByUsernameOrEmailaAndPassword(string userNameOrEmail, string password) {

          return  context.Users.Where(u => ((u.Username == userNameOrEmail || u.Email == userNameOrEmail) && u.Password == password)).FirstOrDefault();
        }

        public User GetByUsernameOrEmail(string username)
        {
            return context.Users.Where(u => (u.Username == username || u.Email == username)).FirstOrDefault();
        }

        public User Add(User user)
        {

            this.context.Users.Add(user);
            context.SaveChanges();

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users;
        }

        public User Update(User userChanges)
        {

            var user = context.Users.Attach(userChanges);
            user.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            context.SaveChanges();

            return userChanges;
        }

        public User Delete(int id)
        {

            User user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }

            return user;
        }

        public User GetUserGalleries(int id)
        {
           return context.Users.Where(u => u.Id == id).Include(u => u.Galleries).FirstOrDefault();

        }

       
    }
}
