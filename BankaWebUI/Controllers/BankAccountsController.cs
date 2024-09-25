using AutoMapper;
using BankaWebBL.Managers;
using BankaWebEL.DTOs;
using BankaWebEL.Entities;
using BankaWebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class BankAccountsController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AccountManager _accountManager;
    private readonly CustomerManager _customerManager;
    private readonly CurrencyManager _currencyManager;
    private readonly AccountTypeManager _accountType;
    private readonly IMapper _mapper;

    public BankAccountsController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, AccountManager accountManager, CustomerManager customerManager, IMapper mapper, CurrencyManager currencyManager, AccountTypeManager accountType)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _accountManager = accountManager;
        _customerManager = customerManager;
        _mapper = mapper;
        _currencyManager = currencyManager;
        _accountType = accountType;
    }

    public IActionResult ChooseLogin()
    {
        return View();
    }
    public IActionResult SelectRole()
    {
        return View();
    }

    public IActionResult Index()
    {
        var accounts = _accountManager.GetAll();
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
        ViewBag.Role = role;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string role, string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ModelState.AddModelError("", "Email ve şifre boş olamaz.");
            return View();
        }

        var user = await _userManager.FindByEmailAsync(email);

        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Contains(role))
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                if (role == "Admin")
                {
                    return RedirectToAction("Index", "AdminDashboard");
                }
                else if (role == "Employee")
                {
                    return RedirectToAction("Index", "EmployeeDashboard");
                }
                else
                {
                    return RedirectToAction("Index", "CustomerDashboard");
                }
            }
            else
            {
                ModelState.AddModelError("", "Seçtiğiniz rol için yetkiniz bulunmuyor.");
            }
        }
        else
        {
            ModelState.AddModelError("", "Geçersiz email veya şifre.");
        }

        return View();
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("ChooseLogin");
    }



    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
    }

    public IActionResult CreateOrUpdate()
    {
        ViewBag.AccountTypes = _accountManager.GetAccountTypes(); 
        ViewBag.Currencies = _accountManager.GetCurrencies(); 
        ViewBag.Users = _userManager.Users.ToList(); 

        return View();
    }

    [HttpPost]
    public IActionResult CreateOrUpdate(AccountDTO accountDto)
    {
        if (ModelState.IsValid)
        {
            _accountManager.AddOrUpdate(accountDto); 
            return RedirectToAction("Index");
        }

      
        var accountTypes = _accountManager.GetAccountTypes();
        var accountTypeDtos = _mapper.Map<List<AccountTypeDTO>>(accountTypes);
        var currencies = _accountManager.GetCurrencies();
        var currencyDtos = _mapper.Map<List<CurrencyDTO>>(currencies);
        var customers = _customerManager.GetAllCustomers().ToList();

        var viewModel = new CreateAccountViewModel
        {
            AccountTypes = accountTypeDtos,
            Currencies = currencyDtos,
            Customers = customers
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var accountTypes = _mapper.Map<List<AccountTypeDTO>>(_accountManager.GetAccountTypes());
        var currencies = _mapper.Map<List<CurrencyDTO>>(_accountManager.GetCurrencies());
        var customers = _customerManager.GetAllCustomers().ToList(); 

        var viewModel = new CreateAccountViewModel
        {
            AccountTypes = accountTypes,
            Currencies = currencies,
            Customers = customers,
            Account = new AccountDTO() 
        };

        return View(viewModel);
    }

   
    [HttpPost]
    public IActionResult Create(CreateAccountViewModel viewModel)
    {
        if (ModelState.IsValid)
        {           
            viewModel.AccountTypes = _mapper.Map<List<AccountTypeDTO>>(_accountManager.GetAccountTypes());
            viewModel.Currencies = _mapper.Map<List<CurrencyDTO>>(_accountManager.GetCurrencies());
            viewModel.Customers = _customerManager.GetAllCustomers().ToList();

            return View(viewModel);
        }

        var accountEntity = _mapper.Map<Account>(viewModel.Account);

        _accountManager.Add(accountEntity);

        return RedirectToAction("Index");
    }



}
