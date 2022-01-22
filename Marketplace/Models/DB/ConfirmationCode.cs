namespace Marketplace.Models.DB
{
    public class ConfirmationCode
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public User User { get; set; }
    }
}