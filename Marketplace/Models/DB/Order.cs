using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Models.DB
{
    public class Order: IComparable<Order>
    {
        public int Id { get; set; }
        
        [ForeignKey("SellerId")]
        public User Seller { get; set; }
        
        [ForeignKey("BuyerId")]
        public User Buyer { get; set; }
        
        public Post Post { get; set; }
        
        public string Status { get; set; }
        
        public string DeliveryAddress { get; set; }
        
        public int CompareTo(Order? o)
        {
            Dictionary<string, int> values = new Dictionary<string, int>() {{"Unconfirmed", 1},{"Active", 2}, {"Completed", 3}, {"Canceled", 4} };
            return values[Status]-values[o.Status];
        }
    }
}