namespace SaLes__APIs.Serviecs.Security
{
    public class UserLogin
    {
        public UserLogin()
        {
            UserName = string.Empty;
            StoredPassword = string.Empty;
        }
        public string UserName { get; set; }
        public string StoredPassword { get; set; }
    }
}
