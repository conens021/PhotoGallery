using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public User Add(User user)
        {
            throw new NotImplementedException();
        }

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            string query =
                  "select [Id],[Username],[Email],[CreatedAt],[UpdateAt],[Firstname],[Lastname] from [User] where [Id] = @Id";
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(
                    query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows) return null;
                    dataReader.Read();
                    User user = new User();
                    user.Id = Convert.ToInt32(dataReader["Id"]);
                    user.Username = Convert.ToString(dataReader["Username"]);
                    user.Email = Convert.ToString(dataReader["Email"]);
                    user.CreatedAt = Convert.ToDateTime(dataReader["CreatedAt"]);
                    user.UpdateAt = Convert.ToDateTime(dataReader["UpdateAt"]);
                    user.Firstname = Convert.ToString(dataReader["Firstname"]);
                    user.Lastname = Convert.ToString(dataReader["Lastname"]);
                    return user;
                }
            }

        }

        public User GetByUsernameOrEmail(string username)
        {
            string query =
                "select Id,Username, Email from [User] where (Username = @Username or Email = @Email )";
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(
                    query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", username);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows) return null;
                    dataReader.Read();
                    User user = new User();
                    user.Id = Convert.ToInt32(dataReader["Id"]);
                    user.Username = Convert.ToString(dataReader["Username"]);
                    user.Email = Convert.ToString(dataReader["Email"]);
                    return user;
                }
            }
        }

        public User GetByUsernameOrEmailaAndPassword(string userNameOrEmail, string password)
        {
            string query =
                "select Id,Username, Email from [User] where (Username = @Username or Email = @Email ) AND [Password] = @Password";
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(
                    query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", userNameOrEmail);
                    cmd.Parameters.AddWithValue("@Email", userNameOrEmail);
                    cmd.Parameters.AddWithValue("@Password", password);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows) return null;
                    dataReader.Read();
                    User user = new User();
                    user.Id = Convert.ToInt32(dataReader["Id"]);
                    user.Username = Convert.ToString(dataReader["Username"]);
                    user.Email = Convert.ToString(dataReader["Email"]);
                    return user;
                }
            }
        }

        public User GetUserGalleriesWithCoverPhotos(int userId)
        {
            string query = "SELECT Gallery.Id as GalleryId,Gallery.Name as GalleryName,Gallery.CreatedAt,Gallery.UpdatedAt,"+
                "[User].Username,[User].Id as UserId,[User].Firstname,[User].Lastname,[User].Email " +
                "from PhotoGallery.dbo.Gallery " +
                "LEFT JOIN PhotoGallery.dbo.[User] " +
                "ON PhotoGallery.dbo.[User].Id = PhotoGallery.dbo.Gallery.UserId " +
                "WHERE[PhotoGallery].dbo.[User].Id = @UserId " +
                "ORDER BY Gallery.CreatedAt DESC";

            User user = new User();

            List<Gallery> galleries = new List<Gallery>();

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    int row = 0;
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        row++;
                        if (row == 1)
                        {
                            user.Id = Convert.ToInt32(reader["UserId"]);
                            user.Username = Convert.ToString(reader["Username"]);
                            user.Firstname = Convert.ToString(reader["Firstname"]);
                            user.Lastname = Convert.ToString(reader["Lastname"]);
                            user.Email = Convert.ToString(reader["Email"]);
                        }

                        Gallery gallery = new Gallery()
                        {
                            Id = Convert.ToInt32(reader["GalleryId"]),
                            Name = Convert.ToString(reader["GalleryName"]),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                            UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]),

                        };
                        galleries.Add(gallery);
                    }

                }
            }

            user.Galleries = galleries;


            return user;
        }


        public IEnumerable<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public User Update(User userChanges)
        {
            throw new NotImplementedException();
        }
    }
}
