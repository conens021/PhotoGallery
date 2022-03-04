namespace BLL.Helpers
{
    public static class Base64StringReader
    {
        public static string GetExtension(string base64String)
        {
            string type = base64String.Split(",")[0];
            int index1 = type.IndexOf("/");
            int index2 = type.IndexOf(";");
            string extension = type.Substring(index1 + 1, index2 - (index1 + 1));

            return extension;
        }

        public static string GetFileType(string base64String)
        {
            string contentType = base64String.Split(",")[0];
            int index1 = contentType.IndexOf(":");
            int index2 = contentType.IndexOf("/");
            string type = contentType.Substring(index1 + 1, index2 - (index1 + 1));

            return type;
        }

        public static string GetContent(string base64String)
        {
            return base64String.Split(",")[1];
        }
    }
}
