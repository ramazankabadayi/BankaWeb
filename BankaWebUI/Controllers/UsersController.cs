using AutoMapper;
using BankaWebBL.Managers;
using BankaWebEL.DTOs;
using BankaWebEL.Entities;
using BankaWebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BankaWebUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CustomerManager _customerManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<IdentityUser> userManager, CustomerManager customerManager, IMapper mapper)
        {
            _userManager = userManager;
            _customerManager = customerManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Create()
        {
            var users = await _userManager.Users.ToListAsync();
            try
            {
                 users = await _userManager.Users.ToListAsync();

                foreach (var user in users)
                {
                    Console.WriteLine($"Kullanıcı: {user.Email} - ID: {user.Id}");
                }

                ViewBag.Users = users;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
            var customers = _customerManager.GetAllCustomers();


            var userInfo = from user in users
                           join customer in customers on user.Id equals customer.Id into userCustomer
                           from subCustomer in userCustomer.DefaultIfEmpty()
                           select new
                           {
                               UserId = user.Id,
                               FullName = subCustomer != null ? subCustomer.FirstName + " " + subCustomer.LastName : "Bilinmeyen Kullanıcı",
                               IdentificationNumber = subCustomer != null ? subCustomer.Id : user.Id
                           };

            ViewBag.Users = userInfo;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new IdentityUser
                {
                    UserName = model.FirstName,
                    
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    var newCustomer = new Customer
                    {
                        Id = newUser.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email
                    };

                    var customerDto = _mapper.Map<CustomerDTO>(newCustomer);

                    _customerManager.Add(customerDto);

                    return RedirectToAction("Index", "Accounts");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }
    }
}
