﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using BLL.Mappers.User;

namespace BLL.Mappers.Gallery
{
    public class GallerySingleWithUser
    {
        public GallerySingleWithUser() { }

        public GallerySingleWithUser(DAL.Entities.Gallery gallery,UserSingle user) {
            this.GalleryId = gallery.Id;
            this.Name = gallery.Name;
            this.CreatedAt = gallery.CreatedAt;
            this.UpdatedAt = gallery.UpdatedAt;
            this.User = user;
        }
        
        public int GalleryId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserSingle User { get; set; }
    }
}
