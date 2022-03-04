using DAL.Entities;
using DAL.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {

        private readonly IConfiguration _configuration;

        public PhotoRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Photo Add(Photo photo, Gallery gallery)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("AddNewPhoto", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Path", photo.Path);
                    cmd.Parameters.AddWithValue("@GalleryId", gallery.Id);
                    conn.Open();
                    int photoId = Convert.ToInt32(cmd.ExecuteScalar());
                    photo.Id = photoId;
                }
            }
            return photo;
        }

        public Photo Delete(int id)
        {
            string query = "DELETE FROM Photo Where Id = @PhotoId";

            Photo photo = GetById(id);

            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PhotoId", id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0) return photo;
                    return null;
                }
            }
        }

        public Photo GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("GetPhotoWithGallery", connection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PhotoId", id);
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (!dataReader.HasRows) return null;

                    dataReader.Read();

                    return  ToPhoto.WithGallery(dataReader);
                }
            }
        }

        public IEnumerable<Photo> GetRecentlyAdded(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand("GetUserRecentlyPhotos", connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();

                    if (!dataReader.HasRows) return null;

                    List<Photo> photos = new List<Photo>();

                    while (dataReader.Read())
                    {
                        photos.Add(ToPhoto.WithGallery(dataReader));
                    }


                    return photos;
                }
            }
        }

        public Photo Update(Photo photoChanges)
        {
            string query = "UPDATE PHOTO set Path = @Path, GalleryId = @GalleryId where Id = @PhotoId";
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Path", photoChanges.Path);
                    cmd.Parameters.AddWithValue("@GalleryId", photoChanges.GalleryId);
                    cmd.Parameters.AddWithValue("@PhotoId", photoChanges.Id);
                    connection.Open();
                    if (cmd.ExecuteNonQuery() > 0) return photoChanges;
                    return null;
                }
            }
        }
    }
}
