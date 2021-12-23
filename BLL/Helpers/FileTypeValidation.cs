using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers
{
    public class FileTypeValidation
    {
        private string[] permittedExtensions = { ".jpg", ".jpeg",".png",".svg",".gif" };


        public bool Validate(string uploadedFileName)
        {
            var ext = Path.GetExtension(uploadedFileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return false;
            }
            return true;

        }

        public string GetExtension(string fileName) {
            return Path.GetExtension(fileName).ToLowerInvariant(); ;
        }

    }
}
