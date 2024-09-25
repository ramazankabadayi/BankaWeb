using BankaWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankaWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "AdminDashboard");
                }
                else if (User.IsInRole("Employee"))
                {
                    return RedirectToAction("Index", "EmployeeDashboard");
                }
                else if (User.IsInRole("Customer"))
                {
                    return RedirectToAction("Index", "CustomerDashboard");
                }
            }

            return RedirectToAction("Login", "Account");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
