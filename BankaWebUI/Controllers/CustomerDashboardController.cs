using Microsoft.AspNetCore.Mvc;
using BankaWebBL.Managers;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using BankaWebEL.Entities;
using Microsoft.AspNetCore.Identity;
using BankaWebUI.Models;

namespace BankaWebUI.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerDashboardController : Controller
    {
        private readonly AccountManager _accountManager;
        private readonly TransactionManager _transactionManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CustomerManager _customerManager;
        private readonly CurrencyManager _currencyManager;
        private readonly AccountTypeManager _accountType;

        public CustomerDashboardController(UserManager<IdentityUser> userManager, AccountManager accountManager, CustomerManager customerManager, CurrencyManager currencyManager, AccountTypeManager accountType, TransactionManager transactionManager)
        {
            _userManager = userManager;
            _accountManager = accountManager;
            _customerManager = customerManager;
            _currencyManager = currencyManager;
            _accountType = accountType;
            _transactionManager = transactionManager;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var accounts = _accountManager.GetAll().Where(a => a.UserId == userId).ToList();
            var currency = _currencyManager.GetAll();
            var accountType = _accountType.GetAll();
            var customer = _customerManager.GetAll();

            var viewModel = new ShowAccountsViewModel
            {
                accountDTO = accounts,
                currencyDTO = currency,
                accountTypeDTO = accountType,
                customerDTO = customer
            };

            return View(viewModel);


        }

        public IActionResult ShowAccounts()
        {
            var userId = _userManager.GetUserId(User);
            var accounts = _accountManager.GetAll().Where(a => a.UserId == userId).ToList();

            var viewModel = new ShowAccountsViewModel
            {
                accountDTO = accounts,
                  
            };

            return View(viewModel);
        }

        
        }
    } 
