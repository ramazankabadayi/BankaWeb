using System.ComponentModel.DataAnnotations.Schema;

namespace BankaWebEL.Entities
{
    public class ExchangeRate : IBaseEntity<int>
    {
        public int Id { get; set; }

        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rate { get; set; } 

        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("FromCurrencyId")]
        public Currency FromCurrency { get; set; }

        [ForeignKey("ToCurrencyId")]
        public Currency ToCurrency { get; set; }
    }
}