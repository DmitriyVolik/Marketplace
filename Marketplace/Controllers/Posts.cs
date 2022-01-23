using Marketplace.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    public class Posts : Controller
    {
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(PostEditViewModel model)
        {
            System.Console.WriteLine($"{model.Test.Length}");
            //foreach (var item in model.Images)
            //{
            //    System.Console.WriteLine($"Image: {item}");
            //}
            return View("AddPost");
        }
    }
}
