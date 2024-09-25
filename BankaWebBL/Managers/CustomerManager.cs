using AutoMapper;
using BankaWebDL;
using BankaWebEL.DTOs;
using BankaWebEL.Entities;

namespace BankaWebBL.Managers
{
    public class CustomerManager : BaseManager<Customer, CustomerDTO, string>
    {
        public CustomerManager(BankaWebDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public IEnumerable<CustomerDTO> GetAllCustomers()
        {
            var customers = _context.Customers.ToList();
            return _mapper.Map<List<CustomerDTO>>(customers);
        }

        public void AddCustomer(CustomerDTO customerDto)
        {
            var customerEntity = _mapper.Map<Customer>(customerDto);
            _context.Customers.Add(customerEntity);
            _context.SaveChanges();
        }
        public void Add(CustomerDTO customerDto)
        {
            var customerEntity = _mapper.Map<Customer>(customerDto);
            _context.Customers.Add(customerEntity);
            _context.SaveChanges();
        }

        public void UpdateCustomer(CustomerDTO customerDto)
        {
            var customerEntity = _mapper.Map<Customer>(customerDto);
            _context.Customers.Update(customerEntity);
            _context.SaveChanges();
        }

        public void DeleteCustomer(string id)
        {
            var customer = _context.Customers.Find(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}
