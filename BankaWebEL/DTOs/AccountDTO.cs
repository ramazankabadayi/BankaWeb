using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaWebEL.DTOs
{
    public class AccountDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "IBAN is required")]
        public string IBAN { get; set; }

        [Required(ErrorMessage = "Balance is required")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "AccountType is required")]
        public string AccountType { get; set; }

        [Required(ErrorMessage = "Currency is required")]
        public string Currency { get; set; }

        public int AccountTypeId { get; set; }
        public int CurrencyId { get; set; }
        public string? UserEmail { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }

        public string CustomerId { get; set; }
    }

}
