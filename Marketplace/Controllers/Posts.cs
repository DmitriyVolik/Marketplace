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
            if (uploads.Count != 0)
            {
                foreach (var item in uploads)
                {
                    using (var writer = new FileStream($"{_appEnvironment.WebRootPath}/images/{Guid.NewGuid()}_{item.FileName}", FileMode.Create))
                    {
                        await item.CopyToAsync(writer);
                        Console.WriteLine($"{item.FileName}: сохранён.");
                    }
                }
            }
            return View("AddPost");
        }
    }
}
