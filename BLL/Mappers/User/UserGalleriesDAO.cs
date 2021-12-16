namespace BLL.Mappers.User

{
    using DAL.Entities;
    public class UserGalleriesDAO
    {

        public UserGalleriesDAO() { }
        public UserGalleriesDAO(UserSingle user,IEnumerable<Gallery> galleries) {
            this.user = user;
            this.galleries = galleries;
        }


        public  UserSingle user { get; set; }
        public IEnumerable<Gallery> galleries { get; set; }
      
    }
}
