﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Marketplace.Models;
using Microsoft.AspNetCore.Http;
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
        public IActionResult CreatePost(PostEditViewModel model, IFormFileCollection uploads )
        {
            Console.WriteLine(model.Post.Title);
            Console.WriteLine(uploads.Count);
            return View("AddPost");
        }
    }
}
