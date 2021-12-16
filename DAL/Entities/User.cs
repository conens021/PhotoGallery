using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DAL.Entities
{
    public partial class User
    {
        public User()
        {
            Galleries = new HashSet<Gallery>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public virtual ICollection<Gallery> Galleries { get; set; }
    }
}
