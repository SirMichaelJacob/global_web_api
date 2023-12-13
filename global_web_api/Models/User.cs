namespace global_web_api.Models
{
    public class User : BaseEntity<int>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
