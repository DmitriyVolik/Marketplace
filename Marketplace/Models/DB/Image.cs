namespace Marketplace.Models.DB
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public Post Post { get; set; }
    }
}
