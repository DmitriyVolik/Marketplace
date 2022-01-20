using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Controllers
{
    public class Auth : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
