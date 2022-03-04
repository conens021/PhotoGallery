namespace BLL.Mappers.Gallery
{
    public class GallerySingleDAO
    {

        public GallerySingleDAO(DAL.Entities.Gallery gallery)
        {
            this.Id = gallery.Id;
            this.Name = gallery.Name;
            this.CreatedAt = gallery.CreatedAt;
            this.UpdatedAt = gallery.UpdatedAt;
            this.CoverPhoto = gallery.CoverPhoto;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CoverPhoto { get; set; }
    }
}
