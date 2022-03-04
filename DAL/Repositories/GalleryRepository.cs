using Microsoft.EntityFrameworkCore;
using DAL.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using DAL.Mappers;

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

                    if (!dataReader.HasRows) return null;

                    dataReader.Read();

                    return ToGallery.WithAllFields(dataReader);
                }
            }

        }
        public Gallery GetGalleryPhotos(int galleryId)
        {

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("GetGalleryWithPotos", connection))
                {

                    List<Photo> photos = new List<Photo>();

                    connection.Open();

                    cmd.Parameters.AddWithValue("@GalleryId", galleryId);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader != null)
                    {
                        while (dataReader.Read())
                        {
                            photos.Add(ToPhoto.WithGallery(dataReader));
                        }
                    }

                    Gallery gallery = GetById(galleryId);
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
                    "insert into Gallery values (@GalleryName,CURRENT_TIMESTAMP,CURRENT_TIMESTAMP,@UserId,@GalleryCover);Select SCOPE_IDENTITY()",
                    connection))
                {
                    cmd.Parameters.AddWithValue("@GalleryName", gallery.Name);
                    cmd.Parameters.AddWithValue("@UserId", user.Id);

                    if (gallery.CoverPhoto == null || gallery.CoverPhoto.Equals(""))
                        gallery.CoverPhoto = "DEFAULT";

                    cmd.Parameters.AddWithValue("@GalleryCover", gallery.CoverPhoto);

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
            //Get all galleries with users Order by last updated
            string query = "GetAllGalleriesWithUser";

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.HasRows) return null;
                    List<Gallery> galleries = new List<Gallery>();

                    User user = null;

                    while (reader.Read())
                    {
                        galleries.Add(
                             ToGallery.WithUser(reader, user = new ToUser().IdAndUsername(reader))
                        );
                    }
                    return galleries;
                }
            }
        }

        public bool UpdateGalleryName(Gallery galleryChanges)
        {
            string query = "UPDATE Gallery SET Name = @GalleryName, UpdatedAt = CURRENT_TIMESTAMP WHERE Id = @GalleryId";

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@GalleryName", galleryChanges.Name);
                    cmd.Parameters.AddWithValue("@GalleryId", galleryChanges.Id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0) return true;
                    return false;
                }
            }
        }

    }
}

