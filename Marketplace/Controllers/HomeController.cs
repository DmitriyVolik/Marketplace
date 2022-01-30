using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Marketplace.Models;
using Microsoft.AspNetCore.Authorization;
using Marketplace.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Context _db;
        public HomeController(Context _db, ILogger<HomeController> logger)
        {
            this._db = _db;
            _logger = logger;
        }
        public IActionResult Index(int id = 0)
        {
            if (id == 0)
            {
                var model = new ViewPosts()
                {
                    Categories = _db.Categories.ToList(),
                    Posts = _db.Posts.Include(x => x.Images).Include(x => x.User).ToList(),
                    SelectedCategoryId = id
                };
                return View(model);
            }
            else
            {
                var model = new ViewPosts()
                {
                    Categories = _db.Categories.ToList(),
                    Posts = _db.Posts.Include(x => x.Images).Include(x => x.User).Where(x=>x.Category.Id == id).ToList(),
                    SelectedCategoryId = id
                };
                return View(model);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}