using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaWebEL.DTOs
{
    public class ExchangeRateDTO
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; }    
        public string ToCurrency { get; set; }     
        public decimal Rate { get; set; }           
        public DateTime CreatedDate { get; set; }  
    }

}
