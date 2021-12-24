using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DAL.Mappers;
using System.Data;

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
            string query = "Insert into [User](Username,Email,CreatedAt,UpdateAt,Firstname,Lastname,Password) " 
                + "values(@Username,@Email,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,@Firstname,@Lastname,@Password); SELECT SCOPE_IDENTITY()";

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))){
                using (SqlCommand cmd = new SqlCommand(query, conn)){
           

                    SqlParameter paramPassword = cmd.CreateParameter();
                    paramPassword.ParameterName = "@Password";
                    paramPassword.DbType =  DbType.AnsiStringFixedLength;
                    paramPassword.Value = user.Password;
                    cmd.Parameters.Add(paramPassword);

                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Firstname", user.Firstname);
                    cmd.Parameters.AddWithValue("@Lastname", user.Lastname);
                    conn.Open();
                    int userId = Convert.ToInt32(cmd.ExecuteScalar());
                    user.Id = userId;
                }
            }

            return user;
        }

        public User GetByUsernameOrEmailaAndPassword(string userNameOrEmail, string password)
        {

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [Password],[Username], [Id], [Email] FROM [dbo].[User] WHERE Username = @Username OR Email = @Email";
                    cmd.Parameters.AddWithValue("@Username", userNameOrEmail);
                    cmd.Parameters.AddWithValue("@Email", userNameOrEmail);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows) return null;
                    dataReader.Read();

                    if (dataReader["Password"].ToString() != password) return null;

                    return new ToUser().UserAuthorization(dataReader);
                }
            }
        }

        public User Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            string query = "GetAllUsers";
            List<User> users = new List<User>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows) return null;
                    while (reader.Read())
                    {
                        users.Add(new ToUser().WithAllFields(reader));
                    }
                }
            }

            return users;
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
                    return new ToUser().WithAllFields(dataReader);
                }
            }

        }

        public User GetByUsernameOrEmail(string username,string email)
        {
            string query =
                "select Id,Username, Email from [User] where (Username = @Username or Email = @Email )";
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(
                    query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows) return null;
                    dataReader.Read();
                    return new ToUser().UserAuthorization(dataReader);
                }
            }
        }

      

        public User GetUserGalleries(int userId)
        {
            string query = "UserGalleries";
            User user = null;
            List<Gallery> galleries = new List<Gallery>();

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))){

                using (SqlCommand cmd = new SqlCommand(query, conn)){

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    int row = 1;

                    if (!reader.HasRows) return null;
                    while (reader.Read()){
                        if (row == 1){
                            user = new ToUser().UserGalleries(reader);
                        }
                        galleries.Add(new ToGallery().WithUser(reader,user));
                        row++;
                    }
                }
            }
            user.Galleries = galleries;
            return user;
        }


        public User Update(User userChanges)
        {
            throw new NotImplementedException();
        }
    }
}
