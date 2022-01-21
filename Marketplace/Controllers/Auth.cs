using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Forum_MVC.Helpers;
using Marketplace.Models;
using Marketplace.Models.DB;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Marketplace.Controllers
{
    public class Auth : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Context _db;
        
        public IActionResult EmailCheck()
        {
            return View();
        }

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
            if (password!=confirmPassword)
            {
                TempData["Error"] += "Error:Passwords is not equal!\n";
                hasErrors = true;
            }
            if (hasErrors)
            {
                return View("SignUp");
            }

            
            
            /*
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, username),
                new (ClaimTypes.NameIdentifier, username),
                new (ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, "User")
                
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(claimsPrincipal);*/
            
            var user = new User() {Email = email, Username = username, PasswordHash = PasswordHash.CreateHash(password)};
            _db.Users.Add(user);

            Random rnd = new Random();
            string codeStr="";
            for (int i = 0; i < 3; i++)
            {
                codeStr += rnd.Next(0, 10).ToString();
            }

            var code = new ConfirmationCode { Code = codeStr, IsActivated = false, User = user };

            _db.ConfirmationCodes.Add(code);
            _db.SaveChanges();
            Task.Run(() => ExpireCode(code));
            
            Email.Send(email,$"<h1>Verification code:</h1><h2>{code}</h2><h2>It will be active until 5 minutes</h2>", "Verification code");
            return View("EmailCheck");
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
                new(ClaimTypes.Name, username),
                new (ClaimTypes.NameIdentifier, username),
                new (ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "User")

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
        
        public void ExpireCode(ConfirmationCode userCode)
        {
            Thread.Sleep(300000);

            var code = _db.ConfirmationCodes.Include(x=>x.User).FirstOrDefault(userCode);

            if (code!=null && !code.IsActivated)
            {
                _db.Users.Remove(code.User);
                _db.ConfirmationCodes.Remove(code);
                _db.SaveChanges();
            }
        }
        
    }
}