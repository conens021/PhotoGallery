using DAL.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class ToPhoto
    {

        private static string GetPath(string path) {
            if (path == null || path.Equals("")) {
                return "DEFAULT";
            }
            return path;
        }

        public static Photo WithAllFields(SqlDataReader _reader)
        {
            Photo photo = new Photo();
            photo.Id = Convert.ToInt32(_reader["Id"]);
            photo.Path = Convert.ToString(_reader["Path"]);
            photo.CreatedAt = Convert.ToDateTime(_reader["CreatedAt"]);
            photo.UpdatedAt = Convert.ToDateTime(_reader["UpdatedAt"]);
            photo.GalleryId = Convert.ToInt32(_reader["GalleryId"]);
            return photo;

        }

        public static Photo WithGallery(SqlDataReader reader) {
            Photo photo = new Photo();
            photo.Id = Convert.ToInt32(reader["Id"]);
            photo.Path = Convert.ToString(reader["Path"]);
            photo.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);
            photo.UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]);
            photo.GalleryId = Convert.ToInt32(reader["GalleryId"]);

            Gallery gallery = new Gallery();
            gallery.Id = Convert.ToInt32(reader["GalleryId"]);
            gallery.Name =  reader["GalleryName"].ToString();
            gallery.CreatedAt = Convert.ToDateTime(reader["GalleryCreated"]);
            gallery.UpdatedAt = Convert.ToDateTime(reader["GalleryUpdated"]);
            gallery.CoverPhoto = GetPath(reader["CoverPhoto"].ToString());

            photo.Gallery = gallery;

            return photo;
        }
    }
}
