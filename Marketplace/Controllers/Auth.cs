using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Forum_MVC.Helpers;
using Marketplace.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Marketplace.Controllers
{
    public class Auth : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Context _db;

        public Auth(ILogger<HomeController> logger, Context db )
        {
            _logger = logger;
            _db = db;
        }
        
        public IActionResult SignUp()
        {
            return View();
        }
        
        public IActionResult SignIn()
        {
            return View();
        }
        
        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount(string email, string username, string password, string confirmPassword)
        {
            bool hasErrors=false;
            if (email==null || username==null || password==null)
            {
                TempData["Error"] += "Error: all fields must be filled\n";
                return View("SignUp");
            }
            if (_db.Users.FirstOrDefault(x=>x.Username==username)!=null)
            {
                TempData["Error"] += "Error: username is already in use\n";
                hasErrors = true;
            }
            if (_db.Users.FirstOrDefault(x=>x.Email==email)!=null)
            {
                TempData["Error"] += "Error: email is already in use\n";
                hasErrors = true;
            }
            
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            if (!(hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password)))
            {
                TempData["Error"] += "Error: the password must contain at least 8 characters, 1 number and a letter in uppercase\n";
                hasErrors = true;
            }
            if (hasErrors)
            {
                return View("SignUp");
            }

            var user = new User() {Email = email, Username = username, PasswordHash = PasswordHash.CreateHash(password)};
            _db.Users.Add(user);
            _db.SaveChanges();
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, username),
                new (ClaimTypes.NameIdentifier, username),
                new (ClaimTypes.Email, email)
                
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(claimsPrincipal);

            return Redirect("/");
        }
        
        [HttpPost("SignInAccount")]
        public IActionResult SignInAccount(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(x => x.Username == username );
            
            if (user==null || !PasswordHash.ValidatePassword(password, user.PasswordHash))
            {
                TempData["Error"] = "Error: incorrect login or password";
                return View("SignIn");
            }
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, password),
                new (ClaimTypes.NameIdentifier, password),
                new (ClaimTypes.Email, user.Email),

            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(claimsPrincipal);
            return Redirect("/");
        }
        
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
        
    }
}