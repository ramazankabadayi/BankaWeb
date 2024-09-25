using BankaWebBL.Managers;
using BankaWebEL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BankaWebUI.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerManager _customerManager;

        public CustomersController(CustomerManager customerManager)
        {
            _customerManager = customerManager;
        }

        public IActionResult Index()
        {
            var customers = _customerManager.GetAllCustomers();
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerDTO customerDto)
        {
            if (ModelState.IsValid)
            {
                _customerManager.AddCustomer(customerDto);
                return RedirectToAction("Index");
            }
            return View(customerDto);
        }

        public IActionResult Edit(string id)
        {
            var customer = _customerManager.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(CustomerDTO customerDto)
        {
            if (ModelState.IsValid)
            {
                _customerManager.UpdateCustomer(customerDto);
                return RedirectToAction("Index");
            }
            return View(customerDto);
        }

        public IActionResult Delete(string id)
        {
            var customer = _customerManager.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            _customerManager.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
    }
}
