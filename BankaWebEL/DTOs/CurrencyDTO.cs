using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaWebEL.DTOs
{
    public class CurrencyDTO
    {
        public int Id { get; set; }             
        public string CurrencyName { get; set; } 
        public string CurrencyCode { get; set; }   
        public string Symbol { get; set; }         
    }

}
