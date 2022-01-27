using Marketplace.Models.DB;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Models
{
    public class ViewPosts
    {
        public List<Post> Posts { get; set; }
        public List<Category> Categories { get; set; }
    }
}
