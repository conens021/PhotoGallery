using Microsoft.AspNetCore.Http;

namespace Presentation.Helpers
{
    public class MultipartRequest
    {
        public List<IFormFile> Photos { get; set; }

        public int GalleryId { get; set; }

    }
}
