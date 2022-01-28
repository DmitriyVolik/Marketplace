using System.Collections.Generic;
using Marketplace.Models.DB;
using System.Linq;

namespace Marketplace.Models
{
    public class PostEditViewModel
    {
        public Post Post { get; set; }
        public List<Category> Categories { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}
