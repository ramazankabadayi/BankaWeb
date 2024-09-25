using BankaWebBL.Managers;
using BankaWebEL.DTOs;
using BankaWebEL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankaWebUI.Controllers
{
    public class CustomerManagementController : Controller
    {
        private readonly CustomerManager _customerManager;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomerManagementController(CustomerManager customerManager, UserManager<IdentityUser> userManager)
        {
            _customerManager = customerManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var customers = _customerManager.GetAll();
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDTO customerDto)
        {
                var identityUser = new IdentityUser
                {
                    UserName = customerDto.Email,
                    Email = customerDto.Email,
                    PhoneNumber = customerDto.PhoneNumber,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(identityUser, "Customer@123");

                if (result.Succeeded)
                {
                    customerDto.Id = identityUser.Id;
                    _customerManager.AddCustomer(customerDto);

                    await _userManager.AddToRoleAsync(identityUser, "Customer");

                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            
            return View(customerDto);
        }
    }
}
