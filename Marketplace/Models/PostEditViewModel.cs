using Marketplace.Models.DB;
using System.Linq;

namespace Marketplace.Models
{
    public class PostEditViewModel
    {
        public Post Post { get; set; }
        public IQueryable<Category> Categories { get; set; }
    }
}
