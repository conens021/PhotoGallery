namespace BLL.Mappers.PhotoDAO
{
    public class PhotoCreateDAO
    {
        public PhotoCreateDAO() { }

        public PhotoCreateDAO(string Path,int galleryId) {
            this.Path = Path;
            this.GalleryId = galleryId; 
        }

        public String Path { get; set; }
        public int GalleryId { get; set; }
    }
}
