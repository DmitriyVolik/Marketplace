using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Marketplace.Models;
using Marketplace.Models.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    public class Posts : Controller
    {
        IWebHostEnvironment _appEnvironment;
        Context _db;

        public Posts(Context _db, IWebHostEnvironment appEnvironment)
        {
            this._db = _db;
            _appEnvironment = appEnvironment;
        }

        [Authorize]
        public IActionResult AddPost()
        {
            return View(new PostEditViewModel() 
            { 
                Categories = _db.Categories
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost(PostEditViewModel model, IFormFileCollection uploads )
        {
            var post = model.Post;
            var nophotoPath = $"/images/nophoto.png";
            Image image = new Image();
            if (uploads.Count != 0)
            {
                foreach (var item in uploads)
                {
                    var guid = Guid.NewGuid();
                    string path = $"{_appEnvironment.WebRootPath}/images/{guid}_{item.FileName}";
                    using (var writer = new FileStream(path, FileMode.Create))
                    {
                        image = new Image()
                        {
                            Path = $"/images/{guid}_{item.FileName}",
                            Post = post
                        };
                        await item.CopyToAsync(writer);
                        _db.Images.Add(image);
                        Console.WriteLine($"{item.FileName}: сохранён.");
                    }
                }
                _db.SaveChanges();
            }

            return View("AddPost");
        }
    }
}
