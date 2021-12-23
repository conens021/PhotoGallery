using DAL.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public class ToUser
    {
        public User IdAndUsername(SqlDataReader reader) {
            User user = new User();
            user.Id = Convert.ToInt32(reader["UserId"]);
            user.Username = Convert.ToString(reader["Username"]);
            return user;
        }

        public User WithAllFields(SqlDataReader reader) {
            User user = new User();
            user.Id = Convert.ToInt32(reader["Id"]);
            user.Username = Convert.ToString(reader["Username"]);
            user.Email = Convert.ToString(reader["Email"]);
            user.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            user.UpdateAt = Convert.ToDateTime(reader["UpdateAt"]);
            user.Firstname = Convert.ToString(reader["Firstname"]);
            user.Lastname = Convert.ToString(reader["Lastname"]);
            return user;
        }

        public User UserAuthorization(SqlDataReader reader) {

            User user = new User();
            user.Id = Convert.ToInt32(reader["Id"]);
            user.Username = Convert.ToString(reader["Username"]);
            user.Email = Convert.ToString(reader["Email"]);
            return user;
        }
        public User UserGalleries(SqlDataReader reader) {
            User user = new User();
            user.Id = Convert.ToInt32(reader["UserId"]);
            user.Username = Convert.ToString(reader["Username"]);
            user.Firstname = Convert.ToString(reader["Firstname"]);
            user.Lastname = Convert.ToString(reader["Lastname"]);
            user.Email = Convert.ToString(reader["Email"]);
            return user;
        }
    }
}
