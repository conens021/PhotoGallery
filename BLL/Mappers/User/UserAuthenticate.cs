namespace BLL.Mappers.User
{
    public class UserAuthenticate
    {
        public UserAuthenticate(string UserNameOrEmail, string password)
        {
            this.UsernameOrEmail = UserNameOrEmail;
            this.Password = password;
        }

        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
