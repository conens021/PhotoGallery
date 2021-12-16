using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
    public partial class Gallery
    {
        public Gallery()
        {
            Photos = new HashSet<Photo>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
