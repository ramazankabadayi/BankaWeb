using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankaWebUI.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageCustomers()
        {
            return RedirectToAction("Index", "CustomerManagement");
        }

        public IActionResult ManageAccounts()
        {
            return RedirectToAction("Index", "BankAccounts");
        }

    }
}
