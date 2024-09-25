using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankaWebEL.Entities
{
    public class Account : IBaseEntity<int>
    {
        public int Id { get; set; }
        public string IBAN { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        [ForeignKey("AccountType")]
        public int AccountTypeId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        public AccountType AccountType { get; set; }
        public Currency Currency { get; set; }
        public IdentityUser User { get; set; } 
    }
}