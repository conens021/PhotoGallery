using System;
using System.Collections.Generic;


namespace DAL.Entities
{
    public partial class Photo
    {
        public int Id { get; set; }
        public string Path { get; set; } = null!;
        public int GalleryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Gallery Gallery { get; set; } = null!;

      

    }


  
}
