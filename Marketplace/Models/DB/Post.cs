using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Marketplace.Models.DB
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public User User { get; set; }

        public User Buyer { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public float Price { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
        
        public string State { get; set; }
        
        public string DeliveryAddress { get; set; }
    }
}
