namespace BLL.Helpers
{
    public static class FileTypeValidation
    {
        private static string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".svg", ".gif" };

        public static bool Validate(string uploadedFileName)
        {
            var ext = Path.GetExtension(uploadedFileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return false;
            }

            return true;
        }

        public static string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName).ToLowerInvariant(); ;
        }

    }
}
