using BankaWebEL.DTOs;

namespace BankaWebUI.Models
{
    public class ShowAccountsViewModel
    {
        public IEnumerable<AccountDTO> accountDTO { get; set; }
        public IEnumerable<CurrencyDTO> currencyDTO { get; set; }
        public IEnumerable<AccountTypeDTO> accountTypeDTO { get; set; }
        public IEnumerable<CustomerDTO> customerDTO { get; set; }



    }
}
