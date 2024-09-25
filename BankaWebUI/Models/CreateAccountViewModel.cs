using BankaWebEL.DTOs;

namespace BankaWebUI.Models
{
    public class CreateAccountViewModel
    {
        public AccountDTO Account { get; set; }
        public List<AccountTypeDTO> AccountTypes { get; set; }
        public List<CurrencyDTO> Currencies { get; set; }
        public List<CustomerDTO> Customers { get; set; }

        public CurrencyDTO Currency {  get; set; }
        
        public CustomerDTO Customer { get; set; }

        public AccountTypeDTO AccountType { get; set; }
    }

}
