using DAL.Entities;

namespace BLL.Mappers.PhotoDAO

{
    public class PhotoSingleDAO
    {

        public PhotoSingleDAO(Photo photo)
        {
            this.Id = photo.Id;
            this.Path = photo.Path;
            this.UpdatedAt = photo.UpdatedAt;
            this.CreatedAt = photo.CreatedAt;

        }
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
