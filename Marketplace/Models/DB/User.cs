namespace Marketplace.Models.DB
{
    public class User
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        
        public string Email { get; set; }
        
        public string PasswordHash { get; set; }
        public bool IsActivated { get; set; }
        
        public string PhoneNumber { get; set; }
    }
}