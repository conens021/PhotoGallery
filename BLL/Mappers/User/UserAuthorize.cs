namespace BLL.Mappers.User
{
    public class UserAuthorize
    {

        public UserAuthorize(DAL.Entities.User user)
        {
            Id = user.Id;
            Username = user.Username;
        }

        public int Id { get; set; }
        public string Username { get; set; }
    }
}
