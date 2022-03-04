namespace Presentation.Helpers
{
    public class PathRegistry
    {
        private readonly IHostEnvironment _hostingEnvironment;

        public PathRegistry(IHostEnvironment hostEnvironment) { 
            _hostingEnvironment = hostEnvironment;
        }

        public  string GetUploadFileFolder()
        {
            return Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", "Images");
        }
    }
}
