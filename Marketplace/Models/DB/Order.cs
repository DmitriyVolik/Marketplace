using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Models.DB
{
    public class Order
    {
        public int Id { get; set; }
        
        [ForeignKey("SellerId")]
        public User Seller { get; set; }
        
        [ForeignKey("BuyerId")]
        public User Buyer { get; set; }
        
        public Post Post { get; set; }
        
        public string Status { get; set; }
        
        public string DeliveryAddress { get; set; }
    }
}