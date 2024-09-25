using BankaWebBL.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AccountManager _accountManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, AccountManager accountManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _accountManager = accountManager;
    }

    public IActionResult Index()
    {
        var accounts = _accountManager.GetAll();
        return View(accounts);
    }
    public IActionResult Details(int id)
    {
        var account = _accountManager.GetById(id);
        if (account == null)
        {
            return NotFound();
        }
        return View(account);
    }

    [HttpGet]
    public IActionResult Login(string role)
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

        ViewBag.Role = role;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError("", "Email and password are required.");
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            await _signInManager.SignInAsync(user, isPersistent: false);

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("Admin"))
            {
                return RedirectToAction("Index", "AdminDashboard");
            }
            else if (roles.Contains("Employee"))
            {
                return RedirectToAction("Index", "EmployeeDashboard");
            }
            else if (roles.Contains("Customer"))
            {
                return RedirectToAction("Index", "CustomerDashboard");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        ModelState.AddModelError("", "Invalid login attempt.");
        return View();
    }



    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

   

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    

}
