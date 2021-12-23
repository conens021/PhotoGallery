using DAL.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public class ToPhoto
    {
        public Photo WithAllFields(SqlDataReader _reader)
        {
            Photo photo = new Photo();
            photo.Id = Convert.ToInt32(_reader["Id"]);
            photo.Path = Convert.ToString(_reader["Path"]);
            photo.CreatedAt = Convert.ToDateTime(_reader["CreatedAt"]);
            photo.UpdatedAt = Convert.ToDateTime(_reader["UpdatedAt"]);
            photo.GalleryId = Convert.ToInt32(_reader["GalleryId"]);
            return photo;

        }

        public Photo WithGallery(SqlDataReader reader) {
            Photo photo = new Photo();
            photo.Id = Convert.ToInt32(reader["Id"]);
            photo.Path = Convert.ToString(reader["Path"]);
            photo.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            photo.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
            photo.GalleryId = Convert.ToInt32(reader["GalleryId"]);
            photo.Gallery = new Gallery() { Id = Convert.ToInt32(reader["GalleryId"]),Name = Convert.ToString(reader["GalleryName"]) };
            return photo;
        }
    }
}
