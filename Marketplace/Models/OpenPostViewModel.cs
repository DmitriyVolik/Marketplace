using System.Collections.Generic;
using Marketplace.Models.DB;

namespace Marketplace.Models;

public class OpenPostViewModel
{
    public Post Post { get; set; }
    public List<Image> Images { get; set; }
}