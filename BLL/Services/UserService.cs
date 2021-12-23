using BLL.Mappers.User;
using BLL.Mappers.Gallery;
using DAL.Entities;
using DAL.Repositories;
using BLL.Excpetions;

namespace BLL.Services
{
    public class UserService
    {
        private IUserRepository userRepository;
        private IGalleryRepository galleryRepository;

        public UserService(IUserRepository userRepository, IGalleryRepository galleryRepository)
        {
            this.userRepository = userRepository;
            this.galleryRepository = galleryRepository;
        }

        public IEnumerable<UserSingle> GetAll()
        {
            return userRepository.GetAll().Select(u => new UserSingle(u));

        }

        public UserSingle GetById(int id)
        {
            User user = userRepository.GetById(id);
            if (user == null) throw new BussinesException("User not found", 404);
            return new UserSingle(user);
        }

        public UserAuthorize GetByUsernameOrEmailAndPassword(string username,string password)
        {
            User user = userRepository.GetByUsernameOrEmailaAndPassword(username, password);
            if (user == null) throw new BussinesException("User not found", 401);
            return new UserAuthorize(user);
        }

        public UserGalleriesWithCovers GetUserGalleries(int userId)
        {
           User user = userRepository.GetUserGalleries(userId);

           if (user == null) throw new BussinesException("User not found", 404);

           IEnumerable<GallerySingleDAO> galleries = user.Galleries.Select(g => new GallerySingleDAO(g));

           return new UserGalleriesWithCovers(new UserSingle(user), galleries);

        }

        public UserAuthorize CreateUser(UserCreateDAO userDAO)
        {
            if (userRepository.GetByUsernameOrEmail(userDAO.Username, userDAO.Email) != null) 
                throw new BussinesException("User with given Username or Email already exists.",400);

            User user = new User();
            user.Username = userDAO.Username;
            user.Email = userDAO.Email;
            user.Password = userDAO.Password;
            user.Firstname = userDAO.Firstname;
            user.Lastname = userDAO.Lastname;
            user.CreatedAt = DateTime.Now;
            user.UpdateAt = DateTime.Now;
            userRepository.Add(user);

            return new UserAuthorize(user);
        }

        public UserSingle Delete(int id)
        {
            User user = userRepository.Delete(id);
            if (user == null) throw new BussinesException("User not found", 404);

            return new UserSingle(user);
        }

        public UserSingle UpdateUser(UserUpdateDAO userDAO)
        {
            User user = userRepository.GetById(userDAO.Id);

            if (user == null) throw new BussinesException("User not found.", 404);

            user.Username = userDAO.Username;
            user.Firstname = userDAO.FirstName;
            user.Lastname = userDAO.LastName;
            user.Email = userDAO.Email;
            user.UpdateAt = DateTime.Now;

            userRepository.Update(user);

            return new UserSingle(user);

        }
    }
}
