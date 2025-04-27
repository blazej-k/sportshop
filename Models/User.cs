namespace SportShop.Models
{
    public enum UserType
    {
        Customer,
        Admin
    }

    public class User
    {
        public required string Id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public UserType Type { get; set; }
    }
}