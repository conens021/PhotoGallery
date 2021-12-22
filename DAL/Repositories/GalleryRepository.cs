using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly IConfiguration _configuration;

        public GalleryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public Gallery GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("Select * FROM Gallery Where Id = @GalleryId", connection))
                {
                    cmd.Parameters.AddWithValue("@GalleryId", id);
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    Gallery gallery = new Gallery();

                    if (!dataReader.HasRows) return null;

                    dataReader.Read();
                    gallery.Id = Convert.ToInt32(dataReader["Id"]);
                    gallery.Name = Convert.ToString(dataReader["Name"]);
                    gallery.CreatedAt = Convert.ToDateTime(dataReader["CreatedAt"]);
                    gallery.UpdatedAt = Convert.ToDateTime(dataReader["UpdatedAt"]);
                    gallery.UserId = Convert.ToInt32(dataReader["UserId"]);
                    return gallery;
                }
            }

        }
        public Gallery GetGalleryPhotos(int galleryId)
        {

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("Select * FROM Photo Where GalleryId = @GalleryId", connection))
                {
                    //Get gallery
                    Gallery gallery = GetById(galleryId);
                    List<Photo> photos = new List<Photo>();
                    connection.Open();
                    //Get photos
                    cmd.Parameters.AddWithValue("@GalleryId", galleryId);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader != null)
                    {
                        while (dataReader.Read())
                        {
                            Photo photo = new Photo();
                            photo.Id = Convert.ToInt32(dataReader["Id"]);
                            photo.Path = Convert.ToString(dataReader["Path"]);
                            photo.CreatedAt = Convert.ToDateTime(dataReader["CreatedAt"]);
                            photo.UpdatedAt = Convert.ToDateTime(dataReader["UpdatedAt"]);
                            photo.GalleryId = Convert.ToInt32(dataReader["GalleryId"]);
                            photos.Add(photo);
                        }
                    }
                    gallery.Photos = photos;

                    return gallery;
                }
            }
        }

        public Gallery Add(Gallery gallery, User user)
        {

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "insert into Gallery values (@GalleryName,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,@UserId);Select SCOPE_IDENTITY()", connection))
                {
                    cmd.Parameters.AddWithValue("@GalleryName", gallery.Name);
                    cmd.Parameters.AddWithValue("@UserId", user.Id);
                    connection.Open();
                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    gallery.Id = id;
                    return gallery;
                }
            }

        }

        public Gallery Delete(int id)
        {
            Gallery gallery = GetById(id);
            if (gallery == null) return null;

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Gallery WHERE ID = @GalleryId", connection))
                {
                    cmd.Parameters.AddWithValue("@GalleryId", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return gallery;
                }
            }
        }

        public IEnumerable<Gallery> GetAllGalleriesWithUser()
        {
            string query = "SELECT Gallery.Id as GalleryId,Gallery.[Name] as GalleryName, Gallery.CreatedAt, Gallery.UpdatedAt," +
                "Username,[User].Id as UserId from PhotoGallery.dbo.Gallery " +
                "LEFT JOIN PhotoGallery.dbo.[User] " +
                "ON PhotoGallery.dbo.[User].Id = PhotoGallery.dbo.Gallery.UserId ";

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows) return null;
                    List<Gallery> galleries = new List<Gallery>();
                    while (reader.Read())
                    {
                        galleries.Add(
                            new Gallery()
                            {
                                Id = Convert.ToInt32(reader["GalleryId"]),
                                Name = Convert.ToString(reader["GalleryName"]),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
                                UpdatedAt = Convert.ToDateTime(reader["UpdatedAt"]),
                                User = new User()
                                {
                                    Id = Convert.ToInt32(reader["UserId"]),
                                    Username = Convert.ToString(reader["Username"])
                                }
                            }
                         );
                    }
                    return galleries;
                }
            }
        }

        public Gallery Update(Gallery galleryChanges)
        {
            throw new NotImplementedException();
        }

    }
}

