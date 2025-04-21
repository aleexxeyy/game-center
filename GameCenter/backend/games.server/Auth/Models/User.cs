namespace Auth.Models
{
    public class User
    {
        public Guid Id { get; init; }
        public string UserName { get; init; }
        public string PasswordHash { get; set; }
    }
}
