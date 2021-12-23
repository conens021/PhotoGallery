using DAL.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public class ToGallery
    {

        public Gallery WithAllFields(SqlDataReader _reader) {
            Gallery gallery = new Gallery();
            gallery.Id = Convert.ToInt32(_reader["Id"]);
            gallery.Name = Convert.ToString(_reader["Name"]);
            gallery.CreatedAt = Convert.ToDateTime(_reader["CreatedAt"]);
            gallery.UpdatedAt = Convert.ToDateTime(_reader["UpdatedAt"]);
            gallery.UserId = Convert.ToInt32(_reader["UserId"]);
            return gallery;
        }

        public Gallery WithUser(SqlDataReader _reader, User user) {
            return new Gallery()
            {
                Id = Convert.ToInt32(_reader["GalleryId"]),
                Name = Convert.ToString(_reader["GalleryName"]),
                CreatedAt = Convert.ToDateTime(_reader["CreatedAt"]),
                UpdatedAt = Convert.ToDateTime(_reader["UpdatedAt"]),
                User = user

            };
        }

    }
}
